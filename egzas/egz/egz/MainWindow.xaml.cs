using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SlaptazodzioAtkurimas
{
    public partial class MainWindow : Window
    {
        private slaptazodziomanageris slaptazodziomanageris;
        private bruteforceatkoduotojas bruteforceatkoduotojas;

        public MainWindow()
        {
            InitializeComponent();
            slaptazodziomanageris = new slaptazodziomanageris();
            bruteforceatkoduotojas = new bruteforceatkoduotojas();

            SlaptazodzioTekstoLaukas.TextChanged += SlaptazodzioTekstoLaukas_TextChanged;
            SlaptazodzioTekstoLaukas.GotFocus += SlaptazodzioTekstoLaukas_GotFocus;
            SlaptazodzioTekstoLaukas.LostFocus += SlaptazodzioTekstoLaukas_LostFocus;

            AtnaujintiVisiblity();
        }

        private void SlaptazodzioTekstoLaukas_TextChanged(object sender, TextChangedEventArgs e)
        {
            AtnaujintiVisiblity();
        }

        private void SlaptazodzioTekstoLaukas_GotFocus(object sender, RoutedEventArgs e)
        {
            SlaptazodzioVieta.Visibility = Visibility.Hidden;
        }

        private void SlaptazodzioTekstoLaukas_LostFocus(object sender, RoutedEventArgs e)
        {
            AtnaujintiVisiblity();
        }

        private void AtnaujintiVisiblity()
        {
            SlaptazodzioVieta.Visibility = string.IsNullOrEmpty(SlaptazodzioTekstoLaukas.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private async void uzkoduotiSlaptazodiMygtukas_Click(object sender, RoutedEventArgs e)
        {
            string slaptazodis = SlaptazodzioTekstoLaukas.Text;
            string uzkoduotasSlaptazodis = slaptazodziomanageris.sukurtiSlaptazodi(slaptazodis);
            UzkoduotoSlaptazodzioTekstas.Text = uzkoduotasSlaptazodis;
        }

        private async void atkoduotoslaptazodziomygtukas_Click(object sender, RoutedEventArgs e)
        {
            string uzkoduotasSlaptazodis = UzkoduotoSlaptazodzioTekstas.Text;

            DateTime startTime = DateTime.Now;

            string atkoduotasSlaptazodis = await Task.Run(() => bruteforceatkoduotojas.atkoduotiSlaptazodi(uzkoduotasSlaptazodis));

            TimeSpan elapsedTime = DateTime.Now - startTime;
            string elapsedTimeString = $"{elapsedTime.TotalSeconds:F2} sekundės";

            AtkoduotoSlaptazodzioTekstas.Text = atkoduotasSlaptazodis ?? "Nerasta";

            PraleistasLaikas.Text = elapsedTimeString;
        }

        private void UzkoduotoSlaptazodzioTekstas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}