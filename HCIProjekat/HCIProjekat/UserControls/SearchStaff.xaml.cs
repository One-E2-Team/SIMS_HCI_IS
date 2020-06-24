﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProjekat.UserControls
{
    /// <summary>
    /// Interaction logic for SearchStaff.xaml
    /// </summary>
    public partial class SearchStaff : UserControl
    {
        private bool isDoctor;
        private ObservableCollection<Model.Zaposleni> zaposleni = new ObservableCollection<Model.Zaposleni>();
        private Model.Zaposleni radnik = null;
        public SearchStaff(bool isDoctor)
        {
            InitializeComponent();
            this.isDoctor = isDoctor;
            zaposleni = Model.SviZaposleni.getInstance().getZaposleni();
        }

        private void CmbStaffType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStaffType.SelectedItem == cmbItemDoctor)
            {
                cmbDoctorSpeciality.IsEnabled = true;
            }
            else
            {
                if (cmbDoctorSpeciality != null)
                {
                    cmbDoctorSpeciality.SelectedIndex = 0;
                    cmbDoctorSpeciality.IsEnabled = false;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isDoctor)
            {
                cmbStaffType.SelectedItem = cmbItemDoctor;
                cmbStaffType.IsEnabled = false;
                foreach (Model.Zaposleni z in zaposleni)
                {
                    if (z.tipZaposlenog.Equals("Doktor"))
                    {
                        dg_pronadjeni_zaposleni.Items.Add(z);
                    }
                }

            }
            else
            {
                foreach (Model.Zaposleni z in zaposleni)
                {
                    dg_pronadjeni_zaposleni.Items.Add(z);
                }
            }


        }
        public String getId()
        {
            return radnik.id;
        }

        private void BtnPratrazi_Click(object sender, RoutedEventArgs e)
        {
            dg_pronadjeni_zaposleni.Items.Clear();

            foreach (Model.Zaposleni z in zaposleni)
            {
                String pol;
                if (rb_pol_m.IsChecked == true)
                    pol = "M";
                else if (rb_pol_z.IsChecked == true)
                    pol = "Z";
                else
                    pol = "";

                if (z.id.Contains(txt_jmbg.Text) && z.ime.Contains(txt_ime.Text) && z.prezime.Contains(txt_prezime.Text) && z.pol.Contains(pol))
                {
                    if (cmbStaffType.SelectedIndex == 0) //svi zaposleni
                    {
                        dg_pronadjeni_zaposleni.Items.Add(z);
                    }
                    else if (cmbStaffType.SelectedIndex == 1 && z.tipZaposlenog.Equals("Doktor")) //doktori
                    {
                        if (cmbDoctorSpeciality.SelectedIndex == 0)
                            dg_pronadjeni_zaposleni.Items.Add(z);
                        else
                        {
                            if (z.zvanje.Equals(cmbDoctorSpeciality.Text))
                                dg_pronadjeni_zaposleni.Items.Add(z);
                        }
                    }
                    else //ostali zaposleni
                    {
                        if (z.tipZaposlenog.Equals(cmbStaffType.Text))
                            dg_pronadjeni_zaposleni.Items.Add(z);
                    }
                }
            }
        }

        private void dg_pronadjeni_zaposleni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            radnik = dg_pronadjeni_zaposleni.SelectedItem as Model.Zaposleni;
        }


    }
}