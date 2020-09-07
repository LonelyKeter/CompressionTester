using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class Tester
    {
        public IEnumerable<string> Algorithms { get; private set; } = new[] {"Identity"};

        public string CurrentAlgorithm { get; private set; }

        private readonly CompressorInterface _compressorInterface;

        public Tester(CompressorInterface compressorInterface)
        {
            _compressorInterface = compressorInterface;
        }


        public Tester SetAlgorithm(string key)
        {
            if (Algorithms.Contains(key))
            {
                CurrentAlgorithm = key;
            }
            else
            {
                throw new ArgumentException("Invalid algorithm key", nameof(key));
            }

            return this;
        }

        public StatisticsBuilder TestViceVersa(Hierarchy hierarchy, IEnumerable<string> tags)
        {
            var builder = new StatisticsBuilder();

            var compressOutput = _compressorInterface.CompressFiles(
                    hierarchy.GetSources(tags), 
                    hierarchy.CompressDir, 
                    CurrentAlgorithm
                    );

            builder.ProcessCompressOutput(compressOutput.StdOut);

            var restoreOutput = _compressorInterface.RestoreFiles(
                hierarchy.GetCompressed(tags, GetCompressedExtension(CurrentAlgorithm)),
                hierarchy.RestoreDir,
                CurrentAlgorithm
                );

            builder.ProcessRestoreOutput(restoreOutput.StdOut);

            return builder;
        }

        //TODO: Real compression file extension evaluation
        private string GetCompressedExtension(string algorithm) => ".comp";
    }
}
