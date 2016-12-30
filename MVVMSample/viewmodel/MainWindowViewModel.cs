﻿using LyricDownload.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LyricDownload.model.model;
using System.Windows.Input;
using LyricDownload.model.service;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using LyricDownload.model;

namespace LyricDownload.viewmodel
{
    class MainWindowViewModel : BindableBase
    {


        #region プロパティ・プライベート変数
        private string urls;
        public string Urls
        {
            get { return urls; }
            set { this.SetProperty(ref this.urls, value); }
        }
        private string songInfo;
        public string SongInfo
        {
            get { return songInfo; }
            set { this.SetProperty(ref this.songInfo, value); }
        }
        private string lyric;
        public string Lyric
        {
            get { return lyric; }
            set { this.SetProperty(ref this.lyric, value); }
        }

        private ObservableCollection<string> songTitleListSource;
        public ObservableCollection<string> SongTitleListSource
        {
            get { return songTitleListSource; }
            set { this.SetProperty(ref this.songTitleListSource, value); }
        }

        private int? selectedIndex;
        public int? SelectedIndex
        {
            get { return selectedIndex; }
            set { this.SetProperty(ref this.selectedIndex, value); }
        }

        private Songs songs;





        #endregion


        //コンストラクタ
        public MainWindowViewModel()
        {
            songs = new Songs();
            songTitleListSource = new ObservableCollection<string>();
            PropertyChanged += hoge;
        }

        private void hoge(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedIndex")) {
                var s = (songs.SongList[selectedIndex.Value]);
                Lyric = s.Lyric;
                SongInfo = "タイトル：" + s.Title + System.Environment.NewLine
                    + "歌手：" + s.Singer + Constants.Tab + "作詞：" + s.Lyricista + Constants.Tab + "作曲：" + s.Composer;
            }
        }



        #region コマンド
        private ICommand saveEverNoteCommand;

        public ICommand SaveEverNoteCommand
        {
            get { return this.saveEverNoteCommand ?? (this.saveEverNoteCommand = new DelegateCommand(calcSaveEverNoteCommand, null)); }
        }
        private ICommand downloadCommand;

        public ICommand DownloadCommand
        {
            get { return this.downloadCommand ?? (this.downloadCommand = new DelegateCommand(calcDownloadCommand, null)); }
        }
        #endregion

        


        #region コマンド実メソッド
        private void calcSaveEverNoteCommand()
        {
            //songsをevernoteに保存するやつ
        }

        private async void calcDownloadCommand()
        {
            for (int i = 0; i < 10; i++)
            {
                var content = await HttpRequester.FileGetContent("http:hogehoge");
                var song = SongInfoExtraction.CreateSong(content);
                addSong(song);
            }
        }



        #endregion

        #region その他プライベートメソッド
        //曲を追加する
        private void addSong(Song song)
        {
            songs.SongList.Add(song);
            SongTitleListSource.Add(song.Title);
        }

        #endregion
    }

}
