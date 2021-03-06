﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAsynchroneousServerSocket
{
    class Program
    {
        public class StateObject
        {
            #region Fields
            // Client  socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1024;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();

            #endregion
        }

        public class AsynchronousSocketListener
        {
            #region Fields
            // Thread signal.  
            public static ManualResetEvent allDone = new ManualResetEvent(false);

            #endregion

            #region Constructors
            public AsynchronousSocketListener()
            {
            }

            #endregion

            #region Methods
            /// <summary>
            /// Method, that listens for requests
            /// </summary>
            public static void StartListening()
            {
                // Establish the local endpoint for the socket.  
                // The DNS name of the computer  
                // running the listener is "host.contoso.com".  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.  
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    while (true)
                    {
                        // Set the event to nonsignaled state.  
                        allDone.Reset();

                        // Start an asynchronous socket to listen for connections.  
                        Console.WriteLine("Waiting for a connection...");
                        listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener);

                        // Wait until a connection is made before continuing.  
                        allDone.WaitOne();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();

            }

            /// <summary>
            /// Method, that accepts a callback
            /// </summary>
            /// <param name="ar">IAsyncResult</param>
            public static void AcceptCallback(IAsyncResult ar)
            {
                // Signal the main thread to continue.  
                allDone.Set();

                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }

            /// <summary>
            /// Method, that analyze the request and generate requested data
            /// </summary>
            /// <param name="ar">IAsyncResult</param>
            public static void ReadCallback(IAsyncResult ar)
            {
                String content = String.Empty;
                ClassWebService CWS = new ClassWebService();
                DollarRates CDR = CWS.GetCurrencyFromOpenExchangeRate();


                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                // Read data from the client socket.   
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read   
                    // more data.  
                    content = state.sb.ToString();
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        // All the data has been read from the   
                        // client. Display it on the console.  
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        // Echo requested data back to the client. 
                        if (content == "OE<EOF>")
                        {
                            string response = RateListToString(CDR);
                            Send(handler, response);
                        }
                        else if (content.IndexOf("<EOF>") == 3)
                        {
                            string country = content.Substring(0, 3);
                            string response = CDR.GetRateFromCountryCode(country);
                            Send(handler, response);

                        }
                        // Echo the data back to the client. 
                        else
                        {
                            Send(handler, content);
                        }
                    }
                    else
                    {
                        // Not all data received. Get more.  
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }

            /// <summary>
            /// Method, that converts a Rates Dicttionary into a Semicolon separated string
            /// </summary>
            /// <param name="CDR">DollarRates</param>
            /// <returns>string</returns>
            private static string RateListToString(DollarRates CDR)
            {
                string sdr = "";
                foreach (var item in CDR.rates)
                {
                    sdr += item.Key + ";" + item.Value.ToString() + ";";
                }
                string result = sdr.Remove(sdr.ToString().LastIndexOf(";"), 1);
                return result;
            }

            /// <summary>
            /// Method, that returns requested data
            /// </summary>
            /// <param name="handler">Socket</param>
            /// <param name="data">String</param>
            private static void Send(Socket handler, String data)
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }

            /// <summary>
            /// Method, that sends callback and closes Socket
            /// </summary>
            /// <param name="ar">IAsyncResult</param>
            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    // Retrieve the socket from the state object.  
                    Socket handler = (Socket)ar.AsyncState;

                    // Complete sending the data to the remote device.  
                    int bytesSent = handler.EndSend(ar);
                    Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            #endregion

            /// <summary>
            /// Main Program
            /// </summary>
            /// <param name="args">string[]</param>
            /// <returns>int</returns>
            public static int Main(string[] args)
            {
                StartListening();
                return 0;
            }
        }
    }
}
