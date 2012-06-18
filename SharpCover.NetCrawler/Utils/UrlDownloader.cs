using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SharpCover.NetCrawler.Utils
{
    public static class UrlDownloader
    {
        public static string Download(Uri url, int timeoutMs = 30000, IDictionary<string, string> httpHeaders = null)
        {
            try {

                System.Net.ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Timeout = timeoutMs;
                httpRequest.AllowAutoRedirect = true;
                httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                httpRequest.CookieContainer = new CookieContainer();

                // append headers
                if (httpHeaders != null) {
                    foreach (string hdr in httpHeaders.Keys) {
                        switch (hdr) {
                            case "Content-Type":
                                httpRequest.ContentType = httpHeaders[hdr];
                                break;
                            default:
                                httpRequest.Headers[hdr] = httpHeaders[hdr];
                                break;
                        }
                    }
                }

                HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());

                //httpHeaders = new Dictionary<string, string>();
                //foreach (string header in response.Headers.AllKeys) {
                //    httpHeaders[header] = response.Headers[header];
                //}

                var strResponse = reader.ReadToEnd();
                response.Close();
                return strResponse;

            } catch (Exception ex) {
                throw new Exception("Error loading stream from " + url + " (inner exception: " + ex.Message + ")", ex);
            }
        }
    }
}
