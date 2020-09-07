using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionTester
{
    public class PromptCommand
    {
        public string Executable { get; private set; }
        public string Command { get; private set; } = string.Empty;

        public IEnumerable<string> Arguments => _arguments;

        private Dictionary<string, List<string>> _parameters = new Dictionary<string, List<string>>();
        private List<string> _arguments = new List<string>();


        public string GetString()
        {
            if (string.IsNullOrEmpty(Executable))
                throw new PromptCommandBuildingException("Executable was not specified");
            
            var builder = new StringBuilder();

            AppendExecutable(builder);
            AppendCommand(builder);
            AppendParameters(builder);
            AppendArguments(builder);

            return builder.ToString();
        }


        public PromptCommand SetExecutable(string value)
        {
            AssertNotNullOrEmpty(value, nameof(value));

            Executable = value;
            return this;
        }
        public PromptCommand SetCommand(string value)
        {
            AssertNotNullOrEmpty(value, nameof(value));

            Command = value;
            return this;
        }

        public PromptCommand SetParameter(string param, IEnumerable<string> paramArgs)
        {
            AssertNotNullOrEmpty(param, nameof(param));

            var list = new List<string>();
            AddNoNullOrEmptyRange(list, paramArgs);

            _parameters[param] = list;

            return this;
        }
        public PromptCommand SetParameter(string param, string paramArg)
        {
            AssertNotNullOrEmpty(param, nameof(param));
            AssertNotNullOrEmpty(paramArg, nameof(paramArg));

            var list = new List<string>();
            list.Add(paramArg);
            _parameters[param] = list;

            return this;
        }

        public PromptCommand SetArguments(IEnumerable<string> values)
        {
            AssertNotNull(values, nameof(values));

            var list = new List<string>();
            AddNoNullOrEmptyRange(list, values);
            _arguments = list;

            return this;
        }
        public PromptCommand AddArguments(IEnumerable<string> values)
        {
            AssertNotNull(values, nameof(values));
            AddNoNullOrEmptyRange(_arguments, values);

            return this;
        }

        private static void AssertNotNullOrEmpty(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentException("Value should not be null or empty", argName);
            }
        }
        private static void AssertNotNull(object arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        private static void AddNoNullOrEmptyRange(List<string> list, IEnumerable<string> values)
        {
            foreach (var val in values)
            {
                AssertNotNullOrEmpty(val, nameof(val));
                list.Add(val);
            }
        }

        private void AppendExecutable(StringBuilder builder) => builder.AppendWhiteSpace(Executable);
        private void AppendCommand(StringBuilder builder) => builder.AppendWhiteSpace(Command);
        private void AppendArguments(StringBuilder builder) => builder.AppendRangeWhiteSpace(_arguments); 
        private void AppendParameters(StringBuilder builder)
        {
            foreach (var (specifier, values) in _parameters)
            {
                builder
                    .AppendWhiteSpace(specifier)
                    .AppendRangeWhiteSpace(values);
            }
        }
    }

    public class PromptCommandBuildingException : Exception
    {
        public PromptCommandBuildingException(string message) : base(message) { }
    }
}
