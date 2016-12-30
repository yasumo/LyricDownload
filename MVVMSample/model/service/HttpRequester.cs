using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LyricDownload.model.service
{
    public class HttpRequester
    {
        //ダウンロードしてコンテンツを返すやつ
        public static HttpWebResponse GetRequest(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.<OS build number>";
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            return res;
        }

        public static string ExtractContents(HttpWebResponse res) {
            Stream s = res.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            
            return sr.ReadToEnd();
        }

        public static string FileGetContent(string url) {
            return ExtractContents(GetRequest(url));
        }



    }
}
