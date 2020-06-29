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
using System.Collections.ObjectModel;


using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using Syncfusion.Pdf.Grid;
using System.Data;
using Controller;
using Model.Roles;
using Class_Diagram.Model.Appointments;
using Model.Appointments;

namespace HCIProjekat.UserControls
{
    /// <summary>
    /// Interaction logic for ListDoctorAppointmentsUC.xaml
    /// </summary>
    public partial class ListDoctorAppointmentsUC : UserControl
    {
        private UserController userController = new UserController();
        private AppointmentController appointmentController = new AppointmentController();
        public ObservableCollection<Appointment> termini;
        public Doctor doktor;
        private DateTime termin_od;
        private DateTime termin_do;
        public ListDoctorAppointmentsUC(DateTime termin_od ,DateTime termin_do,Doctor doktor)
        {
            InitializeComponent();
            Model.SviTermini.getInstance();
            Model.SviZaposleni.getInstance();
            Model.SviPacijenti.getInstance();
            this.doktor = doktor;
            foreach (Appointment app in appointmentController.GetExistingAppointmentsInSpan(termin_od, termin_do, doktor))
            {
                termini.Add(app);
            }
            this.termin_od = termin_od;
            this.termin_do = termin_do;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(Appointment t in termini)
            {
                //Patient p = userController.GetPatientBySearch(t.jmbgPacijenta,"","")[0];
                DgAppointmentsListXAML.Items.Add(new Model.TerminiDoktoraRow(t.StartTime.ToShortDateString(),t.StartTime.ToShortTimeString(),t.room.GetId().ToString(),t.MedicalRecordId.ToString()));
            }
            lbl_appointment_counter.Content = termini.Count;
        }

        private void btn_pdf_Click(object sender, RoutedEventArgs e)
        {
            
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();

            PdfGraphics graphics = page.Graphics;
            PdfFont font1 = new PdfStandardFont(PdfFontFamily.TimesRoman, 35);

            PdfFont font2 = new PdfStandardFont(PdfFontFamily.TimesRoman, 20);

            PdfFont font3 = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Top;
            graphics.DrawString("Nasa mala klinika", font1, PdfBrushes.Black, new PointF(140, 0));

            graphics.DrawString("Izvestaj o zauzetosti lekara ", font2, PdfBrushes.Black, new PointF(150, 60));

            graphics.DrawString(printInfo(), font3, PdfBrushes.Black, new PointF(10, 100));
            graphics.DrawString(dateInfo(), font3, PdfBrushes.Black, new PointF(350, 100));
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("Datum");
            dataTable.Columns.Add("Vreme");
            dataTable.Columns.Add("Sala");
            dataTable.Columns.Add("JMBG");
            dataTable.Columns.Add("Ime");
            dataTable.Columns.Add("Prezime");
            //Add rows to the DataTable.


            foreach (Appointment t in termini)
            {
                /*Patient p = userController.GetPatientBySearch(t.jmbgPacijenta,"","")[0];
                dataTable.Rows.Add(new object[] { t.vreme.ToShortDateString(), t.vreme.ToShortTimeString(), t.soba, p.Jmbg, p.Name, p.Surname });*/
            }
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(0, 200));
            //Save the document.
            doc.Save("Output.pdf");
            //close the document
            doc.Close(true);
        }

        private String printInfo()
        {
            String retVal = "Id:  "+doktor.GetId()+"\nIme:  "+doktor.Name+"\nPrezime:  "+doktor.Surname + "\nZvanje:  "+getZvanje(doktor);
            return retVal;
        }

        private String dateInfo()
        {
            String retVal = "Vreme od: " + termin_od.ToShortDateString()+ "- "+termin_do.ToShortDateString()+"\nUkupno pregleda: "+lbl_appointment_counter.Content.ToString()+"\n\nPodaci preuzeti: "+DateTime.Now.ToString();
            return retVal;
        }

        private string getZvanje(Staff zap)
        {
            if (zap.UserType.Equals(UserType.Specialist))
                return ((Specialist)zap).Specialization;
            else if (zap.UserType.Equals(UserType.Doctor))
                return "Doktor opste prakse";
            else if (zap.UserType.Equals(UserType.Secretary))
                return "Sekretar";
            else if (zap.UserType.Equals(UserType.Manager))
                return "Menadzer";
            else
                return "";
        }
    }
}
