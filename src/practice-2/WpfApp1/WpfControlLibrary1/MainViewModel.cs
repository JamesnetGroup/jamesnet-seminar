using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfControlLibrary1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetProperty<T>(ref T oldValue,
            T newValue,
            [CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if (handler != null)
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
            set => SetProperty(ref _currentItem, value);
        }

        public MainViewModel()
        {
            Items = GetItems();
            CurrentItem = Items[1];
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
