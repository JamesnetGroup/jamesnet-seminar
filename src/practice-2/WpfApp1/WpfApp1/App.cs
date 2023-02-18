using System.Windows;
using WpfControlLibrary1;

namespace WpfApp1
{
    internal class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window2 win = new();
            win.ShowDialog();
        }
    }
}
