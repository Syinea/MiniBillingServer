using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MiniBillingServer.Handlers
{
    class SilkDataCallHandler : Http.FilteredHttpHandler
    {
        private MD5 md5 = MD5.Create();

        private string md5sum(string plain)
        {
            byte[] bPay = Encoding.ASCII.GetBytes(plain);

            byte[] hash = md5.ComputeHash(bPay);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public override bool Handle(HttpListenerContext context)
        {
            // Validate Handler
            if (context.Request.Url.LocalPath.ToLower() != "/billing_silkdatacall.asp")
            {
                return false;
            }

            // Security check
            base.Handle(context);

            int UserJID = 0;

            try
            {
                UserJID = Int32.Parse(context.Request.QueryString["JID"]);

            }
            catch (FormatException ex)
            {
                Console.WriteLine("SilkDataCall: Invalid JID Format");
                SendResult(context.Response, "-2");
                return true;
            }

            Model.SilkData data = Model.SilkDB.Instance.GetSilkData(UserJID);

            string KeyString = "SROG8Z_CDE1210598DK_AKD3HW1K04DL2-";

            string Valid_Key = md5sum(string.Format("{0}.{1}.{2}.{3}.{4}", UserJID, data.SilkOwn, data.SilkGift, data.Mileage, KeyString));

            SendResult(context.Response, string.Format("1:{0},{1},{2},{3}", data.SilkOwn, data.SilkGift, data.Mileage, Valid_Key));
           
            return true;
        }

    }
}
