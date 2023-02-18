using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetProperty<T>(ref T oldValue, 
            ref T newValue, 
            [CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if(handler != null) 
            {
                oldValue = newValue;
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<CompanyModel> Items { get; set; }

        private CompanyModel _currentItem;
        public CompanyModel CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, ref value);
        }

        public Window1()
        {
            InitializeComponent();

            Items = GetItems();
            CurrentItem = Items[1];
            DataContext = this;
        }

        private List<CompanyModel> GetItems()
        {
            //string[] source = new string[] { "Microsoft", "Apple" };
            //List<string> source = new();
            //source.Add("Microsoft");
            //source.Add("Apple");
            List<CompanyModel> source = new();
            source.Add(new() { Id = "MSFT", Name = "Microsoft" });
            source.Add(new() { Id = "APPL", Name = "Apple" });
            return source;
        }
    }
}
