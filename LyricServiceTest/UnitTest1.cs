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
        public void CreateSong()
        {
            //i
            var content = HttpRequester.FileGetContent("http://www.kasX-tXme.com/Xtem-65154.html");
            var song = SongInfoExtraction.CreateSong(content);
        }

    }
}
