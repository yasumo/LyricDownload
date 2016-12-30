using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractionSongsUrl
{
    public class ExtractService
    {
        public static List<string> ExtractSongUrls(string target)
        {
            return extract(target, "<td class=\"song_title\"><a href=\"([^\"]*)\">");
        }


        private static List<string> extract(string target, string regexStr)
        {
            var ret = new List<string>();
            Regex r =
                new Regex(regexStr);
            MatchCollection mc = r.Matches(target);

            foreach (Match m in mc)
            {
                ret.Add(m.Groups[1].Value);
            }

            return ret;
        }

    }
}
