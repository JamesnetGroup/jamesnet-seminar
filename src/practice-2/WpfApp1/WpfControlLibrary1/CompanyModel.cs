using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrary1
{
    public class CompanyModel
    {
        public string Name { get; internal set; }
        public string Id { get; internal set; }
        public bool IsSelected { get; set; }
    }
}
