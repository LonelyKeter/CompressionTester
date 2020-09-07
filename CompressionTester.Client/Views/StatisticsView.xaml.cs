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
using CompressionTester.Client.ViewModels;

namespace CompressionTester.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        public ObservableCollection<StatisticsPresentation> StatisticsList
        {
            get { return (ObservableCollection<StatisticsPresentation>)GetValue(StatisticsListProperty); }
            set { SetValue(StatisticsListProperty, value); }
        }
        public static readonly DependencyProperty StatisticsListProperty =
            DependencyProperty.Register("StatisticsList", typeof(ObservableCollection<StatisticsPresentation>), typeof(StatisticsView), new PropertyMetadata(null));


        public StatisticsView()
        {
            InitializeComponent();
        }
    }
}
