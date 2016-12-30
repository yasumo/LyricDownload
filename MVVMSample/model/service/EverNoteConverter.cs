using LyricDownload.model.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricDownload.model.service
{
    public class EverNoteConverter
    {
        public static string Convert(Song song)
        {
            var retStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + System.Environment.NewLine
                + "<!DOCTYPE en-export SYSTEM \"http://xml.evernote.com/pub/evernote-export2.dtd\">" + System.Environment.NewLine
                + "<en-export application=\"Evernote/Windows\" version=\"6.x\">" + System.Environment.NewLine
                + "<note><title>"
                + song.Title
                +"</title><content><![CDATA[<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + System.Environment.NewLine
                + "<!DOCTYPE en-note SYSTEM \"http://xml.evernote.com/pub/enml2.dtd\">" + System.Environment.NewLine
                + "<en-note>"
                + song.Lyric
                +"</en-note>" + System.Environment.NewLine
                + "]]></content>"
                + "<tag>" + song.Singer + "</tag>"
                + "<tag>" + song.Lyricista + "</tag>"
                + "<tag>" + song.Composer + "</tag>"
                + "</note></en-export>" + System.Environment.NewLine;
            return retStr;
        }
    }
}
