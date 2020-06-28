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
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : UserControl, IRemoveSelection
    {
        public RoomsView()
        {
            InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ClearFromFirstUserControlUp();
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = new MenuViewModel();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = new RemovalViewModel("prostoriju " + "Hirurgija 1", this);
        }

        private void ShowHistoricalDataButton_Click(object sender, RoutedEventArgs e)
        {
            //DrugsViewModel.Drug drug = DrugListDG.SelectedItem as DrugsViewModel.Drug;
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = new RoomOccupancyHistoryViewModel(null);
        }

        private void DrugListDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null) ShowHistoricalDataButton.IsEnabled = true;
            else ShowHistoricalDataButton.IsEnabled = false;
        }

        private void Report_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //;
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = new EquipmentRelocationViewModel();
        }

        private void EqupmentButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = new RoomEquipmentViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).cntrlZ2.Content = new RoomAddViewModel();
        }

        public void RemoveSelectedItem()
        {
            throw new NotImplementedException();
        }
    }
}