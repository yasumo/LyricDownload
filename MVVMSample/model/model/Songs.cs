using LyricDownload.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricDownload.model.model
{
    class Songs : BindableBase
    {
        private List<Song> songList;
        public List<Song> SongList
        {
            get { return this.songList; }
            set { this.SetProperty(ref this.songList, value); }
        }
        public Songs()
        {
            songList = new List<Song>();
        }
    }
}
