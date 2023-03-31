using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    [Serializable]

    public class NIkePatike
    {

        public int BrojPatika { get; set; }
        public string NazivPatika { get; set; }
        public bool Cekirano { get; set; }
        public string Slika { get; set; }
        public string RTF_putanja { get; set; }
        public DateTime DatumDodavanja { get; set; }

        public NIkePatike()
            {

            }
        

        public NIkePatike(int brojPatika, string nazivPatika, bool cekirano, string slika, string rTF_putanja, DateTime datumDodavanja)
        {
            BrojPatika = brojPatika;
            NazivPatika = nazivPatika;
            Cekirano = cekirano;
            Slika = slika;
            RTF_putanja = rTF_putanja;
            DatumDodavanja = datumDodavanja;
        }
    }
}
