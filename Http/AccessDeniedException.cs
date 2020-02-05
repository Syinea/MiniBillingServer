using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MiniBillingServer.Http
{
    class AccessDeniedException : HttpException
    {
        public AccessDeniedException(string message)
            : base(message)
        {

        }

        public AccessDeniedException(string message, HttpListenerContext context)
            : base(message, context)
        {
        }
    }
}
