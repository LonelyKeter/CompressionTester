using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompressionTester.Client.Models;

namespace CompressionTester.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для SourceView.xaml
    /// </summary>
    public partial class SourceView : UserControl
    {
        public ObservableCollection<Source> Sources
        {
            get { return (ObservableCollection<Source>)GetValue(SourcesProperty); }
            set { SetValue(SourcesProperty, value); }
        }

        public static readonly DependencyProperty SourcesProperty =
            DependencyProperty.Register(
                "SourcesCollection", 
                typeof(ObservableCollection<Source>), 
                typeof(SourceView), 
                new PropertyMetadata(null));


        public SourceView()
        {
            InitializeComponent();
        }
    }
}
