using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricDownload.model.service
{
    public class HttpRequester
    {
        //ダウンロードしてコンテンツを返すやつ
        public static async Task<string> FileGetContent(string url)
        {
            await Task.Delay(1000);
            var content = "";

            using (StreamReader r = new StreamReader(@"E:\WindowsWorkFiles\Desktop\sample\samplehtml.html"))
            {
                string line;
                while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
                {
                    content += line + System.Environment.NewLine;
                }
            }

            return content;
        }

    }
}
