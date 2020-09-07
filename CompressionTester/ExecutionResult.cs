using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public readonly struct ExecutionResult
    {
        public readonly string StdOut;
        public readonly string StdErr;
        public readonly int ExitCode;

        public ExecutionResult(string stdout, string stderr, int exitCode)
        {
            StdOut = stdout;
            StdErr = stderr;
            ExitCode = exitCode;
        }
    }
}
