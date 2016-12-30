using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LyricDownload.model.service;


namespace LyricServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 数値文字変換()
        {
            Assert.AreEqual("隠して→開いて→隠して<br>まだこれは", DecodeWebString.DecodeHtmlCharacterReference("&#38560;&#12375;&#12390;&rarr;&#38283;&#12356;&#12390;&rarr;&#38560;&#12375;&#12390;<br>&#12414;&#12384;&#12371;&#12428;&#12399;"));
        }

        [TestMethod]
        public void FileGetContentTest()
        {
           var hoge = HttpRequester.FileGetContent("http:hogehoge").Result;
           Console.WriteLine(hoge);
        }

        [TestMethod]
        public void CreateSong()
        {
            var content = HttpRequester.FileGetContent("http:hogehoge").Result;
            var song = SongInfoExtraction.CreateSong(content);
        }
    }
}
