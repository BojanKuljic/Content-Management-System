using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using System.IO;
using System.Data;
using Classes;

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {

        private DataIO serializer = new DataIO();

        public static BindingList<NIkePatike> patike { get; set; }
        public string user;

        public TableWindow(string text)
        { 
            user = text;
            InitializeComponent();

            korisnik.Content = "Dobrodosli, " +text;

            patike = serializer.DeSerializeObject<BindingList <NIkePatike>>("patike.xml");

            if (patike == null)
            {
                patike = new BindingList<NIkePatike>();
            }
            DataContext = this;

            if (user == "POSJETILAC")
            {
                dodaj.Visibility = Visibility.Hidden;
                obrisi.Visibility = Visibility.Hidden;
            }
        }

    

        private void Hyperlink_Click(object sender, RoutedEventArgs ee)
        {
            if (dodaj.Visibility != Visibility.Hidden)
            {
                AddWindow addWindow = new AddWindow(((Classes.NIkePatike)TabelarniPrikaz.SelectedItem));
                addWindow.ShowDialog();
                serializer.SerializeObject<BindingList<NIkePatike>>(patike, "patike.xml");
            }
            else
            {
               ViewWindow viewWindow = new ViewWindow(((Classes.NIkePatike)TabelarniPrikaz.SelectedItem));
               viewWindow.ShowDialog();
               serializer.SerializeObject<BindingList<NIkePatike>>(patike, "patike.xml");
            }
        }      
    
        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(null);
            addWindow.ShowDialog();
            serializer.SerializeObject<BindingList<NIkePatike>>(patike, "patike.xml");
        }

        private void obrisi_Click(object sender, RoutedEventArgs e)
        {
            int brojOznaceinih = 0;
            for (int i = 0; i <= patike.Count-1; i++)
            {
                
                var oznaceno = TabelarniPrikaz.Items[i] as NIkePatike;
                if (oznaceno != null && oznaceno.Cekirano)
                {
                    brojOznaceinih++;
                   

                   if (MessageBox.Show("Da li ste sigurni da zelite da obrisete stavku?" , Title = "Brisanje", button: MessageBoxButton.OKCancel, icon: MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        File.Delete(patike[i].NazivPatika + patike[i].BrojPatika.ToString() + ".rtf");
                        patike.RemoveAt(i);
                        i--;
                    }
                   
                }
                if (patike.Count == 0)
                {
                    MessageBox.Show("Sve stavke su obrisane", "OBAVESTENJE", MessageBoxButton.OK, MessageBoxImage.Information);
                }
               
            }

            serializer.SerializeObject<BindingList<NIkePatike>>(patike, "patike.xml");

            if (brojOznaceinih < 1)
            {
                MessageBox.Show("Oznacite stavku za brisanje !", "GRESKA", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            serializer.SerializeObject<BindingList<NIkePatike>>(patike, "patike.xml");
        }
    }
}
