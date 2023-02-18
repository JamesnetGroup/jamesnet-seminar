using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            lb.ItemsSource = GetItems();
        }

        private IEnumerable GetItems()
        {
            //string[] source = new string[] { "Microsoft", "Apple" };
            //List<string> source = new();
            //source.Add("Microsoft");
            //source.Add("Apple");
            List<CompanyModel> source = new();
            source.Add(new() { Name = "Microsoft" });
            source.Add(new() { Name = "Apple" });
            return source;
        }
    }
}
