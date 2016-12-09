using MVVMSample.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMSample.model.model;
using System.Windows.Input;
using MVVMSample.model.service;
using System.ComponentModel;

namespace MVVMSample.viewmodel
{
    class MainWindowViewModel : BindableBase
    {

        //AさんとBさんの年齢を計算するやつ
        private Person pA=new Person();
        private Person pB=new Person();

        //Aさん用のフィールド
        private string leftValue;
        public string LeftValue
        {
            get { return leftValue; }
            set { this.SetProperty(ref this.leftValue, value); }
        }

        //Bさん用のフィールド
        private string rightValue;
        public string RightValue
        {
            get { return rightValue; }
            set { this.SetProperty(ref this.rightValue, value); }
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
            PropertyChanged += leftChanged;
            PropertyChanged += rightChanged;

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
            LeftValue = v.Age.ToString();
        }

        private void pBAgeChanged(object sender, PropertyChangedEventArgs e)
        {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "Age") return;

            // 変更のあったものをキャスト
            var v = (Person)sender;

            // 各々の処理
            Console.WriteLine("pbが変更されました: " + v.Age);
            RightValue = v.Age.ToString();
        }

        private void leftChanged(object sender, PropertyChangedEventArgs e)
        {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "LeftValue") return;

            // 変更のあったものをキャスト
            var v = (MainWindowViewModel)sender;

            // 各々の処理
            Console.WriteLine("leftが変更されました: " + v.leftValue);
            pA.Age = StringToInt(LeftValue);
            CalcExecuteReal();
        }
        private void rightChanged(object sender, PropertyChangedEventArgs e)
        {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "RightValue") return;

            // 変更のあったものをキャスト
            var v = (MainWindowViewModel)sender;

            // 各々の処理
            Console.WriteLine("rightが変更されました: " + v.rightValue);
            pB.Age = StringToInt(RightValue);
            CalcExecuteReal();
        }


        private ICommand calcCommand;

        public ICommand CalcCommand
        {
            get { return this.calcCommand ?? (this.calcCommand = new DelegateCommand(CalcExecute, CanCalcExecute)); }
        }
        private bool CanCalcExecute()
        {
            //バリデートするならココでやる
            return true;
        }

        //ボタンが押されたときに計算する方
        private void CalcExecute()
        {
            SumAge = IntToString(Calculation.Sum(pA.Age, pB.Age));
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
