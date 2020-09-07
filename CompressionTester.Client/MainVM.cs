using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompressionTester.Client.ViewModels;
using CompressionTester.Client.Views;

namespace CompressionTester.Client
{
    public class MainVM : DependencyObject
    {
        private readonly MainModel _mainModel;

        public IList CommandBindings { get; }

        public object CurrentPage
        {
            get => GetValue(CurrentPageProperty); 
            set => SetValue(CurrentPageProperty, value);
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(object), typeof(MainVM), new PropertyMetadata(null));


        public MainVM()
        {
            _mainModel = new MainModel();
            
            CommandBindings = CreateCommandBindings();
        }

        private IList CreateCommandBindings() => new[]
        {
            new CommandBinding(ApplicationCommands.New, OnNewTest, NewTestCanExecute),
            new CommandBinding(ApplicationCommands.Open, OnOpenTest, OpenTestCanExecute),
            new CommandBinding(ApplicationCommands.Close, OnCloseTest, CloseTestCanExecute)
        };

        private void OnNewTest(object sender, ExecutedRoutedEventArgs args)
        {
            var testModel = _mainModel.CreateTest();
            if (testModel != null)
            {
                CurrentPage = new TestPage(new TestVM(testModel));
            }
        }
        private void NewTestCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = true;

        private void OnOpenTest(object sender, ExecutedRoutedEventArgs args)
        {
            var testModel = _mainModel.OpenTest();
            if (testModel != null)
            {
                CurrentPage = new TestPage(new TestVM(testModel));
            }
        }
        private void OpenTestCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = true;

        private void OnCloseTest(object sender, ExecutedRoutedEventArgs args)
        {
            CurrentPage = null;
        }
        private void CloseTestCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = CurrentPage != null;
    }
}
