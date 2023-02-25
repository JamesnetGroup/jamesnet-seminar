using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;

namespace James.Core
{
    public class PrismContent : ContentControl
    {
        public PrismContent()
        {
            ViewModelLocationProvider.AutoWireViewModelChanged(this, AutoWireViewModelChanged);
        }

        private void AutoWireViewModelChanged(object arg1, object arg2)
        {
            if(arg1 is FrameworkElement fe)
            {
                fe.DataContext = arg2;
            }
        }
    }
}
