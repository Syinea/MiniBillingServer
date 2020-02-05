using System.Net;
using MiniBillingServer.Http;

namespace MiniBillingServer.Handlers
{
    class ServerStateHandler : FilteredHttpHandler
    {
        public override bool Handle(HttpListenerContext context)
        {
            // Validate Handler
            if (context.Request.Url.LocalPath.ToLower() != "/billing_serverstate.asp")
            {
                return false;
            }

            // Security check
            base.Handle(context);
            

            // Build response
            SendResult(context.Response, "1");

            return true;
        }
    }
}
