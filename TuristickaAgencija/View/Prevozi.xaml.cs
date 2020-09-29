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
using TuristickaAgencija.Baza;

namespace TuristickaAgencija
{
    /// <summary>
    /// Interaction logic for Prevozi.xaml
    /// </summary>
    public partial class Prevozi : UserControl
    {
        private Baza.DbTuristickaAgencija context;
        private UnitOfWork unit;
        private IEnumerable<KartePrevoza> aranz;
        public Prevozi()
        {
            InitializeComponent();
            context = new Baza.DbTuristickaAgencija();
            unit = new UnitOfWork(context);
            napuniKarte();

            grid1.ItemsSource = aranz;


        }

        private void napuniKarte()
        {
            this.aranz = this.unit.KartePrevozs.GetAllKartePrevoza();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (two.SelectedDate.Value < three.SelectedDate.Value)
            {
                TurAranzmann novi = new TurAranzmann();
                novi.destinacija = one.Text;
                novi.datumPocetka = two.SelectedDate.Value;
                novi.datumKraja = three.SelectedDate.Value;
                try
                {
                    novi.cena = float.Parse(four.Text);
                }
                catch
                {
                    MessageBox.Show("Cena nije uneta u ciframa.");
                    return;
                }
                novi.nacinPlacanja = five.Text;
                this.unit.TurAranzmans.AddTurAranzmann(novi);

                MessageBox.Show("Uspešno dodat novi aranžman");
            }
            else
            {
                MessageBox.Show("Datumi nisu validni.");
                one.Text = String.Empty;
                two.SelectedDate = null;
                three.SelectedDate = null;
                four.Text = string.Empty;
                five.Text = string.Empty;
            }
            napuniKarte();

            grid1.ItemsSource = aranz;
            


        }
    }
}
