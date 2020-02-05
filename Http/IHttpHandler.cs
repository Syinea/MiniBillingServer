
namespace MiniBillingServer.Http
{
    abstract class IHttpHandler
    {
        public abstract bool Handle(System.Net.HttpListenerContext context);

        protected void SendResult(System.Net.HttpListenerResponse response, string responseString)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;

            System.IO.Stream output = response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
