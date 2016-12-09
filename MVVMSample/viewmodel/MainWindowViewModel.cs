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
        private string _leftValue;
        public string LeftValue
        {
            get { return _leftValue; }
            set { this.SetProperty(ref this._leftValue, value); }
        }
        private string _rightValue;
        public string RightValue
        {
            get { return _rightValue; }
            set { this.SetProperty(ref this._rightValue, value); }
        }

        private string _answerValue;
        public string AnswerValue
        {
            get { return _answerValue; }
            set { this.SetProperty(ref this._answerValue, value); }
        }


        private string _answerValueReal;
        public string AnswerValueReal
        {
            get { return _answerValueReal; }
            set { this.SetProperty(ref this._answerValueReal, value); }
        }

        public MainWindowViewModel() {
            PropertyChanged += leftChanged;
            PropertyChanged += rightChanged;

        }

        private void leftChanged(object sender, PropertyChangedEventArgs e)
        {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "LeftValue") return;

            // そしてキャスト
            var v = (MainWindowViewModel)sender;

            // 各々の処理
            Console.WriteLine("leftが変更されました: " + v._leftValue);
            CalcExecuteReal();
        }
        private void rightChanged(object sender, PropertyChangedEventArgs e)
        {
            // 文字列でプロパティ名を判別
            if (e.PropertyName != "RightValue") return;

            // そしてキャスト
            var v = (MainWindowViewModel)sender;

            // 各々の処理
            Console.WriteLine("leftが変更されました: " + v._rightValue);
            CalcExecuteReal();
        }


        private ICommand calcCommand;

        public ICommand CalcCommand
        {
            get { return this.calcCommand ?? (this.calcCommand = new DelegateCommand(CalcExecute, CanCalcExecute)); }
        }
        private bool CanCalcExecute()
        {
            return true;
        }

        private void CalcExecute()
        {
            AnswerValue = IntToString(Calculation.Sum(StringToInt(LeftValue), StringToInt(RightValue)));
        }

        private void CalcExecuteReal()
        {
            if (CanCalcExecute()) { 
                AnswerValueReal = IntToString(Calculation.Sum(StringToInt(LeftValue), StringToInt(RightValue)));
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
