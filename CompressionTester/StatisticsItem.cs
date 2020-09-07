using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class StatisticsItem
    {
        public readonly string Name;

        public readonly long SourceSize;
        public readonly long CompressedSize;
        public readonly long RestoredSize;

        public readonly decimal CompressionCoefficient;

        public readonly int CompressionTime;
        public readonly int RestorationTime;

        public StatisticsItem(
            string name,
            long sourceSize,
            long compressedSize,
            long restoredSize,
            decimal compressionCoefficient,
            int compressionTime,
            int restorationTime)
        {
            Name = name;

            SourceSize = sourceSize;
            CompressedSize = compressedSize;
            RestoredSize = restoredSize;

            CompressionCoefficient = compressionCoefficient;

            CompressionTime = compressionTime;
            RestorationTime = restorationTime;
        }
    }
}
