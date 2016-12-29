using LyricDownload.util;
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

namespace LyricDownload.viewmodel
{
    class MainWindowViewModel : BindableBase
    {


        #region プロパティ・プライベート変数
        private ObservableCollection<string> songTitleListSource;
        public ObservableCollection<string> SongTitleListSource
        {
            get { return songTitleListSource; }
            set { this.SetProperty(ref this.songTitleListSource, value); }
        }

        private int selectedIndex;
        public int SelectedIndex
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
                var s = (songs.SongList[selectedIndex]);
                Console.WriteLine(s.Lyric);
                Console.WriteLine(s.Lyricista);
            }
        }



        #region コマンド
        private ICommand saveHtmlCommand;

        public ICommand SaveHtmlCommand
        {
            get { return this.saveHtmlCommand ?? (this.saveHtmlCommand = new DelegateCommand(calcSaveHtmlCommand, null)); }
        }
        #endregion




        #region コマンド実メソッド
        private void calcSaveHtmlCommand()
        {
            addSong();
        }

        #endregion

        #region その他プライベートメソッド
        private void addSong()
        {
            var song = new Song();
            song.Title = "aaa";
            song.Singer = "aaa";
            song.Lyricista = "aaa";
            song.Composer = "aaa";
            song.Lyric = "aaa";
            songs.SongList.Add(song);
            SongTitleListSource.Add(song.Title);
            //            syncSongTitleList();
        }
        private void syncSongTitleList() {
            SongTitleListSource = new ObservableCollection<string>((from p in songs.SongList
                                                                    select p.Title).ToList());
        }


        #endregion
    }

}
