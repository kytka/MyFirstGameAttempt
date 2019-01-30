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
        Hrdina Player1;
        public int delete = 0; //TODO tohle smaz
        StreamReader soubormapa;
        int velikostmapy; //pocet bloku na jedne strane ctverce mapy 
        int velikostctverecku;
        int vyska;
        int sirka;
        public List<PictureBox> SeznamPictureBoxu;

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
            return (int)(Minimum / velikostmapy);
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
            velikostmapy = 10; //TODO upravit na obecne nectvercove rozmery
            velikostctverecku = DejMiVelikostCtverceVPixelech();
            sirka = int.Parse(soubormapa.ReadLine());
            vyska = int.Parse(soubormapa.ReadLine());
            SeznamPictureBoxu = new List<PictureBox>();

            for (int y = 0; y < vyska; y++)
            {
                string radek = soubormapa.ReadLine();
                for (int x = 0; x < sirka; x++)
                {
                    char znak = radek[x];
                    Point pomocnybod = new Point();
                    pomocnybod.X = levyhornirohmapy.X + velikostctverecku*x;
                    pomocnybod.Y = levyhornirohmapy.Y + velikostctverecku*y;
                    switch (znak)
                    {
                        case '1'://Player1
                            Player1 = new Hrdina(pomocnybod, velikostctverecku);
                            this.Controls.Add(Player1);
                            break;
                        case 'H'://hardblock
                            HardBlock hardblock = new HardBlock(pomocnybod,velikostctverecku);
                            SeznamPictureBoxu.Add(hardblock);
                            this.Controls.Add(hardblock);
                            break;
                        case 'S'://softblock
                            break;
                        case 'N'://none
                            break;
                            //TODO pridat dalsi moznosti
                        default:
                            break;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) //TODO vymysli rychlost pohybu... na velkem platne bude 5px pomalych
                    //TODO mozna by bylo lepsi kdyby to cele obsluhovala nejaka funkce, ktera bude kontrolovat kam se pohybovat da a kam ne
            {
                case Keys.Up:
                    Player1.Top -= 5;
                    break;
                case Keys.Down:
                    Player1.Top += 5;
                    break;
                case Keys.Left:
                    Player1.Left -= 5;
                    break;
                case Keys.Right:
                    Player1.Left += 5;
                    break;
                default:
                    break;
            }
        }
    }

    public abstract class NepohyblivePrvky : PictureBox
    {

    }

    public abstract class PohyblivePrvky : PictureBox
    {

    }

    public class Hrdina : PohyblivePrvky
    {
        public Hrdina(Point souradnice,int velikostctverecku)
        {
            Location = souradnice;
            this.Image = Properties.Resources.Old_hero;
            this.Height = velikostctverecku;
            this.Width = velikostctverecku;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }

    public class HardBlock : NepohyblivePrvky
    {
        public HardBlock(Point souradnice,int velikostctverecku)
        {
            Location = souradnice;
            this.Image = Properties.Resources.hardblock;
            this.Height = velikostctverecku;
            this.Width = velikostctverecku;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
