﻿using ManagingDirectorMobile.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagingDirectorMobile.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void MenuGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = null;
        }

        private void MenuFunctionsPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            // No operation - override the menu grid mouse up event handler
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).cntrlZ1.Content = null;
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = null;
            ((MainWindow)Application.Current.MainWindow).cntrlZ3.Content = null;
            ((MainWindow)Application.Current.MainWindow).cntrlZ4.Content = null;
        }
    }
}
