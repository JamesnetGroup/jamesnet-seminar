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

        }
    }
}
