// Written by Joe Zachary for CS 3500, November 2012
// Revised by Joe Zachary April 2016
// Revised extensively by Joe Zachary April 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CustomNetworking
{

    /// <summary>
    /// The type of delegate that is called when a StringSocket send has completed.
    /// </summary>
    public delegate void SendCallback(bool wasSent, object payload);

    /// <summary>
    /// The type of delegate that is called when a receive has completed.
    /// </summary>
    public delegate void ReceiveCallback(String s, object payload);

    /// <summary> 
    /// A StringSocket is a wrapper around a Socket.  It provides methods that
    /// asynchronously read lines of text (strings terminated by newlines) and 
    /// write strings. (As opposed to Sockets, which read and write raw bytes.)  
    ///
    /// StringSockets are thread safe.  This means that two or more threads may
    /// invoke methods on a shared StringSocket without restriction.  The
    /// StringSocket takes care of the synchronization.
    /// 
    /// Each StringSocket contains a Socket object that is provided by the client.  
    /// A StringSocket will work properly only if the client refrains from calling
    /// the contained Socket's read and write methods.
    /// 
    /// We can write a string to a StringSocket ss by doing
    /// 
    ///    ss.BeginSend("Hello world", callback, payload);
    ///    
    /// where callback is a SendCallback (see below) and payload is an arbitrary object.
    /// This is a non-blocking, asynchronous operation.  When the StringSocket has 
    /// successfully written the string to the underlying Socket, or failed in the 
    /// attempt, it invokes the callback.  The parameter to the callback is the payload.  
    /// 
    /// We can read a string from a StringSocket ss by doing
    /// 
    ///     ss.BeginReceive(callback, payload)
    ///     
    /// where callback is a ReceiveCallback (see below) and payload is an arbitrary object.
    /// This is non-blocking, asynchronous operation.  When the StringSocket has read a
    /// string of text terminated by a newline character from the underlying Socket, or
    /// failed in the attempt, it invokes the callback.  The parameters to the callback are
    /// a string and the payload.  The string is the requested string (with the newline removed).
    /// </summary>

    public class StringSocket : IDisposable
    {
        // Underlying socket
        private Socket socket;

        // Encoding used for sending and receiving
        private Encoding encoding;

        //
        private Queue<ReceiveCallback> callbackQueue = new Queue<ReceiveCallback>();
        private const int BUFFER_SIZE = 1024;
        private byte[] incomingBytes = new byte[BUFFER_SIZE];
        private char[] incomingChars = new char[BUFFER_SIZE];
        private Decoder decoder;
        private StringBuilder incoming;
        private int pendingIndex = 0;
        private StringBuilder outgoing;
        private byte[] pendingBytes = new byte[0];
        private readonly object sendSync = new object();
        private readonly object recSync = new object();
        private readonly object recQueSync = new object();
        private bool sendIsOngoing;
        private bool isReceiving;

        private Queue<SendCallback> sendCallbackQueue;
        private Queue<Object> sendCallbackPayloadQueue;
        private Queue<ReceiveCallback> receiveCallbackQueue;
        private Queue<Object> recieveCallbackPayloadQueue;
        private Queue<string> stringBack;
        private string partialMessage;

        private Queue<SendSave> sendSaveQueue;
        private Queue<ReceiveSave> receiveQueueSave;
        int pendingRecIndex;

        private int sentBytes;
        int bytesReceivedSoFar;
        private bool enteredTry;



        /// <summary>
        /// Creates a StringSocket from a regular Socket, which should already be connected.  
        /// The read and write methods of the regular Socket must not be called after the
        /// StringSocket is created.  Otherwise, the StringSocket will not behave properly.  
        /// The encoding to use to convert between raw bytes and strings is also provided.
        /// </summary>
        internal StringSocket(Socket s, Encoding e)
        {
            socket = s;
            encoding = e;
            StringSocketListener list = new StringSocketListener(4000, e);

            sendSaveQueue = new Queue<SendSave>();
            receiveQueueSave = new Queue<ReceiveSave>();
            stringBack = new Queue<string>();
        }

        /// <summary>
        /// Stuct used to save the data that needs to be sent by the socket
        /// </summary>
        private struct SendSave
        {
            public string SentMessage { get; set; }
            public object Payload { get; set; }
            public SendCallback Callback { get; set; }
        }

        /// <summary>
        /// Struct used to save the data that is being received by the socket
        /// </summary>
        private struct ReceiveSave
        {
            public object Payload { get; set; }
            public ReceiveCallback Callback { get; set; }
            public int Length { get; set; }
        }

        /// <summary>
        /// Shuts down this StringSocket.
        /// </summary>
        public void Shutdown(SocketShutdown mode)
        {
            socket.Shutdown(mode);
        }

        /// <summary>
        /// Closes this StringSocket.
        /// </summary>
        public void Close()
        {
            socket.Close();
        }


        /// <summary>
        /// We can write a string to a StringSocket ss by doing
        /// 
        ///    ss.BeginSend("Hello world", callback, payload);
        ///    
        /// where callback is a SendCallback (see below) and payload is an arbitrary object.
        /// This is a non-blocking, asynchronous operation.  When the StringSocket has 
        /// successfully written the string to the underlying Socket it invokes the callback.  
        /// The parameters to the callback are true and the payload.
        /// 
        /// If it is impossible to send because the underlying Socket has closed, the callback 
        /// is invoked with false and the payload as parameters.
        ///
        /// This method is non-blocking.  This means that it does not wait until the string
        /// has been sent before returning.  Instead, it arranges for the string to be sent
        /// and then returns.  When the send is completed (at some time in the future), the
        /// callback is called on another thread.
        /// 
        /// This method is thread safe.  This means that multiple threads can call BeginSend
        /// on a shared socket without worrying around synchronization.  The implementation of
        /// BeginSend must take care of synchronization instead.  On a given StringSocket, each
        /// string arriving via a BeginSend method call must be sent (in its entirety) before
        /// a later arriving string can be sent.
        /// </summary>
        public void BeginSend(String s, SendCallback callback, object payload)
        {
            //Lock to the save queue
            lock (sendSaveQueue)
            {
                //Enqueue the send information
                sendSaveQueue.Enqueue(new SendSave { SentMessage = s, Callback = callback, Payload = payload });

                //If the send is not going, start it.
                if (!sendIsOngoing)
                {
                    Console.WriteLine("Sending " + sendSaveQueue.Peek().SentMessage.ToString());
                    sendIsOngoing = true;
                    SendBytes();
                }
            }
        }


        /// <summary>
        /// Used by the string socket to begin sending bytes to the reveving socket.
        /// Initialized bytes sent to track how many bytes have been sent to know when sending is done.
        /// If there is an exception, the send is dequeued.
        /// </summary>
        private void SendBytes()
        {
            sentBytes = 0;
            pendingBytes = encoding.GetBytes(sendSaveQueue.Peek().SentMessage);
            try
            {
                //Begin sending bytes
                socket.BeginSend(pendingBytes, 0, pendingBytes.Length, SocketFlags.None, MessageSent, null);

            }
            catch (Exception)
            {
                sendSaveQueue.Dequeue();
            }
        }

        /// <summary>
        /// Uses the socket to send a message and continues to try until the entire string of bits its sent.
        /// Another task is created to send the callback to not block the BeginSend.
        /// </summary>
        /// <param name="result"></param>
        private void MessageSent(IAsyncResult result)
        {
            lock (sendSaveQueue)
            {
                //Keep track of bytes sent
                sentBytes = sentBytes + socket.EndSend(result);

                //How many bytes are left to be sent
                int leftToSend = pendingBytes.Length - sentBytes;

                if (leftToSend > 0)
                {
                    socket.BeginSend(pendingBytes, sentBytes, leftToSend, SocketFlags.None, MessageSent, null);
                }
                else
                {
                    //New task to send the callback
                    SendSave toCall = sendSaveQueue.Dequeue();
                    Task sendCallback = new Task(() => toCall.Callback.Invoke(true, toCall.Payload));
                    sendCallback.Start();

                    //If there is still something in the queue, Send it out.
                    if (sendSaveQueue.Count > 0)
                    {
                        SendBytes();
                    }
                    else
                    {
                        sendIsOngoing = false;
                    }
                }
            }
        }

        /// <summary>
        /// We can read a string from the StringSocket by doing
        /// 
        ///     ss.BeginReceive(callback, payload)
        ///     
        /// where callback is a ReceiveCallback (see below) and payload is an arbitrary object.
        /// This is non-blocking, asynchronous operation.  When the StringSocket has read a
        /// string of text terminated by a newline character from the underlying Socket, it 
        /// invokes the callback.  The parameters to the callback are a string and the payload.  
        /// The string is the requested string (with the newline removed).
        /// 
        /// Alternatively, we can read a string from the StringSocket by doing
        /// 
        ///     ss.BeginReceive(callback, payload, length)
        ///     
        /// If length is negative or zero, this behaves identically to the first case.  If length
        /// is positive, then it reads and decodes length bytes from the underlying Socket, yielding
        /// a string s.  The parameters to the callback are s and the payload
        ///
        /// In either case, if there are insufficient bytes to service a request because the underlying
        /// Socket has closed, the callback is invoked with null and the payload.
        /// 
        /// This method is non-blocking.  This means that it does not wait until a line of text
        /// has been received before returning.  Instead, it arranges for a line to be received
        /// and then returns.  When the line is actually received (at some time in the future), the
        /// callback is called on another thread.
        /// 
        /// This method is thread safe.  This means that multiple threads can call BeginReceive
        /// on a shared socket without worrying around synchronization.  The implementation of
        /// BeginReceive must take care of synchronization instead.  On a given StringSocket, each
        /// arriving line of text must be passed to callbacks in the order in which the corresponding
        /// BeginReceive call arrived.
        /// 
        /// Note that it is possible for there to be incoming bytes arriving at the underlying Socket
        /// even when there are no pending callbacks.  StringSocket implementations should refrain
        /// from buffering an unbounded number of incoming bytes beyond what is required to service
        /// the pending callbacks.
        /// </summary>
        public void BeginReceive(ReceiveCallback callback, object payload, int length = 0)
        {
            lock (recSync)
            {
                //get the callback info!
                receiveQueueSave.Enqueue(new ReceiveSave { Callback = callback, Payload = payload,Length = length });

                if (!isReceiving)
                {
                    //did it enter the try?
                    enteredTry = false;
                    isReceiving = true;
                    ReceiveNewMessage();
                }

            }
        }

        private void ReceiveNewMessage()
        {
            //if we have messages pending
            while (receiveQueueSave.Count > 0)
            {
                //if we have a string to send.
                if (stringBack.Count > 0)
                {
                    ReceiveSave received = receiveQueueSave.Dequeue();
                    string sb = stringBack.Dequeue();
                    //if we have to remove some of the leftovers and there are no more bytes to
                    //receive.
                     if (received.Length > 0 && enteredTry == false)
                     {
                        //cut off the string and send it back.
                         Task.Run(() => received.Callback(sb.Substring(0, received.Length), received.Payload));
                        sb.Substring(received.Length);

                        //get the new index!
                        int rec = received.Length;
                        int sbl = sb.Length;
                        int remain = sbl - rec;

                        if (remain > 0)
                        {
                            stringBack.Enqueue(sb.Substring(remain));
                        }
                        

                     }
                    else
                    {
                        Task.Run(() => received.Callback(sb, received.Payload));

                    }
                }
               
                else
                {
                    break;
                }

            }
            //if we only need to read in part of the line.
            if (receiveQueueSave.Count > 0 && receiveQueueSave.Peek().Length > 0)
            {
                try
                {
                    Task.Run(() => socket.BeginReceive(incomingBytes, 0, receiveQueueSave.Peek().Length, SocketFlags.None,
                             MessageReceived, null));
                }
                catch (ObjectDisposedException)
                {
                    receiveQueueSave.Dequeue();
                }
            }
            //if we need to read in the whole line.
            else if (receiveQueueSave.Count > 00)
            {
                try
                {
                    Task.Run(() => socket.BeginReceive(incomingBytes, 0, incomingBytes.Length, SocketFlags.None,
                             MessageReceived, null));
                }
                catch (ObjectDisposedException)
                {
                    receiveQueueSave.Dequeue();
                }
            }
            else
            {
                //reset the values!
                isReceiving = false;
                enteredTry = false;
            }
        }
        /// <summary>
        ///  gets the bytes and if there is a length restriction it just adds the bytes to the queue, otherwise we
        ///  need to remove the \n character.
        /// </summary>
        /// <param name="ar"></param>
        private void MessageReceived(IAsyncResult ar)
        {
            lock (recSync)
            {
                enteredTry = true;
                // returns the number of bytes received in the previous instance of socket.BeginReceive()
                int bytesReceived = socket.EndReceive(ar);
                bytesReceivedSoFar = bytesReceived;

                if (receiveQueueSave.Peek().Length > 0)
                {
                    partialMessage += encoding.GetString(incomingBytes, 0, bytesReceived);
                    stringBack.Enqueue(partialMessage);
                    partialMessage = partialMessage.Substring(partialMessage.Length);

                }
                else
                {

                    // add the bytes to the partial message.
                    partialMessage += encoding.GetString(incomingBytes, 0, bytesReceived);

                    // if we still have a new line that needs to be removed.
                    int times = 0;
                    while (!(partialMessage.IndexOf("\n").Equals(-1)) && times == 0) 
                    {
                        // get the index, add the substring to the queue, and increase the index of partialMessage/
                        int messageLength = partialMessage.IndexOf("\n");
                        stringBack.Enqueue(partialMessage.Substring(0, messageLength));
                        partialMessage = partialMessage.Substring(messageLength + 1);
                    }
                }
                //check for more!
                ReceiveNewMessage();
            }

        }
       

        /// <summary>
        /// Frees resources associated with this StringSocket.
        /// </summary>
        public void Dispose()
        {
            Shutdown(SocketShutdown.Both);
            Close();
        }
    }
}
