using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class StatisticsBuilder
    {
        private readonly Dictionary<string, (long sourceLength, long resultLength, int operationTime)> _compressionData 
            = new Dictionary<string, (long sourceLength, long resultLength, int operationTime)>();
        private readonly Dictionary<string, (long sourceLength, long resultLength, int operationTime)> _restorationData 
            = new Dictionary<string, (long sourceLength, long resultLength, int operationTime)>();

        public void ProcessCompressOutput(string stdOut)
        {
            var parsedData = ParseOutput(stdOut);
            MoveData(parsedData, _compressionData);
        }
        public void ProcessRestoreOutput(string stdOut)
        {
            var parsedData = ParseOutput(stdOut);
            MoveData(parsedData, _restorationData);
        }

        public Statistics ToStatistics()
        {
            var statistics = new Statistics();

            foreach (var (tag, _) in _restorationData)
            {
                if (TryJoinData(tag, out var item))
                {
                    statistics.AddItem(item);
                }
            }

            return statistics;
        }

        private static Stream StreamFromString(string s)
        {
            var bytes = Encoding.ASCII.GetBytes(s);
            return new MemoryStream(bytes);
        }

        private static string GetTag(string fileName)
        {
            var info = new FileInfo(fileName);
            return Path.GetFileNameWithoutExtension(info.FullName);
        }

        private static Dictionary<string, (long sourceLength, long resultLength, int operationTime)> ParseOutput(string stdOut)
        {
            var result = new Dictionary<string, (long sourceLength, long resultLength, int operationTime)>();

            using (var reader = new StreamReader(StreamFromString(stdOut)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = line.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    var tag = GetTag(tokens[0]);
                    var sourceLength = long.Parse(tokens[2]);
                    var resultLength = long.Parse(tokens[3]);
                    var operationTime = int.Parse(tokens[4]);

                    result[tag] = (sourceLength, resultLength, operationTime);
                }
            }

            return result;
        }

        private static void MoveData<T>(T source, T dist)
            where T : Dictionary<string, (long sourceLength, long resultLength, int operationTime)>
        {
            foreach (var (tag, data) in source)
            {
                dist.Add(tag, data);
            }
        }

        public bool TryJoinData(string tag, out StatisticsItem item)
        {
            if (!_restorationData.ContainsKey(tag) || !_compressionData.ContainsKey(tag))
            {
                item = new StatisticsItem();
                return false;
            }

            var compressionData = _compressionData[tag];
            var restorationData = _restorationData[tag];

            var compressionCoefficient =
                (decimal) compressionData.sourceLength / (decimal) compressionData.resultLength;

            item = new StatisticsItem(
                tag,
                compressionData.sourceLength,
                compressionData.resultLength,
                restorationData.resultLength,
                compressionCoefficient,
                compressionData.operationTime,
                restorationData.operationTime
            );

            return true;
        }
    }
}
