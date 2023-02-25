using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace James.Core
{
    public class PrismRegion : ContentControl
    {
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.Register("RegionName", typeof(string), typeof(PrismRegion),
            new PropertyMetadata(RegionNamePropertyChanged));

        public string RegionName
        {
            get => (string)GetValue(RegionNameProperty);
            set => SetValue(RegionNameProperty, value);
        }

        private static void RegionNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is string str
                && string.IsNullOrEmpty(str) == false
                && Application.Current?.CheckAccess() == true)
            {
                IRegionManager rm = RegionManager.GetRegionManager(Application.Current.MainWindow);
                RegionManager.SetRegionName((PrismRegion)d, str);
                RegionManager.SetRegionManager(d, rm);
            }
        }
    }
}
