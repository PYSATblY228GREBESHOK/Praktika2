using System;
using System.Collections.Generic;
using System.Data;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Polly;
using Praktika2;
using Praktika2.KFCDataSetTableAdapters;
using static MaterialDesignThemes.Wpf.Theme;

namespace Practika2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
        }
    }
}
