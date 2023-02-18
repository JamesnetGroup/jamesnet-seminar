using System.Windows;

namespace WpfApp1
{
    internal class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window1 win = new();
            win.ShowDialog();
        }
    }
}
