using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompressionTester.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для NewTestDialog.xaml
    /// </summary>
    public partial class NewTestDialog : Window
    {
        public string Path => FolderTextBox.Text;
        public string TestName => TestNameTextBox.Text;

        public NewTestDialog(string defaultName, string defaultProjectPath)
        {
            InitializeComponent();

            TestNameTextBox.Text = defaultName;
            FolderTextBox.Text = defaultProjectPath;
        }

        private void OnContentRendered(object sender, EventArgs args)
        {
            TestNameTextBox.Focus();
            TestNameTextBox.SelectAll();
        }

        private void OnOkButtonClick(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(TestNameTextBox.Text))
            {
                MessageBox.Show("Name should be specified");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void OnChooseFolderButtonClick(object sender, EventArgs args)
        {
            var newPath = Dialog.GetFolder(Path);
            if (newPath != null)
                FolderTextBox.Text = newPath;
        }
    }
}
