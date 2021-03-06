﻿using LyricDownload.model.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LyricDownload.model.service
{
    public class SongInfoExtracter
    {
        public static Song CreateSong(string contents) {
            contents = deleteTabAndNewLine(contents);
            var title = extractTitle(contents);
            var singer = extractSinger(contents);
            var lyricista = extractLyricista(contents);
            var composer = extractComposer(contents);
            var lyric = extractLyric(contents);
            return new Song {
                Title = title,
                Singer = singer,
                Lyricista = lyricista,
                Composer = composer,
                Lyric = lyric
            };
        }
        private static string deleteTabAndNewLine(string contents)
        {
            return contents.Replace(Constants.Tab, "").Replace(System.Environment.NewLine, "").Replace(Constants.NewLineN,"");
        }

        private static string extractRegex( string target, string regexStr)
        {
            var ret = "";
            Regex r =
                new Regex(regexStr);
            Match m = r.Match(target);

            if (m.Success)
            {
                ret = m.Groups[1].Value;
            }
            else
            {
                ret = "???";
            }
            return ret;
        }

        private static string extractComposer(string contents)
        {
            var ret = extractRegex(contents, @"<tr><th>作曲</th><td><a href=[^>]*>([^<]*)</a>");
            ret = WebStringDecoder.DecodeHtmlCharacterReference(ret)
                .Replace("\"", "&quot;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("&", "&amp;");
            return ret;
        }

        private static string extractLyricista(string contents)
        {
            var ret = extractRegex(contents, @"<tr><th>作詞</th><td><a href=[^>]*>([^<]*)</a>");
            ret = WebStringDecoder.DecodeHtmlCharacterReference(ret);
            return ret;
        }



        private static string extractTitle(string contents)
        {
            var ret = extractRegex(contents,@"<h1>(.*)</h1>");
            ret = WebStringDecoder.DecodeHtmlCharacterReference(ret);
            return ret;
        }

        private static string extractSinger(string contents)
        {
            var ret = extractRegex(contents, @"<tr><th>歌手</th><td><a href=[^>]*>([^<]*)</a>");
            ret = WebStringDecoder.DecodeHtmlCharacterReference(ret);
            return ret;
        }


        private static string extractLyric(string contents)
        {
            var ret = extractRegex(contents, @"var lyrics = '([^']*)'");
            ret = WebStringDecoder.DecodeHtmlCharacterReference(ret).Replace("<br>", Environment.NewLine);
            return ret;
        }



    }
}
