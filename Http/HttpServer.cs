using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Threading;

namespace MiniBillingServer.Http
{
    class HttpServer
    {
        private HttpListener m_listener = new HttpListener();
        private Thread m_workthread;

        public HttpListenerPrefixCollection Prefixes
        {
            get
            {
                return m_listener.Prefixes;
            }
        }

        public List<IHttpHandler> Handlers
        {
            get;
            private set;
        }

        public HttpServer()
        {
            Handlers = new List<IHttpHandler>();
        }
        

        private static void worker(object state) {

            HttpServer server = (HttpServer)state;

            Console.WriteLine("Working running");

            while (server.m_listener.IsListening)
            {
                HttpListenerContext context = server.m_listener.GetContext();

                Console.WriteLine("> {0}", context.Request.Url.ToString());

                bool handled = false;
                try
                {
                    foreach (IHttpHandler handler in server.Handlers)
                    {
                        if (handler.Handle(context))
                        {
                            handled = true;
                            break;
                        }
                    }
                }
                catch (AccessDeniedException ex)
                {
                    // Access Denied
                    // Send Status 403
                    Console.WriteLine("[Access-denied] {0} from {1}", ex.Context.Request.Url.ToString(), ex.Context.Request.RemoteEndPoint.ToString());
                    

                    string responseString = "<HTML><BODY>Access denied</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);


                    HttpListenerResponse response = context.Response;

                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    response.StatusCode = 403;
                    response.StatusDescription = "Access denied";
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                    continue;
                }

                // Check is the request was handled
                if (!handled)
                {
                    // Unhandler Request Handler
                    // Send Status 500
                    Console.WriteLine("Unhandled Request!");

                    string responseString = "<HTML><BODY>Unhandled Request</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);


                    HttpListenerResponse response = context.Response;

                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    response.StatusCode = 500;
                    response.StatusDescription = "Internal Server error";
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
            }
        }

        public void Start()
        {
            Console.WriteLine("Start Listening ...");

            m_listener.Start();

            m_workthread = new Thread(new ParameterizedThreadStart(worker));

            m_workthread.Start(this);
        }

        public void Stop()
        {
            m_listener.Stop();
        }
    }
}
