using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester.Client.ViewModels
{
    public class StatisticsPresentation
    {
        public string Name { get; set; }

        public string SourceFileSize { get; set; }
        public string RestoredFileSize { get; set; }

        public float CompressionCoefficient { get; set; }

        public string CompressionTime { get; set; }
        public string RestorationTime { get; set; }
    }
}
