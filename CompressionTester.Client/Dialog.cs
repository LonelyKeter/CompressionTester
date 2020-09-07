using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CompressionTester.Client.Views;

namespace CompressionTester.Client
{
    static class Dialog
    {
        public static string GetFolder(string startPath)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (startPath != null)
                    folderDialog.SelectedPath = startPath;

                if (folderDialog.ShowDialog() != DialogResult.OK)
                    return string.Empty;

                return folderDialog.SelectedPath;
            }
        }

        public static (string directory, string name) GetNewTestInfo()
        {
            var ntDialog = new NewTestDialog("NewTest", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            return ntDialog.ShowDialog() == true 
                ? (ntDialog.Path, ntDialog.TestName) 
                : (null, null);
        }

        public static string GetExistingTest()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.Filter = "Compression test file (*.ctst)|*.ctst|All files (*.*)|*.*";

                return dialog.ShowDialog() == DialogResult.OK
                    ? dialog.FileName
                    : string.Empty;
            }
        }
    }
}
