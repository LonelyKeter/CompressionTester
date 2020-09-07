using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompressionTester.Client
{
    public static class TestCommands
    {
        public static readonly RoutedCommand StartTest = new RoutedCommand("Start test", typeof(Control));
        public static readonly RoutedCommand AddSources = new RoutedCommand("Add sources", typeof(Control));
        public static readonly RoutedCommand RemoveSources = new RoutedCommand("Remove sources", typeof(Control));
        public static readonly RoutedCommand ClearStatistics = new RoutedCommand("Clear statistics", typeof(Control));
    }
}
