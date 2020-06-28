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
    /// Interaction logic for ChangeDrugAmount.xaml
    /// </summary>
    public partial class ChangeDrugAmount : Window
    {
        private Dictionary<string, int> drugDict = new Dictionary<string, int>();
        private int amount = 1;
        public ChangeDrugAmount()
        {
            InitializeComponent();
            drugDict = DrugsPage.getInstance().getDrugDict();
            foreach (string s in drugDict.Keys) changeAmountComboBox.Items.Add(s);
            changeAmountComboBox.SelectedIndex = 0;
            if (MainWindow.GetLanguage() == MainWindow.Language.Serbian) ToSerbian();
            else if (MainWindow.GetLanguage() == MainWindow.Language.English) ToEnglish();
        }

        private void ToSerbian()
        {
            changeAmountButton.Content = "Promeni";
            backFromChangeAmountButton.Content = "Nazad";
        }

        private void ToEnglish()
        {
            changeAmountButton.Content = "Change";
            backFromChangeAmountButton.Content = "Back";
        }

        private void ChangeDrugAmountWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.CenterDialog(this);
        }

        private void backFromChangeAmountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void changeAmountButton_Click(object sender, RoutedEventArgs e)
        {
            string choice = changeAmountComboBox.SelectedItem.ToString();
            DrugsPage.getInstance().ChangeDrugAmount(choice, amount);
            drugDict[choice] = amount;
            changeAmountButton.IsEnabled = false;
        }

        private void changeAmountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!changeAmountComboBox.Items.IsEmpty)
            {
                string choice = changeAmountComboBox.SelectedItem.ToString();
                amountText.Text = drugDict[choice].ToString();
            }
            //else amountText.Text = "0";
        }

        private void searchInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter) searchButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string input = searchInput.Text.ToLower();
            changeAmountComboBox.Items.Clear();
            changeAmountButton.IsEnabled = false;

            foreach (string s in drugDict.Keys)
            {
                if (s.ToLower().Contains(input)) changeAmountComboBox.Items.Add(s);
            }
            if (changeAmountComboBox.Items.Count != 0)
            {
                changeAmountComboBox.SelectedIndex = 0;
                plusButton.IsEnabled = true;
                if (amount > 1) minusButton.IsEnabled = true;
                amountText.IsEnabled = true;
            }
            else
            {
                amountText.IsEnabled = false;
                plusButton.IsEnabled = false;
                minusButton.IsEnabled = false;
            }
        }

        private void amountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            changeAmountButton.IsEnabled = false;
            plusButton.IsEnabled = false;
            minusButton.IsEnabled = false;
            string choice = changeAmountComboBox.SelectedItem.ToString();

            if (Int32.TryParse(amountText.Text, out amount))
            {
                if (amount >= 1)
                {
                    if (amount != drugDict[choice]) changeAmountButton.IsEnabled = true;
                    plusButton.IsEnabled = true;
                    if (amount > 1) minusButton.IsEnabled = true;
                }
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            amountText.Text = (++amount).ToString();
            minusButton.IsEnabled = true;
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            amountText.Text = (--amount).ToString();
            if (amount == 1) minusButton.IsEnabled = false;
        }
    }
}