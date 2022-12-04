using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hethongquanlylab.Models
{
    public class Link
    {
        private string name;
        private string val;
        private string altVal;

        public string Name { get => name; set => name = value; }
        public string Value { get => val; set => val = value; }
        public string AltVal { get => altVal; set => altVal = value; }

        public Link (string name, string val, string altval = "")
        {
            this.Name = name;
            this.Value = val;
            this.AltVal = altval;
        }
    }
}
