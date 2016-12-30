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
using LyricDownload.model;
using System.IO;

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

        private string saveDir;
        public string SaveDir
        {
            get { return saveDir; }
            set { this.SetProperty(ref this.saveDir, value); }
        }

        

        private Songs songs;
        private bool canSave;
        #endregion


        //コンストラクタ
        public MainWindowViewModel()
        {
            songs = new Songs();
            songTitleListSource = new ObservableCollection<string>();
            PropertyChanged += propertyChange;
            SaveDir = @"D:\tmp";
            canSave = false;
        }


        #region ハンドラ
        private void propertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedIndex"))
            {
                if (selectedIndex == null)
                {
                    Lyric = null;
                    SongInfo = null;
                    return;
                }
                var s = (songs.SongList[selectedIndex.Value]);
                Lyric = s.Lyric.Replace(Constants.HtmlBR, System.Environment.NewLine);
                SongInfo = "タイトル：" + s.Title + System.Environment.NewLine
                    + "歌手：" + s.Singer + Constants.Tab + "作詞：" + s.Lyricista + Constants.Tab + "作曲：" + s.Composer;
            }
        }
        #endregion

        #region コマンド
        private ICommand saveEverNoteCommand;

        public ICommand SaveEverNoteCommand
        {
            get { return this.saveEverNoteCommand ?? (this.saveEverNoteCommand = new DelegateCommand(saveEverNoteCommandExecute, canSaveEverNote)); }
        }

        private ICommand downloadCommand;

        public ICommand DownloadCommand
        {
            get { return this.downloadCommand ?? (this.downloadCommand = new DelegateCommand(downloadCommandExecute, null)); }
        }
        private ICommand clearCommand;

        public ICommand ClearCommand
        {
            get { return this.clearCommand ?? (this.clearCommand = new DelegateCommand(clearCommandExecute, null)); }
        }
        #endregion

        #region コマンド実メソッド
        private void clearCommandExecute() {
            clear();
        }
        private void saveEverNoteCommandExecute()
        {
            int num = 0;
            foreach(var s in songs.SongList){
                var evernote = EverNoteConverter.Convert(s);
                FileCreater.SaveFile(evernote, saveDir + Path.DirectorySeparatorChar + num + ".enex");
                num++;
            }
        }

        private async void downloadCommandExecute()
        {
            var strReader = new StringReader(Urls);
            var urlList = new List<string>();
            while (true)
            {
                var line = strReader.ReadLine();
                if (line != null)
                {
                    urlList.Add(line);
                }
                else
                {
                    break;
                }
            }


            foreach (var u in urlList)
            {
                //連続でダウンロードすると怪しまれると思うので待機
                var cRandom = new System.Random();
                int random = cRandom.Next(1000, 3000);
                await Task.Delay(random);

                //実際のダウンロード
                var content = HttpRequester.FileGetContent(u);
                var song = SongInfoExtracter.CreateSong(content);
                addSong(song);

            }
            canSave = true;

        }



        #endregion

        #region その他プライベートメソッド
        //曲を追加する
        private void addSong(Song song)
        {
            songs.SongList.Add(song);
            SongTitleListSource.Add(song.Title);
        }
        private void clear()
        {
            SelectedIndex = null;
            songs = new Songs();
            Urls = "";
            SongTitleListSource = new ObservableCollection<string>();
            canSave = false;

        }

        private bool canSaveEverNote()
        {
            return canSave;
        }

        #endregion
    }

}
