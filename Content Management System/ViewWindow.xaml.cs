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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ViewWindow(Classes.NIkePatike s)
        {
            InitializeComponent();
            
            NazivPrikaz.Text = s.NazivPatika;
            VelicinaPatika.Text = s.BrojPatika.ToString();
            DatumPrikaz.Text = s.DatumDodavanja.ToString();
            Uri uri = new Uri(s.Slika);
            SlikaPrikaz.Source = new BitmapImage(uri);

            TextRange textRange;
            System.IO.FileStream fileStream;

            if (System.IO.File.Exists(s.RTF_putanja))
            {
                textRange = new TextRange(OpisRichTextBox.Document.ContentStart, OpisRichTextBox.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(s.RTF_putanja, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void izadjiPrikaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
