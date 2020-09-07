using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompressionTester;

namespace CompressionTester.Client.Models
{
    public class TestModel
    {
        private readonly Project _project;

        private Tester _tester;

        public Preview SourcePreview { get; private set; }
        public Preview RestoredPreview { get; private set; }

        public Statistics Statistics { get; private set; }

        public string AlgorithmKey { get; set; }
        public IEnumerable<string> AlgorithmsAvailable => _tester.Algorithms;

        private TestModel(Project project, string compressorPath)
        {
            _project = project;

            SourcePreview = new Preview();
            RestoredPreview = new Preview();

            _tester = new Tester(new CompressorInterface(compressorPath));

            Statistics = _project.GetStatistics();
        }

        public void Test(IEnumerable<string> tags)
        {
            _tester
                .SetAlgorithm(AlgorithmKey)
                .TestViceVersa(_project.Hierarchy, tags);
        }
        
        
        public static TestModel Open(string path, string compressorPath)
        {
            var project = Project.Open(path);
            return new TestModel(project, compressorPath);
        }

        public static TestModel Create(string directoryPath, string title, string compressorPath)
        {
            var project = Project.Create(directoryPath, title);
            return new TestModel(project, compressorPath);
        }
    }
}
