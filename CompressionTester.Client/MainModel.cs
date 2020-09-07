using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using CompressionTester.Client.Models;

namespace CompressionTester.Client
{
    class MainModel
    {
        public string DefaultCompressorPath = Environment.CurrentDirectory;

        public TestModel OpenTest()
        {
            var path = Dialog.GetExistingTest();

            if (!string.IsNullOrEmpty(path))
            {
                return TestModel.Open(path, DefaultCompressorPath);
            }
            else
            {
                return null;
            }
        }

        public TestModel CreateTest()
        {
            var (directoryPath, title) = Dialog.GetNewTestInfo();

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(directoryPath))
            {
                return TestModel.Create(directoryPath, title, DefaultCompressorPath);
            }
            else
            {
                return null;
            }
        }
    }
}
