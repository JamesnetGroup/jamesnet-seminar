using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace James.Forms.Local.ViewModels
{
    public class MainContentViewModel : ObservableObject
    {
        public string Name { get; init; }

        public MainContentViewModel() 
        {
            Name = "James";
        }
    }
}
