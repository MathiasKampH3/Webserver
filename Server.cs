using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Webserver
{
    public class Server
    {
        // property for default encoding
        private readonly Encoding CharEncoder = Encoding.UTF8;
        // property to be used as the webservers socket
        private Socket serverSocket;
        // property to hold the html page file path
        private string contentPath;
        // property to determine the timeout seconds
        private int timeOut = 8;
        // property to determine if the webserver is running
        private bool isRunning = false;

        // dictionary to hold the Content types that are supported by the webserver
        private readonly Dictionary<string, string> extensions = new Dictionary<string, string>()
        {
            //{ "extension", "content type" }
            {"htm", "text/html"},
            {"html", "text/html"},
            {"xml", "text/xml"},
            {"txt", "text/plain"},
            {"css", "text/css"},
            {"png", "image/png"},
            {"gif", "image/gif"},
            {"jpg", "image/jpg"},
            {"jpeg", "image/jpeg"},
            {"zip", "application/zip"}
        };

        /// <summary>
        /// Method HandleTheRequest has the purpose of handling the request, when a client is trying to connect to the webserver
        /// </summary>
        /// <param name="clientSocket"></param>
        private void HandleTheRequest(Socket clientSocket)
        {
            // buffer array of 10kb
            byte[] buffer = new byte[10240];
            // receiving the request from the client
            int receivedBufferCount = clientSocket.Receive(buffer);
            // contains the full request in a string format
            string strReceived = CharEncoder.GetString(buffer, 0, receivedBufferCount);
            // contains the http method. eg. (GET, POST)
            string httpMethod = strReceived.Substring(0, strReceived.IndexOf(" "));
            // contains the length of characters until the "/"
            int start = strReceived.IndexOf(httpMethod) + httpMethod.Length + 1;
            // contains the length of characters from the received request till HTTP minus the start 
            int length = strReceived.LastIndexOf("HTTP") - start - 1;
            // contains only the url of the webpage eg.(/helloWorld.html)
            string requestedUrl = strReceived.Substring(start, length);

            string requestedFile = null;

            if (httpMethod.Equals("GET") || httpMethod.Equals("POST"))
            {
                // checks if there is any parameters in the url 
                requestedFile = requestedUrl.Split('?')[0];
            }
            else
            {
                // return notImplemented error
                NotImplemented(clientSocket);
                return;
            }

            // replaces forward slash with backslash
            requestedFile = requestedFile.Replace("/", @"\").Replace("\\..", "");
            // returns the length of the url without extenstion 
            start = requestedFile.LastIndexOf('.') + 1;

            if (start > 0)
            {
                length = requestedFile.Length - start;

                // returns the extension of the webpage file eg. (helloWorld.html) 
                string extension = requestedFile.Substring(start, length);

                // check if the extension is a valid extension from the dictionary 
                if (extensions.ContainsKey(extension))
                {
                    // checks if the webpage file exists
                    if (File.Exists(contentPath + requestedFile))
                    {
                        // sends an ok answer back to the client
                        SendOkResponse(clientSocket,
                            File.ReadAllBytes(contentPath + requestedFile), extensions[extension]);
                    }

                    else
                    {
                        // send a fileNotFound error back to the client
                        NotFound(clientSocket);
                    }
                }
            }

            // return a default page called index.htm
            else
            {
                
                if (requestedFile.Substring(length - 1, 1) != @"\")
                {
                    requestedFile += @"\";
                }

                if (File.Exists(contentPath + requestedFile + "index.htm"))
                {
                    SendOkResponse(clientSocket,
                        File.ReadAllBytes(contentPath + requestedFile + "\\index.htm"), "text/html");
                }

                else if (File.Exists(contentPath + requestedFile + "index.html"))
                {
                    SendOkResponse(clientSocket,
                        File.ReadAllBytes(contentPath + requestedFile + "\\index.html"), "text/html");
                }

                else
                {
                    NotFound(clientSocket);
                }
            }
        }

        /// <summary>
        /// method NotImplemented is being called whenever there is a 501 error
        /// </summary>
        /// <param name="clientSocket"></param>
        private void NotImplemented(Socket clientSocket)
        {
            SendResponse(clientSocket, "Not implemented", "501", "text/html");
        }
        /// <summary>
        /// Method NotFound is being called if the webpage is not found
        /// </summary>
        /// <param name="clientSocket"></param>
        private void NotFound(Socket clientSocket)
        {
            SendResponse(clientSocket, "Not found", "404", "text/html");
        }

        /// <summary>
        /// SendOkResponse is being called if the request was successfull
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="bContent"></param>
        /// <param name="contentType"></param>
        private void SendOkResponse(Socket clientSocket, byte[] bContent, string contentType)
        {
            SendResponse(clientSocket, bContent, "200 OK", contentType);
        }


        /// <summary>
        /// method to send a response with strings 
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="strContent"></param>
        /// <param name="responseCode"></param>
        /// <param name="contentType"></param>
        private void SendResponse(Socket clientSocket, string strContent, string responseCode,
            string contentType)
        {
            byte[] bContent = CharEncoder.GetBytes(strContent);
            SendResponse(clientSocket, bContent, responseCode, contentType);
        }

        /// <summary>
        /// method to send a response with byte array to be used by the method above
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="bContent"></param>
        /// <param name="responseCode"></param>
        /// <param name="contentType"></param>
        private void SendResponse(Socket clientSocket, byte[] bContent, string responseCode,
            string contentType)
        {
            try
            {
                byte[] bHeader = CharEncoder.GetBytes(
                    "HTTP/1.1 " + responseCode + "\r\n"
                    + "Server: Mathias's webserver\r\n"
                    + "Content-Length: " + bContent.Length.ToString() + "\r\n"
                    + "Connection: close\r\n"
                    + "Content-Type: " + contentType + "\r\n\r\n");
                clientSocket.Send(bHeader);
                clientSocket.Send(bContent);
                clientSocket.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Method start has the purpose of starting the webserver, based on ipaddress, port, max connections, and contentPath
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="maxNOfCon"></param>
        /// <param name="contentPath"></param>
        /// <returns></returns>
        public bool Start(IPAddress ipAddress, int port, int maxNOfCon, string contentPath)
        {
            if (isRunning) return false;
            {
                try
                {
                    // create a new socket for ipv4 and tcpIp protocol 
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // binds the ipaddress and port to the socket
                    serverSocket.Bind(new IPEndPoint(ipAddress, port));
                    // sets the maximum number of connections to the socket
                    serverSocket.Listen(maxNOfCon);
                    // timeout in milliseconds
                    serverSocket.ReceiveTimeout = timeOut;
                    // timeout in milliseconds
                    serverSocket.SendTimeout = timeOut;
                    // sets the value isRunning to true to indicate that the webserver is up an running correctly
                    isRunning = true;
                    // sets the contentPath
                    this.contentPath = contentPath;
                }
                catch
                {
                    return false;
                }

                // this thread will listen for client requests
                Thread requestListener = new Thread(() =>
                {
                    // this part of the code runs while the server is running
                    while (isRunning)
                    {
                        // create a clientSocket
                        Socket clientSocket;
                        try
                        {
                            // server accept the client socket
                            clientSocket = serverSocket.Accept();

                            // new thread to handle the requests from clients
                            Thread requestHandler = new Thread(() =>
                            {
                                // set the timeout 
                                clientSocket.ReceiveTimeout = timeOut;
                                clientSocket.SendTimeout = timeOut;
                                try
                                {
                                    //  run HandleTheRequest method on the clients socket
                                    HandleTheRequest(clientSocket);
                                }
                                catch
                                {
                                    try
                                    {
                                        // close the socket again
                                        clientSocket.Close();
                                    }
                                    catch
                                    {
                                    }
                                }
                            });

                            // start the request handler thread
                            requestHandler.Start();
                        }
                        catch
                        {
                        }
                    }
                });
                // start the listener thread
                requestListener.Start();
                return true;
            }
        }

        /// <summary>
        /// Method Stop, closes the server socket
        /// </summary>
        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                try
                {
                    serverSocket.Close();
                }
                catch
                {
                }

                serverSocket = null;
            }
        }
    }
}