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

namespace LyricDownload.viewmodel
{
    class MainWindowViewModel : BindableBase
    {

        //AさんとBさんの年齢を計算するやつ
        private Person pA=new Person();
        private Person pB=new Person();

        //Aさん用のフィールド
        public int? LeftValue
        {
            get { return pA.Age; }
            set { pA.Age=value; }
        }

        //Bさん用のフィールド
        public int? RightValue
        {
            get { return pB.Age; }
            set { pB.Age=value; }
        }

        //年齢の合計(ボタンが押されてから計算)
        private string sumAge;
        public string SumAge
        {
            get { return sumAge; }
            set { this.SetProperty(ref this.sumAge, value); }
        }

        //年齢の合計(すぐに計算)
        private string sumAgeReal;
        public string SumAgeReal
        {
            get { return sumAgeReal; }
            set { this.SetProperty(ref this.sumAgeReal, value); }
        }

        //コンストラクタ
        public MainWindowViewModel() {

            pA.PropertyChanged += pAAgeChanged;
            pB.PropertyChanged += pBAgeChanged;
        }

        private void pAAgeChanged(object sender, PropertyChangedEventArgs e) {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "Age") return;

            // 変更のあったものをキャスト
            var v = (Person)sender;

            // 各々の処理
            Console.WriteLine("paが変更されました: " + v.Age);
            CalcExecuteReal();
        }

        private void pBAgeChanged(object sender, PropertyChangedEventArgs e)
        {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "Age") return;

            // 変更のあったものをキャスト
            var v = (Person)sender;

            // 各々の処理
            Console.WriteLine("pbが変更されました: " + v.Age);
            CalcExecuteReal();
        }



        private ICommand calcCommand;

        public ICommand CalcCommand
        {
            get { return this.calcCommand ?? (this.calcCommand = new DelegateCommand(CalcExecute, CanCalcExecute)); }
        }


        private ICommand hiddenTaskBarCommand;
        public ICommand HiddenTaskBarCommand
        {
            get { return this.hiddenTaskBarCommand ?? (this.hiddenTaskBarCommand = new DelegateCommand(HiddenTaskBarExecute)); }
        }

        private bool CanCalcExecute()
        {
            //バリデートするならココでやる、falseを返すと非活性になる
            return true;
        }

        private bool CanHiddenTaskBarExecute() {
            return true;
        }

        //ボタンが押されたときに計算する方
        private void CalcExecute()
        {
            SumAge = IntToString(Calculation.Sum(pA.Age, pB.Age));
        }

        //タスクバーを隠すコマンド
        private void HiddenTaskBarExecute(object x)
        {
            if (x != null)
            {
                var window = (Window)x;
                window.ShowInTaskbar = !window.ShowInTaskbar;
//                SystemCommands.CloseWindow(window);
            }
        }

        //値の変動があったときにすぐに計算する方
        private void CalcExecuteReal()
        {
            if (CanCalcExecute()) { 
                SumAgeReal = IntToString(Calculation.Sum(pA.Age, pB.Age));
            }
        }

        private int StringToInt(string src)
        {
            int ret = 0;
            if (int.TryParse(src, out ret))
            {
                return ret;
            }
            return 0;
        }

        private string IntToString(int src)
        {
            return src.ToString();
        }
    }
}
