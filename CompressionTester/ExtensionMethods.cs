using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public static class ExtensionMethods
    {
        //Deconstructors
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key,
            out TValue value)
        {
            key = pair.Key;
            value = pair.Value;
        }

        //StringBuilder
        public static StringBuilder AppendRange(this StringBuilder builder, IEnumerable<string> values)
        {
            foreach (var val in values)
            {
                builder.Append(val);
            }

            return builder;
        }
        public static StringBuilder AppendRangeWhiteSpace(this StringBuilder builder, IEnumerable<string> values)
        {
            foreach (var val in values)
            {
                builder.AppendWhiteSpace(val);
            }

            return builder;
        }
        public static StringBuilder AppendWhiteSpace(this StringBuilder builder, string value)
        {
            return builder.Append(value).Append(" ");
        }
    }
}
