using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester.Client.Models
{
    public class Source
    {
        public string Name { get; private set; }

        public Source(string name)
        {
            Name = name;
        }
    }
}
