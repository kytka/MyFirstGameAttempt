using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstGameAttempt
{
    public partial class Form1 : Form
    {
        StreamReader soubormapa;
        int velikostmapy; //pocet bloku na jedne strane ctverce mapy 
        int velikostctverecku;
        int vyska;
        int sirka;

        public Form1()
        {
            InitializeComponent();
            string cestakmape = "veci_ktere_potrebuju/mapa.txt";
            soubormapa = new StreamReader(cestakmape);
            NactiMapuZeSouboru();
        }

        public static int DEJCISLO(StreamReader vstupnisoubor)
        {
            int VstupniZnak = vstupnisoubor.Read();
            int NacteneCislo = 0;
            bool Minus = false;
            while ((VstupniZnak != '-') && ((VstupniZnak > '9') || (VstupniZnak < '0')))
            {
                VstupniZnak = vstupnisoubor.Read();
            }
            if (VstupniZnak == '-')
            {
                Minus = true;
                VstupniZnak = vstupnisoubor.Read();
            }
            while ((VstupniZnak <= '9') && (VstupniZnak >= '0'))
            {
                NacteneCislo *= 10;
                NacteneCislo += (VstupniZnak - '0');
                VstupniZnak = vstupnisoubor.Read();
            }
            if (Minus == true)
            {
                NacteneCislo *= -1;
            }
            return NacteneCislo;
        }

        public int DejMiVelikostCtverceVPixelech()
        {
            int Minimum;
            if (ClientSize.Height > ClientSize.Width)
            {
                Minimum = ClientSize.Width;
            }
            else Minimum = ClientSize.Height;
            return (int)(velikostmapy / Minimum);
        }

        public Point DejMiLevyHorniRohMapy()
        {
            Point souradnice = new Point();
            if (ClientSize.Height > ClientSize.Width)
            {
                souradnice.X = 0;
                souradnice.Y = (ClientSize.Height - ClientSize.Width) / 2;
            }
            else
            {
                souradnice.X = (ClientSize.Width - ClientSize.Height) / 2;
                souradnice.Y = 0;
            }
            return souradnice;
        }

        public void NactiMapuZeSouboru()
        {
            Point levyhornirohmapy = DejMiLevyHorniRohMapy();
            velikostmapy = DEJCISLO(soubormapa);
            velikostctverecku = DejMiVelikostCtverceVPixelech();
            sirka = int.Parse(soubormapa.ReadLine());
            vyska = int.Parse(soubormapa.ReadLine());

            for (int y = 0; y < vyska; y++)
            {
                string radek = soubormapa.ReadLine();
                for (int x = 0; x < sirka; x++)
                {
                    char znak = radek[x];
                    switch (znak)
                    {
                        case '1'://Player1
                            break;
                        case 'H'://hardblock
                            break;
                        case 'S'://softblock
                            break;
                        case 'N'://none
                            break;
                            //TODO pridat dalsi moznosti bloku
                        default:
                            break;
                    }
                }
            }
        }
    }
}
