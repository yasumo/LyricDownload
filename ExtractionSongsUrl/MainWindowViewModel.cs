using ExtractionSongsUrl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace ExtractionSongsUrl
{
    class MainWindowViewModel:BindableBase
    {
        private string html;
        public string Html
        {
            get { return html; }
            set { this.SetProperty(ref this.html, value); }
        }
        private string urls;
        public string Urls
        {
            get { return urls; }
            set { this.SetProperty(ref this.urls, value); }
        }

        private string prefix;
        public string Prefix
        {
            get { return prefix; }
            set { this.SetProperty(ref this.prefix, value); }
        }
        private List<string> urlList;

        public MainWindowViewModel() {
            urlList = new List<string>();
            PropertyChanged += propertyChange;
        }

        private void propertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Prefix"))
            {
                apply();
            }
        }



        private ICommand extractCommand;

        public ICommand ExtractCommand
        {
            get { return this.extractCommand ?? (this.extractCommand = new DelegateCommand(extractCommandExecute, null)); }
        }

        private void extractCommandExecute()
        {
            urlList = ExtractService.ExtractSongUrls(Html);
            apply();
        }
        private void apply() {
            var tmp = "";
            foreach (var u in urlList) {
                tmp += prefix + u + Environment.NewLine;
            }
            Urls = tmp;
        }
    }
}
