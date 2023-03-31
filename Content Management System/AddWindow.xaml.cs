using Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
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
using F = System.Windows.Forms;

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private string linkZaSliku;
        private readonly int indeks = -1;
        string stariFajl = "";

        public AddWindow(Classes.NIkePatike s)
        {
            InitializeComponent();
            PromenaFonta.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            PromenaVelicine.ItemsSource = new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };

            if (s != null)
            {
                NazivDodaj.Text = s.NazivPatika;
                Uri uri = new Uri(s.Slika);
                UcitanaSlika.Source = new BitmapImage(uri);
                Velicina.Text = s.BrojPatika.ToString();
                dodaj.Content = "IZMJENI";
                naslovDodavanje.Content = "IZMJENA SADRZAJA";

                indeks = TableWindow.patike.IndexOf(s);
                TextRange textRange;
                System.IO.FileStream fileStream;

                if (System.IO.File.Exists(s.RTF_putanja))
                {
                    textRange = new TextRange(OpisRichTextBox.Document.ContentStart, OpisRichTextBox.Document.ContentEnd);
                    using (fileStream = new System.IO.FileStream(s.RTF_putanja, System.IO.FileMode.OpenOrCreate))
                    {
                        textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                    }

                    stariFajl = s.RTF_putanja;
                }

                
            }
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {


            if (ValidacijaPolja())
            {

                NIkePatike patika1 = new NIkePatike(Int32.Parse(Velicina.Text), NazivDodaj.Text, false,
                    UcitanaSlika.Source.ToString(), NazivDodaj.Text + Velicina.Text.ToString() + ".rtf", DateTime.Now);

                if (indeks == -1)
                {
                    string naziv = NazivDodaj.Text + Velicina.Text.ToString() + ".rtf";
                    FileStream fileStream = new FileStream(naziv, FileMode.Create);
                    new TextRange(OpisRichTextBox.Document.ContentStart, OpisRichTextBox.Document.ContentEnd).Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();

                    if (MessageBox.Show("Potvrdite unos novih patika: " + NazivDodaj.Text.Trim(), Title = "Potvrda", button: MessageBoxButton.OKCancel, icon: MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        TableWindow.patike.Add(patika1);
                    }
                    
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("Da li zelite da potvrdite izmjene? ", Title = "Potvrda", button: MessageBoxButton.OKCancel, icon: MessageBoxImage.Question) == MessageBoxResult.OK)
                        TableWindow.patike[indeks] = patika1;

                    string naziv = NazivDodaj.Text + Velicina.Text.ToString() + ".rtf";
                    FileStream fileStream = new FileStream(naziv, FileMode.Create);
                    new TextRange(OpisRichTextBox.Document.ContentStart, OpisRichTextBox.Document.ContentEnd).Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();      
                    
                    // brisi stari fajl
                    if(File.Exists(stariFajl))
                    {
                        File.Delete(stariFajl);
                    }



                    this.Close();
                }
            }        
            else
            {
                MessageBox.Show("Podaci nisu dobro popunjeni!", "GRESKA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private bool ValidacijaPolja()
        {
            bool validno = true;
           // int rez;
            if (NazivDodaj.Text.Trim().Equals("") || NazivDodaj.Text.Trim().Equals("Unesite naziv..."))
            {
                validno = false;
                NazivDodaj.BorderBrush = Brushes.Red;
                NazivDodaj.Foreground = Brushes.DarkRed;
                NazivDodaj.BorderThickness = new Thickness(2);
                NazivGreska.Content = "Unesite naziv!";
            }
            else
            {
                NazivDodaj.BorderBrush = Brushes.Green;
                NazivDodaj.Foreground = Brushes.Green;
                NazivDodaj.BorderThickness = new Thickness(2);
                NazivGreska.Content = "";
            }

            if (Velicina.Text.Trim().Equals("") || Velicina.Text.Trim().Equals("Unesite velicinu..."))
            {
                validno = false;
                Velicina.BorderBrush = Brushes.Red;
                Velicina.BorderThickness = new Thickness(2);
                VelicinaGreska.Content = "Unesite velicinu!";
            }
            else
            {
                Velicina.BorderBrush = Brushes.Green;
                Velicina.Foreground = Brushes.Green;
                Velicina.BorderThickness = new Thickness(2);
                VelicinaGreska.Content = "";

                try
                {
                    
                    if (Int64.Parse(Velicina.Text.Trim()) < 0)
                    {
                        validno = false;
                        Velicina.BorderBrush = Brushes.Red;
                        Velicina.BorderThickness = new Thickness(2);
                        VelicinaGreska.Content = "Velicina mora biti \n pozitivan broje!";
                    }
                    else
                    {
                        Velicina.BorderBrush = Brushes.Green;
                        Velicina.Foreground = Brushes.Green;
                        Velicina.BorderThickness = new Thickness(2);
                        VelicinaGreska.Content = "";
                    }
                }
                catch (Exception e)
                {
                    validno = false;
                    Velicina.BorderBrush = Brushes.Red;
                    Velicina.BorderThickness = new Thickness(2);
                    VelicinaGreska.Content = "Mora biti broj!";
                    Console.WriteLine(e.Message);
                }
            }


            if (new TextRange(OpisRichTextBox.Document.ContentStart, OpisRichTextBox.Document.ContentEnd).Text.Trim().Equals(""))
            {
                validno = false;
                OpisRichTextBox.BorderBrush = Brushes.Red;
                OpisRichTextBox.BorderThickness = new Thickness(2);
                opis.Background = Brushes.LightBlue;
                opis.Opacity = 0.7;
                opis.Content = " Opis nije dodat!";
            }
            else
            {
                OpisRichTextBox.BorderBrush = Brushes.Green;
                OpisRichTextBox.BorderThickness = new Thickness(2);
                opis.Background = Brushes.LightBlue;
                opis.Opacity = 0.7;                
                opis.Content = " Opis dodat!";
                opis.Foreground = Brushes.Green;    
            }


            if (UcitanaSlika.Source == null)
            {
                validno = false;

                SlikaGreska.Content = "Dodajte sliku!";
                SlikaGreska.Foreground = Brushes.Red;
                ucitajButtom.BorderBrush = Brushes.Red;
                ucitajButtom.BorderThickness = new Thickness(2);

            }
            else
            {
                SlikaGreska.Content = "Ucitano";               
                SlikaGreska.Foreground = Brushes.Green;
                ucitajButtom.BorderBrush = Brushes.Black;
                ucitajButtom.BorderThickness = new Thickness(2);
            }
                

            return validno;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void NazivDodaj_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NazivDodaj.Text.Trim().Equals("Unesite naziv..."))
            {
                NazivDodaj.Text = "";
                NazivDodaj.Foreground = Brushes.DarkRed;
            }
        }

        private void NazivDodaj_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NazivDodaj.Text.Trim().Equals(string.Empty))
            {
                NazivDodaj.Text = "Unesite naziv...";
                NazivDodaj.Foreground = Brushes.LightSlateGray;
            }
        }

        private void Velicina_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Velicina.Text.Trim().Equals("Unesite velicinu..."))
            {
                Velicina.Text = "";
                Velicina.Foreground = Brushes.DarkRed;
            }
        }

        private void Velicina_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Velicina.Text.Trim().Equals(string.Empty))
            {
                Velicina.Text = "Unesite velicinu...";
                Velicina.Foreground = Brushes.LightSlateGray;
            }

        }
        private void ucitajButtom_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SlikaUcitavanje.Content.Equals(" "))
            {
                SlikaUcitavanje.Content = "Ucitavanje...";
                SlikaUcitavanje.Foreground = Brushes.DarkRed;               
            }
          
        }

        private void ucitajButtom_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SlikaUcitavanje.Content.Equals("Ucitavanje..."))
            {
                SlikaUcitavanje.Content = " ";
                SlikaUcitavanje.Foreground = Brushes.LightSlateGray;
            }          

        }        
       


        private void PromenaFonta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PromenaFonta.SelectedItem != null && !OpisRichTextBox.Selection.IsEmpty)
                OpisRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, PromenaFonta.SelectedItem);
        }

        private void PromenaVelicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PromenaVelicine.SelectedItem != null && !OpisRichTextBox.Selection.IsEmpty)
                OpisRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, PromenaVelicine.SelectedItem);
        }

        private void PromenaBoje_Click(object sender, RoutedEventArgs e)
        {
            F.ColorDialog colorDialog = new F.ColorDialog(); //F je naziv za Forms
            if (colorDialog.ShowDialog() == F.DialogResult.OK)
            {
                OpisRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty,
                    new SolidColorBrush(Color.FromRgb(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)));
                //dodali smo referencu za system drawing i windows forms
            }
        }
        private void OpisRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = OpisRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            Bolding.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = OpisRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            PromenaVelicine.SelectedItem = temp;
            temp = OpisRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            Underline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
            temp = OpisRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            Italic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = OpisRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            PromenaFonta.SelectedItem = temp;
          
        }

        private void ucitajButtom_Click(object sender, RoutedEventArgs e)
        {
            F.OpenFileDialog openFileDialog = new F.OpenFileDialog();
            openFileDialog.Filter = "JPEG|*.jpg|PNG|*.png";
            if (openFileDialog.ShowDialog() == F.DialogResult.OK)
            {
                linkZaSliku = openFileDialog.FileName;
                UcitanaSlika.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void OpisRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(OpisRichTextBox.Document.ContentStart, OpisRichTextBox.Document.ContentEnd);
            string[] text = textRange.Text.Split(' ', '\n');
            int brojacReci = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (!text[i].Trim().Equals(""))
                {
                    brojacReci++;
                }
            }

           
            StatusTextBlock.Text = brojacReci.ToString();
        }

        private void izadjiDodaj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
