using JamesStduio.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamesStduio
{
    internal class Starter
    {
        [STAThread] 
        public static void Main() 
        {
            _ = new App()
                .WireViewModel()
                .AddModule<DirectModules>()
                .Run();
        }
    }
}
