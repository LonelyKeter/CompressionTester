using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompressionTester.Client.Models;

namespace CompressionTester.Client.ViewModels
{
    public class TestVM : DependencyObject
    {
        private TestModel _testModel;

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

        public TestVM(TestModel testModel)
        {
            Assert.TestModelIsNotNull(testModel);

            _testModel = testModel;
            Sources = new ObservableDictionary<string, Source>(_testModel.GetSources());
        }

        private void StartTest();
        private void AddSources();
        private void RemoveResources();
        private void ClearStatistics();

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
