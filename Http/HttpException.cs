using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace MiniBillingServer.Http
{
    class HttpException : Exception
    {
        public HttpListenerContext Context
        {
            get;
            protected set;
        }

        public HttpException(string message)
            : base(message)
        {
            
        }

        public HttpException(string message, HttpListenerContext context)
            : base(message)
        {
            this.Context = context;
        }


    }
}
