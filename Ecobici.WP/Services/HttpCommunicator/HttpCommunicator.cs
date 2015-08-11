using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Ecobici.WP.Services.HttpCommunicator
{
    public class HttpCommunicator : IHttpCommunicator
    {
        private WebRequest request;

        public async Task<string> GetRequestAsync(string url)
        {
            request = WebRequest.Create(url);
            request.Method = "GET";
            WebResponse response = await request.GetResponseAsync();
            // Get the stream containing all content returned by the requested server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            return reader.ReadToEnd();
        }
    }
}