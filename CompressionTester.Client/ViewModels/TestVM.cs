using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CompressionTester.Client.Models;

namespace CompressionTester.Client.ViewModels
{
    public class TestVM : DependencyObject
    {
        private TestModel _testModel;

        public IList CommandBindings { get; }

        public Image SourcePreview      
        {
            get => (Image)GetValue(SourcePreviewProperty); 
            set => SetValue(SourcePreviewProperty, value); 
        }
        public static readonly DependencyProperty SourcePreviewProperty =
            DependencyProperty.Register("SourcePreview", typeof(Image), typeof(TestVM), new PropertyMetadata(null));

        public Image RestoredPreview
        {
            get => (Image)GetValue(RestoredPreviewProperty);
            set => SetValue(RestoredPreviewProperty, value);
        }
        public static readonly DependencyProperty RestoredPreviewProperty =
            DependencyProperty.Register("RestoredPreview", typeof(Image), typeof(TestVM), new PropertyMetadata(null));

        public ObservableDictionary<string, Source> Sources { get; }
        public ObservableCollection<Source> SelectedSources

        public TestVM(TestModel testModel)
        {
            Assert.TestModelIsNotNull(testModel);

            _testModel = testModel;
            var tagSources = _testModel
                .GetSourceTags()
                .ToDictionary(key => key, name => new Source(name));

            Sources = new ObservableDictionary<string, Source>(tagSources);

            CommandBindings = CreateBindings();
        }

        private IList CreateBindings() 
            => new[] 
            {
                new CommandBinding(TestCommands.StartTest, StartTest, StartTestCanExecute),
                new CommandBinding(TestCommands.AddSources, AddSources, AddSourcesCanExecute),
                new CommandBinding(TestCommands.RemoveSources, RemoveSources, RemoveSourcesCanExecute),
                new CommandBinding(TestCommands.ClearStatistics, ClearStatistics, ClearStatisticsCanExecute)
            };

        private void StartTest(object sender, RoutedEventArgs args)
        {
            throw new NotImplementedException("Start test feature is not implemented.");
        }
        private void StartTestCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = true;

        private void AddSources(object sender, RoutedEventArgs args)
        {
            throw new NotImplementedException("Add sources feature is not implemented.");
        }
        private void AddSourcesCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = true;

        private void RemoveSources(object sender, RoutedEventArgs args)
        {
            throw new NotImplementedException("Remove sources feature is not implemented.");
        }
        private void RemoveSourcesCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = true;

        private void ClearStatistics(object sender, RoutedEventArgs args)
        {
            throw new NotImplementedException("Clear statistics feature is not implemented.");
        }
        private void ClearStatisticsCanExecute(object sender, CanExecuteRoutedEventArgs args) => args.CanExecute = true;

        private static class Assert
        {
            public static void TestModelIsNotNull(TestModel testModel)
            {
                if (testModel == null)
                    throw new ArgumentException("Model argument cannot be null");
            }
        }
    }
}
