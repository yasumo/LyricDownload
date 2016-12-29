using LyricDownload.util;
using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;
using This = LyricDownload.Behaviors.SystemMenuBehavior;



namespace LyricDownload.Behaviors
{
    public class SystemMenuBehavior : Behavior<Window>
    {
        #region Properties
        public bool? IsVisible
        {
            get { return (bool?)this.GetValue(This.IsVisibleProperty); }
            set { this.SetValue(This.IsVisibleProperty, value); }
        }
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof(bool?), typeof(This), new PropertyMetadata(null, This.OnPropertyChanged));


        public bool? CanMinimize
        {
            get { return (bool?)this.GetValue(This.CanMinimizeProperty); }
            set { this.SetValue(This.CanMinimizeProperty, value); }
        }
        public static readonly DependencyProperty CanMinimizeProperty = DependencyProperty.Register("CanMinimize", typeof(bool?), typeof(This), new PropertyMetadata(null, This.OnPropertyChanged));


        public bool? CanMaximize
        {
            get { return (bool?)this.GetValue(This.CanMaximizeProperty); }
            set { this.SetValue(This.CanMaximizeProperty, value); }
        }
        public static readonly DependencyProperty CanMaximizeProperty = DependencyProperty.Register("CanMaximize", typeof(bool?), typeof(This), new PropertyMetadata(null, This.OnPropertyChanged));


        public bool? ShowContextHelp
        {
            get { return (bool?)this.GetValue(This.ShowContextHelpProperty); }
            set { this.SetValue(This.ShowContextHelpProperty, value); }
        }
        public static readonly DependencyProperty ShowContextHelpProperty = DependencyProperty.Register("ShowContextHelp", typeof(bool?), typeof(This), new PropertyMetadata(null, This.OnPropertyChanged));


        public bool EnableAltF4
        {
            get { return (bool)this.GetValue(This.EnableAltF4Property); }
            set { this.SetValue(This.EnableAltF4Property, value); }
        }
        public static readonly DependencyProperty EnableAltF4Property = DependencyProperty.Register("EnableAltF4", typeof(bool), typeof(This), new PropertyMetadata(true));

        public bool EnableClose
        {
            get { return (bool)this.GetValue(This.EnableCloseProperty); }
            set { this.SetValue(This.EnableCloseProperty, value); }
        }
        public static readonly DependencyProperty EnableCloseProperty = DependencyProperty.Register("EnableClose", typeof(bool), typeof(This), new PropertyMetadata(true));

        #endregion





        #region Overrides
        protected override void OnAttached()
        {
            this.AssociatedObject.SourceInitialized += this.OnSourceInitialized;
            base.OnAttached();
        }


        protected override void OnDetaching()
        {
            var source = (HwndSource)HwndSource.FromVisual(this.AssociatedObject);
            source.RemoveHook(this.HookProcedure);
            this.AssociatedObject.SourceInitialized -= this.OnSourceInitialized;
            base.OnDetaching();
        }
        #endregion


        #region Event Handlers
        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var self = obj as SystemMenuBehavior;
            if (self != null)
                self.Apply();
        }


        private void OnSourceInitialized(object sender, EventArgs e)
        {
            this.Apply();
            var source = (HwndSource)HwndSource.FromVisual(this.AssociatedObject);
            source.AddHook(this.HookProcedure);
        }


        private IntPtr HookProcedure(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {


            //--- Alt + F4を無効化
            if (!this.EnableAltF4)
                if (msg == Constant.WM_SYSKEYDOWN)
                    if (wParam.ToInt32() == Constant.VK_F4)
                        handled = true;

            //--- ×ボタン禁止
            if (!this.EnableClose)
                if (msg == Constant.WM_SYSCOMMAND)
                    if (wParam.ToInt32() == Constant.SC_CLOSE)
                        handled = true;


            //--- ok
            return IntPtr.Zero;
        }
        #endregion


        #region Methods
        private void Apply()
        {
            if (this.AssociatedObject == null)
                return;

            //--- スタイル
            var hwnd = new WindowInteropHelper(this.AssociatedObject).Handle;
            var style = User32.GetWindowLong(hwnd, Constant.GWL_STYLE);
            if (this.IsVisible.HasValue)
            {
                if (this.IsVisible.Value) style |= Constant.WS_SYSMENU;
                else style &= ~Constant.WS_SYSMENU;
            }
            if (this.CanMinimize.HasValue)
            {
                if (this.CanMinimize.Value) style |= Constant.WS_MINIMIZEBOX;
                else style &= ~Constant.WS_MINIMIZEBOX;
            }
            if (this.CanMaximize.HasValue)
            {
                if (this.CanMaximize.Value) style |= Constant.WS_MAXIMIZEBOX;
                else style &= ~Constant.WS_MAXIMIZEBOX;
            }
            User32.SetWindowLong(hwnd, Constant.GWL_STYLE, style);

            //--- 拡張スタイル
            var exStyle = User32.GetWindowLong(hwnd, Constant.GWL_EXSTYLE);
            if (this.ShowContextHelp.HasValue)
            {
                if (this.ShowContextHelp.Value) exStyle |= Constant.WS_EX_CONTEXTHELP;
                else exStyle &= ~Constant.WS_EX_CONTEXTHELP;
            }
            User32.SetWindowLong(hwnd, Constant.GWL_EXSTYLE, exStyle);
        }
        #endregion
    }
}