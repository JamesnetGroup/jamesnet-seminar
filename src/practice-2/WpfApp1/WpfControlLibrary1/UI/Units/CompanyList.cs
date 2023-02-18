using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary1.UI.Units
{
    public class CompanyList : ListBox
    {
        public static readonly DependencyProperty SelectionCommandProperty = 
            DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(CompanyList));
    
        public ICommand SelectionCommand
        {
            get { return (ICommand)this.GetValue(SelectionCommandProperty); }
            set { this.SetValue(SelectionCommandProperty, value); }
        }

        static CompanyList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CompanyList), new FrameworkPropertyMetadata(typeof(CompanyList)));
        }

        public CompanyList()
        {
            PreviewMouseLeftButtonDown += CompanyList_MouseLeftButtonDown;
        }

        private void CompanyList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectionCommand.Execute(SelectedItem);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CompanyListItem();
        }
    }
}
