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
using CompressionTester.Client.ViewModels;

namespace CompressionTester.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage(TestVM testVM)
        {
            this.DataContext = testVM;

            InitializeComponent();
        }
    }
}
