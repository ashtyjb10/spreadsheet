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

            sendCallbackQueue = new Queue<SendCallback>();
            sendCallbackPayloadQueue = new Queue<object>();
            receiveCallbackQueue = new Queue<ReceiveCallback>();
            recieveCallbackPayloadQueue = new Queue<object>();

            decoder = e.GetDecoder();

            sendSaveQueue = new Queue<SendSave>();
            receiveQueueSave = new Queue<ReceiveSave>();
            

            incoming = new StringBuilder();
            outgoing = new StringBuilder();
            stringBack = new Queue<string>();
            pendingRecIndex = 0;
            string incomingString = "";
            ReceiveCallback sb;
            //this.BeginSend("Hello\n", (bb, pp) => {}, null);
            //this.BeginReceive((ss,p) => { incomingString = ss; }, null);
            sendIsOngoing = false;
            //remember the socket and encoding
            //call begin recieve to start listening

            StringSocketListener list = new StringSocketListener(4000, e);


            // TODO: Complete implementation of StringSocket
        }

        private struct SendSave
        {
            public StringBuilder sentMessage { get; set; }
            public object Payload { get; set; }
            public SendCallback Callback { get; set; }
        }

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
            
            //remember the callback, string and payload that needs to be stored.
            //send bytes out
            //use the socket to send the bytes.
            //when the bytes have been completely sent you can quit calling the callback.
            //same idea as the send in the chat server.

            lock (sendSync)
            {
                //convert string into an array of bytes.
                outgoing.Append(s);
                pendingBytes = encoding.GetBytes(outgoing.ToString());

                sendCallbackQueue.Enqueue(callback);
                sendCallbackPayloadQueue.Enqueue(payload);
                if (!sendIsOngoing)
                {
                    sendIsOngoing = true;
                    sendBytes();
                }
            }
        }



        public void sendBytes()
        {
            //we are in the middle of sending.
            if (pendingIndex < pendingBytes.Length)
            {
                try
                {
                    socket.BeginSend(pendingBytes, pendingIndex, pendingBytes.Length - pendingIndex,
                            SocketFlags.None, MessageSent, null);
                }
                catch (ObjectDisposedException)
                {

                }
            }
            //not currently have block of bytes, make a new one!
            else if (outgoing.Length > 0)
            {
                pendingBytes = encoding.GetBytes(outgoing.ToString());
                pendingIndex = 0;
                outgoing.Clear();
                try
                {
                    socket.BeginSend(pendingBytes, pendingIndex, pendingBytes.Length - pendingIndex,
                           SocketFlags.None, MessageSent, null);
                }
                catch (ObjectDisposedException)
                {
                }
            }
            else
            {
                SendCallback send = sendCallbackQueue.Dequeue();
                object pay = sendCallbackPayloadQueue.Dequeue();
                send(true, pay);
                sendIsOngoing = false;
            }
        }

        private void MessageSent(IAsyncResult result)
        {
            lock(sendSync)
            { 
                int byteSent = socket.EndSend(result);
                if (byteSent == 0)
                {
                    socket.Close();

                }
                else
                {
                    pendingIndex += byteSent;
                    sendBytes();
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
                receiveQueueSave.Enqueue(new ReceiveSave { Callback = callback, Payload = payload,Length = length });

                if (!isReceiving)
                {
                    isReceiving = true;
                    ReceiveNewMessage();
                }

            }
        }

        private void ReceiveNewMessage()
        {
            string sb = null;
            
            //if we have messages pending
            while (receiveQueueSave.Count > 0)
            {
                //if we have a string to send.
                if (stringBack.Count > 0)
                {
                    ReceiveSave received = receiveQueueSave.Dequeue();
                    sb = stringBack.Dequeue();
                    /*if (received.Length > 0)
                    {
                        Task.Run(() => received.Callback(sb.Substring(0, received.Length), received.Payload));
                        Console.WriteLine();
                        int rl = received.Length;
                        int sbl = sb.Length;
                        sb = sb.Substring(received.Length + 1, (sb.Length - 1) - received.Length);
                        //cut the string...
                    }
                    else
                    {
                        Task.Run(() => received.Callback(sb, received.Payload));
                    }*/
                    Task.Run(() => received.Callback(sb, received.Payload));

                }
                else
                {
                    break;
                }

            }
            /*if (sb != null)
            {
                stringBack.Enqueue(sb);
            }*/
            //int bytesReceived = 0;
            if (receiveQueueSave.Count > 0)
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
                isReceiving = false;
            }
        }

        private void MessageReceived(IAsyncResult ar)
        {
            lock (recSync)
            {
                // returns the number of bytes received in the previous instance of socket.BeginReceive()
                int bytesReceived = socket.EndReceive(ar);

                //if (length > 0 )only get bytes we want... even if it contains a newline.
                if (receiveQueueSave.Peek().Length > 0)
                {
                    
                   string partialMessage2 = encoding.GetString(incomingBytes, 0, receiveQueueSave.Peek().Length);
                    stringBack.Enqueue(partialMessage2);
                    partialMessage += encoding.GetString(incomingBytes, partialMessage2.Length, bytesReceived- receiveQueueSave.Peek().Length);

                }
                else
                {
                    //grab it all!
                    partialMessage += encoding.GetString(incomingBytes, 0, bytesReceived);
                    // while partialMessage still has a new line
                    while (!(partialMessage.IndexOf("\n").Equals(-1)))
                    {
                        // get the index of the newline
                        int messageLength = partialMessage.IndexOf("\n");

                        // get the completed message and add it to queue
                        stringBack.Enqueue(partialMessage.Substring(0, messageLength));

                        // advance our starting index and remove the message we just found from partialMessage
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
