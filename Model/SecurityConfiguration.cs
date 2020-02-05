using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MiniBillingServer.Model
{
    class SecurityConfiguration : Configuration
    {
        /// <summary>
        /// A list of all allowed hostnames
        /// </summary>
        public List<string> Allowed_Hosts
        {
            get;
            protected set;
        }

        /// <summary>
        /// A list of all allowed ips
        /// </summary>
        public List<IPAddress> Allowed_IPs
        {
            get;
            protected set;
        }

        public SecurityConfiguration(string filename)
            : base(filename)
        {

        }

        protected override void LoadConfiguration()
        {
            // Read allowed hostnames
            string[] tmp_AllowedHosts = IniReadValue("SECURITY", "Allowed_Hosts").Split(',');
            Allowed_Hosts = new List<string>();
            Allowed_Hosts.AddRange(tmp_AllowedHosts);

            // Read allowed ips
            string[] tmp_AllowedIPs = IniReadValue("SECURITY", "Allowed_IPs").Split(',');
            Allowed_IPs = new List<IPAddress>();
            foreach (string address in tmp_AllowedIPs)
            {
                try
                {
                    Allowed_IPs.Add(IPAddress.Parse(address));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("[Config Parse Error] {0} (\"{1}\")", ex.Message, address);
                }
            }

        }
    }
}
