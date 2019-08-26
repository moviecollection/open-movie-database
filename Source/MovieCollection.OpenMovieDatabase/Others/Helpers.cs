using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace MovieCollection.OpenMovieDatabase
{
    internal static class Helpers
    {
        internal static async Task<string> DownloadJsonAsync(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            Debug.WriteLine(format: "Sending request to: {0}", url);

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}
