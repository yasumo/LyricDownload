using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LyricDownload.model.service
{
    public class WebStringDecoder
    {
        static public string DecodeHtmlCharacterReference(string target) { 
            return  HttpUtility.HtmlDecode(target);
        }
    }
}
