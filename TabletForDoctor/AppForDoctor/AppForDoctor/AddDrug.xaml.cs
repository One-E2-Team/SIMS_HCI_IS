﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppForDoctor
{
    /// <summary>
    /// Interaction logic for AddDrug.xaml
    /// </summary>
    public partial class AddDrug : Window
    {
        private HashSet<string> drugSet = new HashSet<string>();
        public AddDrug()
        {
            InitializeComponent();
            //TODO: load all medicaments in hospital
            drugSet.Add("Novi");
            drugSet.Add("Nnovi");
            drugSet.Add("Noovi");
            drugSet.Add("Novvi");
            drugSet.Add("Novii");
            drugSet.Add("Novio");
            drugSet.ExceptWith(DrugsPage.getInstance().getDrugSet());
            foreach (string s in drugSet) addDrugsComboBox.Items.Add(s);
            addDrugsComboBox.SelectedIndex = 0;
            if (MainWindow.GetLanguage() == MainWindow.Language.Serbian) ToSerbian();
            else if (MainWindow.GetLanguage() == MainWindow.Language.English) ToEnglish();
            if (MainWindow.GetTheme() == MainWindow.Theme.Light) ToLightTheme();
            else if (MainWindow.GetTheme() == MainWindow.Theme.Dark) ToDarkTheme();
        }

        private void ToSerbian()
        {
            addDrugButton.Content = "Dodaj";
            backFromAddButton.Content = "Nazad";
        }

        private void ToEnglish()
        {
            addDrugButton.Content = "Add";
            backFromAddButton.Content = "Back";
        }

        private void ToLightTheme()
        {
        }

        private void ToDarkTheme()
        {
        }

        private void backFromAddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddDrugWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow w = MainWindow.getInstance();
            this.Left = w.Left + (w.Width - this.ActualWidth) / 2;
            this.Top = w.Top + (w.Height - this.ActualHeight) / 2;
        }

        private void addDrugButton_Click(object sender, RoutedEventArgs e)
        {
            string item = addDrugsComboBox.SelectedItem.ToString();
            DrugsPage d = DrugsPage.getInstance();
            d.addDrugToSet(item);
            drugSet.Remove(item);
            addDrugsComboBox.Items.Remove(item);
            addDrugsComboBox.SelectedIndex = 0;
            if (drugSet.Count == 0)
            {
                DrugsPage.getInstance().disableAddButton();
                this.Close();
            }
            else if (addDrugsComboBox.Items.Count == 0) addDrugButton.IsEnabled = false;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string input = searchInput.Text.ToLower();
            addDrugsComboBox.Items.Clear();
            foreach (string s in drugSet)
            {
                if (s.ToLower().Contains(input))
                {
                    addDrugsComboBox.Items.Add(s);
                }
            }
            if (addDrugsComboBox.Items.Count != 0)
            {
                addDrugsComboBox.SelectedIndex = 0;
                addDrugButton.IsEnabled = true;
            }
            else addDrugButton.IsEnabled = false;
        }

        private void searchInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter) searchButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
    }
}
