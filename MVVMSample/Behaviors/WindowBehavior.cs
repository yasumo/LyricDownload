using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using This = LyricDownload.Behaviors.WindowBehavior;

namespace LyricDownload.Behaviors
{
    class WindowBehavior : Behavior<Window>
    {
        public bool? IsVisible
        {
            get { return (bool?)this.GetValue(This.IsVisibleProperty); }
            set { this.SetValue(This.IsVisibleProperty, value); }
        }
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof(bool?), typeof(This), new PropertyMetadata(null, This.OnPropertyChanged));


        private void Apply()
        {
            if (this.AssociatedObject == null)
                return;
            this.AssociatedObject.ShowInTaskbar = IsVisible.Value;
        }

        #region Overrides
        protected override void OnAttached()
        {
            this.AssociatedObject.SourceInitialized += this.OnSourceInitialized;
            base.OnAttached();
        }


        protected override void OnDetaching()
        {
            this.AssociatedObject.SourceInitialized -= this.OnSourceInitialized;
            base.OnDetaching();
        }
        #endregion

        #region Event Handlers

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            this.Apply();
        }
        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var self = obj as WindowBehavior;
            if (self != null)
                self.Apply();
        }
        #endregion
    }
}
