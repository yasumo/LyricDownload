using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricDownload.model.service
{
    public class FileCreater
    {
        public static void SaveFile(string output,string filePath, string encoding = "shift_jis") {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, Encoding.GetEncoding(encoding));
            //TextBox1.Textの内容を書き込む
            sw.Write(output);
            //閉じる
            sw.Close();
        }
    }
}
