using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LyricDownload.util;

namespace LyricDownload.model.model
{
    public class Song : BindableBase
    {
        #region プロパティ・プライベート変数
        //タイトル
        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        //歌手
        private string singer;
        public string Singer
        {
            get { return this.singer; }
            set { this.SetProperty(ref this.singer, value); }
        }

        //作詞家
        private string lyricista;
        public string Lyricista
        {
            get { return this.lyricista; }
            set { this.SetProperty(ref this.lyricista, value); }
        }
        //作曲家
        private string composer;
        public string Composer
        {
            get { return this.composer; }
            set { this.SetProperty(ref this.composer, value); }
        }
        //歌詞
        private string lyric;
        public string Lyric
        {
            get { return this.lyric; }
            set { this.SetProperty(ref this.lyric, value); }
        }
        #endregion
    }
}
