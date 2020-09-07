using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class CompressorInterface
    {
        private readonly string _compressorPath;
        private ProcessStartInfo _startInfo;

        public CompressorInterface(string compressorPath)
        {
            _compressorPath = compressorPath;
            _startInfo = CreateStartInfo();
        }

        public ExecutionResult CompressFiles(
            IEnumerable<FileInfo> sources, 
            DirectoryInfo targetDir,
            string algorithmKey)
        {
            var command = BuildCompressionCommand(sources, targetDir, algorithmKey);
            return Execute(command);
        }

        public ExecutionResult RestoreFiles(
            IEnumerable<FileInfo> sources,
            DirectoryInfo targetDir,
            string algorithmKey)
        {
            var command = BuildRestorationCommand(sources, targetDir, algorithmKey);
            return Execute(command);
        }


        private PromptCommand BuildCompressionCommand(
            IEnumerable<FileInfo> sources,
            DirectoryInfo targetDir,
            string algorithmKey)
        {
            var command = new PromptCommand()
                .SetExecutable(_compressorPath)
                .SetCommand("comp")
                .SetParameter("-d", targetDir.FullName)
                .SetParameter("-a", algorithmKey)
                .AddArguments(sources.Select(s => "\"" + s.FullName + "\""));

            return command;
        }

        private PromptCommand BuildRestorationCommand(
            IEnumerable<FileInfo> sources,
            DirectoryInfo targetDir,
            string algorithmKey)
        {
            var command = new PromptCommand()
                .SetExecutable(_compressorPath)
                .SetCommand("comp")
                .SetParameter("-d", targetDir.FullName)
                .SetParameter("-a", algorithmKey)
                .AddArguments(sources.Select(s => "\"" + s.FullName + "\""));

            return command;
        }

        private ExecutionResult Execute(PromptCommand command)
        {
            using (var process = new Process())
            {
                process.StartInfo = _startInfo;
                process.StartInfo.Arguments = command.GetString();

                var stdout = GetStdOut(process);
                var stderr = GetStdErr(process);

                process.WaitForExit();

                return new ExecutionResult(stdout, stderr, process.ExitCode);
            }
        }

        private static string GetStdOut(Process process)
        {
            string stdout = null;
            using (var outStream = process.StandardOutput)
            {
                stdout = outStream.ReadToEnd();
            }
            return stdout;
        }

        private static string GetStdErr(Process process)
        {
            string stderr = null;
            using (var errStream = process.StandardError)
            {
                stderr = errStream.ReadToEnd();
            }
            return stderr;
        }

        private ProcessStartInfo CreateStartInfo() 
            => new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
    }
}
