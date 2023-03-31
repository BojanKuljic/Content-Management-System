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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void PrijaviteSe_Click(object sender, RoutedEventArgs e)
        {
            if (validation())
            {


                if (KorisnickoIme.Text.Equals("ADMIN") && Lozinka.Password == "ADMIN")
                {
                    TableWindow tableWindow = new TableWindow(KorisnickoIme.Text);
                    Close();
                    tableWindow.ShowDialog();
                }
                else if (KorisnickoIme.Text.Equals("POSJETILAC") && Lozinka.Password == "POSJETILAC")
                {
                    TableWindow tableWindow = new TableWindow(KorisnickoIme.Text);
                    Close();
                    tableWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Korisnicko ime ili lozinka nisu dobri!", "GRESKA", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Podaci nisu dobro popunjeni!", "GRESKA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        bool validation()
        {
            bool result = true;

            if (!KorisnickoIme.Text.Equals("ADMIN") && !KorisnickoIme.Text.Equals("POSJETILAC"))
            {
                result = false;
                KorisnickoImeGreska.Content = "Pogresno korisnicko ime!";
                KorisnickoImeGreska.Background = Brushes.LightBlue;
                KorisnickoIme.BorderBrush = Brushes.Red;
                KorisnickoIme.Foreground = Brushes.Red;
                KorisnickoIme.BorderThickness = new Thickness(3);
            }
            else
            {
                KorisnickoImeGreska.Content = "";
                KorisnickoIme.BorderBrush = Brushes.Green;
                KorisnickoIme.Foreground = Brushes.Green;
                KorisnickoImeGreska.Background = Brushes.Transparent;
                KorisnickoIme.BorderThickness = new Thickness(3);
            }

            if (!Lozinka.Password.Equals("ADMIN") && !Lozinka.Password.Equals("POSJETILAC"))
            {
                result = false;
                LozinkaGreska.Background = Brushes.LightBlue;
                LozinkaGreska.Content = "Pogresna lozinka!";
                Lozinka.BorderBrush = Brushes.Red;
                Lozinka.Foreground = Brushes.Red;
                Lozinka.BorderThickness = new Thickness(3);
            }
            else
            {
                LozinkaGreska.Content = "";
                Lozinka.BorderBrush = Brushes.Green;
                Lozinka.Foreground = Brushes.Green;
                LozinkaGreska.Background = Brushes.Transparent;
                Lozinka.BorderThickness = new Thickness(3);
            }

            return result;
        }


        private void KorisnickoIme_GotFocus(object sender, RoutedEventArgs e)
        {
            if (KorisnickoIme.Text.Trim().Equals("Unesite korisnicko ime..."))
            {
                KorisnickoIme.Text = "";
                KorisnickoIme.Foreground = Brushes.Green;          
            }
            
        }
        private void Lozinka_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Lozinka.Password.Trim().Equals("Unesite lozinku..."))
            {
                Lozinka.Password = string.Empty;
                Lozinka.Foreground = Brushes.Green;
            }

        }

        private void KorisnickoIme_LostFocus(object sender, RoutedEventArgs e)
        {
            if (KorisnickoIme.Text.Trim().Equals(string.Empty))
            {
                KorisnickoIme.Text = "Unesite korisnicko ime...";
                KorisnickoIme.Foreground = Brushes.LightSlateGray;
            }

        }

       

        private void Lozinka_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Lozinka.Password.Trim().Equals(string.Empty))
            {
                Lozinka.Password = "Unesite lozinku...";
                Lozinka.Foreground = Brushes.LightSlateGray;
            }

        }

        private void Izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
