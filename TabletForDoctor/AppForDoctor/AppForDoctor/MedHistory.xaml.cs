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
using System.Windows.Shapes;

namespace AppForDoctor
{
    /// <summary>
    /// Interaction logic for MedHistory.xaml
    /// </summary>
    public partial class MedHistory : Window
    {
        public MedHistory()
        {
            InitializeComponent();
        }

        private void MedHistoryWindow_Closed(object sender, EventArgs e)
        {
            //TODO: save history in data base
            ExaminationPage.closeHistory();
        }
    }
}
