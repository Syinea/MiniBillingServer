using System.Collections.Generic;
using System.Net;

namespace MiniBillingServer.Http
{
    abstract class FilteredHttpHandler : IHttpHandler
    {
        protected Model.SecurityConfiguration m_securityConfig;

        public FilteredHttpHandler()
        {
            m_securityConfig = new Model.SecurityConfiguration("Settings/config.ini");
        }

        public override bool Handle(System.Net.HttpListenerContext context)
        {
            IPAddress clientIP = context.Request.RemoteEndPoint.Address;

            List<string> HostIP = new List<string>();
            foreach (string AuthorizedHost in m_securityConfig.Allowed_Hosts)
            {
                HostIP.Add(Dns.GetHostAddresses(AuthorizedHost)[0].ToString());
            }

            if (!(m_securityConfig.Allowed_IPs.Contains(clientIP) || HostIP.Contains(clientIP.ToString())))
            {
                throw new AccessDeniedException("Access to the resource was denied", context);
            }

            return true;
        }
    }
}
