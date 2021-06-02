using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AAUtil.Sample.Utils
{
    class HttpUtils
    {
        public static void PostRequest()
        {
            try
            {
                InnerPostRequest();
            }
            catch (WebException wex)
            {
                ProcessWebException(wex);
            }
        }

        private static string ReadResponseContent(WebResponse response)
        {
            if (response == null)
            {
                return null;
            }

            using (var respStream = response.GetResponseStream())
            using (var reader = new StreamReader(respStream))
            {
                return reader.ReadToEnd();
            }
        }

        private static void ProcessWebException(WebException wex)
        {
            // If you reach this point, an exception has been caught.  
            Console.WriteLine("A WebException has been caught.");
            // Write out the WebException message.  
            Console.WriteLine(wex.ToString());
            // Get the WebException status code.  
            WebExceptionStatus status = wex.Status;
            // If status is WebExceptionStatus.ProtocolError,
            //   there has been a protocol error and a WebResponse
            //   should exist. Display the protocol error.  
            if (status == WebExceptionStatus.ProtocolError)
            {
                Console.Write("The server returned protocol error ");
                // Get HttpWebResponse so that you can check the HTTP status code.  
                HttpWebResponse httpResponse = (HttpWebResponse)wex.Response;

                Console.WriteLine((int)httpResponse.StatusCode + " - " + httpResponse.StatusCode);

                var respContent = ReadResponseContent(wex.Response);

                Console.WriteLine("服务端返回内容:" + respContent);
            }
        }

        private static void InnerPostRequest()
        {
            // Create a request using a URL that can receive a post.
            var request = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ") as HttpWebRequest;
            // Set the Method property of the request to POST.
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";

            //阻止302跳转
            request.AllowAutoRedirect = false;

            // Create POST data and convert it to a byte array.
            string postData = "mm=1&&nn=2";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            // Close the response.
            response.Close();
        }
    }
}
