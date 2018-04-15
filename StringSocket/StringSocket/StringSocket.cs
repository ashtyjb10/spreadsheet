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
        private bool isRecieving;

        private Queue<SendCallback> sendCallbackQueue;
        private Queue<Object> sendCallbackPayloadQueue;
        private Queue<ReceiveCallback> receiveCallbackQueue;
        private Queue<Object> recieveCallbackPayloadQueue;

        private Queue<SendSave> sendSaveQueue;
        private Queue<ReceiveSave> receiveQueueSave;

        private int sentBytes;
       

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


            sendSaveQueue = new Queue<SendSave>();
            receiveQueueSave = new Queue<ReceiveSave>();


            decoder = e.GetDecoder();

            sendSaveQueue = new Queue<SendSave>();
            recieveQueueSave = new Queue<RecieveSave>();
            

            incoming = new StringBuilder();
            outgoing = new StringBuilder();
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
            public string SentMessage { get; set; }
            public object Payload { get; set; }
            public SendCallback Callback { get; set; }
        }

        private struct ReceiveSave
        {

            public object Payload { get; set; }
            public ReceiveCallback Callback { get; set; }

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

            lock (sendSaveQueue)
            {
                
                sendSaveQueue.Enqueue(new SendSave { SentMessage = s, Callback = callback, Payload = payload });

                if (!sendIsOngoing)
                {
                    
                    sendIsOngoing = true;
                    SendBytes();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void SendBytes()
        {
            sentBytes = 0;
            pendingBytes = encoding.GetBytes(sendSaveQueue.Peek().SentMessage);
            try
            {
                socket.BeginSend(pendingBytes, 0, pendingBytes.Length, SocketFlags.None, MessageSent, null);
            }
            catch(Exception)
            {
                sendSaveQueue.Dequeue();
            }
        }

        private void MessageSent(IAsyncResult result)
        {
            
            lock (sendSaveQueue)
            {
                sentBytes = sentBytes + socket.EndSend(result);

                int leftToSend = pendingBytes.Length - sentBytes;

                if (leftToSend > 0)
                {
                    socket.BeginSend(pendingBytes, sentBytes, leftToSend, SocketFlags.None, MessageSent, null);
                }
                else
                {
                    //Dequeue the sendSave
                    SendSave toCall = sendSaveQueue.Dequeue();

                    //Create a new task for the callback to ensure non-blocking.
                    Task callbackTask = new Task(() => toCall.Callback.Invoke(true, toCall.Payload));
                    callbackTask.Start();
                    

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
                recieveQueueSave.Enqueue(new RecieveSave { Callback = callback, Payload = payload });

                if (!isRecieving)
                {
                    isRecieving = true;
                    RecieveNewMessage();
                    isRecieving = false;
                }

            }
        }

        private void RecieveNewMessage()
        {
            //if we have messages pending
            while (recieveQueueSave.Count > 0)
            {
                //if we have a string to send.
                if (incoming.Length > 0)
                {
                    RecieveSave recieved = recieveQueueSave.Dequeue();
                    recieved.Callback(incoming.ToString(), recieved.Payload);
                    //ThreadPool.QueueUserWorkItem(o => recieved.Callback(incoming.ToString(), recieved.Payload));
                }
                else
                {
                    break;
                }

            }

            if (recieveQueueSave.Count > 0)
            {
                try
                {
                    socket.BeginReceive(incomingBytes, 0, incomingBytes.Length, SocketFlags.None,
                            MessageReceived, null);

  
                }
                catch (ObjectDisposedException)
                {
                    recieveQueueSave.Dequeue();
                }
            }
            else
            {
                isRecieving = false;
            }

        }

        private void MessageReceived(IAsyncResult result)
        {
            lock (recSync)
            {
                int bytesRead = socket.EndReceive(result);

                int charsRead = decoder.GetChars(incomingBytes, 0, bytesRead, incomingChars, 0, false);
                incoming.Append(incomingChars, 0, charsRead);
                for (int index = 0; index < incoming.Length; index++)
                {

                    if (incoming[index] == '\n')
                    { 
                        incoming.Remove(incoming.Length - 1, 1);
                    }


                }
                RecieveNewMessage();

            /* // Figure out how many bytes have come in
             int bytesRead = socket.EndReceive(result);



                if (bytesRead == 0)
                {
                    Console.WriteLine("Socket closed");
                    ReceiveCallback rec = receiveCallbackQueue.Dequeue(); 
                    object pay = recieveCallbackPayloadQueue.Dequeue();

                 rec(incoming.ToString(), pay);
                 socket.Close();
             }
             else
             {

                 int charsRead = decoder.GetChars(incomingBytes, 0, bytesRead, incomingChars, 0, false);
                 incoming.Append(incomingChars, 0, charsRead);
                 for (int index = 0; index < incoming.Length; index++)
                 {

                     if (incoming[index] == '\n')
                     {

                         ReceiveCallback rec = receiveCallbackQueue.Dequeue();
                         object pay = recieveCallbackPayloadQueue.Dequeue();

                         incoming.Remove(incoming.Length - 1, 1);
                         rec(incoming.ToString(), pay);
                         incoming.Clear();
                     }
                 }
                 try
                 {
                     socket.BeginReceive(incomingBytes, 0, incomingBytes.Length, SocketFlags.None,
                         MessageReceived, null);
                 }
                 catch (ObjectDisposedException)
                 { }*/

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
