﻿using ManagingDirectorMobile.Model;
using ManagingDirectorMobile.ViewModel;
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
    /// Interaction logic for DrugsChangeView.xaml
    /// </summary>
    public partial class DrugsChangeView : UserControl
    {
        public DrugsChangeView()
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

        private void DrugBatchListDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                CodeTextBox.Text = ((sender as DataGrid).SelectedItem as DrugBatchDummy).Code;
                (DataContext as DrugsChangeViewModel).EXPDate = ((sender as DataGrid).SelectedItem as DrugBatchDummy).StringifyEXP();
            }
        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            var EXP = DatePicker.SelectedDate;
            int threshold, quantity;
            if (!Int32.TryParse(ThresholdTextBox.Text, out threshold)) threshold = -1;
            if (!Int32.TryParse(NewQuantity.Text, out quantity)) quantity = -1;
            String code = CodeTextBox.Text;
            if (code.Equals("")) code = null;
            (DataContext as DrugsChangeViewModel).Save(threshold, EXP, code, quantity);
            ((MainWindow)Application.Current.MainWindow).ClearFromFirstUserControlUp();
        }
    }
}