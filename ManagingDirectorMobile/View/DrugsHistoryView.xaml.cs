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

namespace ManagingDirectorMobile.View
{
    /// <summary>
    /// Interaction logic for DrugsHistoryView.xaml
    /// </summary>
    public partial class DrugsHistoryView : UserControl
    {
        public DrugsHistoryView()
        {
            InitializeComponent();
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ClearFromFirstUserControlUp();
        }

        private void PanelGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}