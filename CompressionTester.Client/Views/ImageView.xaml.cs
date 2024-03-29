﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompressionTester.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для Preview.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        public System.Drawing.Image Source
        {
            get { return (System.Drawing.Image)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(System.Drawing.Image), typeof(ImageView), new PropertyMetadata(null));



        public ImageView()
        {
            InitializeComponent();
        }
    }
}
