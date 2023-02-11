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

namespace FormsMain.UI.Units
{
    public class JamesButton : Button
    {
        static JamesButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(JamesButton), new FrameworkPropertyMetadata(typeof(JamesButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Border Border = GetTemplateChild("bd") as Border;
        }
    }
}
