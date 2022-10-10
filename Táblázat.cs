using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Jelmez2
{
    public partial class Táblázat : Form
    {
        public Táblázat()
        {
            InitializeComponent();

            int ev = DateTime.Now.Year;
            int honap = DateTime.Now.Month;
            int nap= DateTime.Now.Day;

            int[] kezdoIdo = new int[Form1.kolcsonzesList.Count];
            int[] mostIdo = new int[Form1.kolcsonzesList.Count]; 
            int[] elteltIdo = new int[Form1.kolcsonzesList.Count]; 
            int[] hatraIdo = new int[Form1.kolcsonzesList.Count];
            int sorsz = 0;

            foreach (var item in Form1.kolcsonzesList)
            {
                kezdoIdo[sorsz] = item.ev * 365 + item.honap * 30 + item.nap;
                mostIdo[sorsz] = ev * 365 + honap * 30 + nap;
                elteltIdo[sorsz] = mostIdo[sorsz] - kezdoIdo[sorsz];
                hatraIdo[sorsz] = item.napokSzama - elteltIdo[sorsz];
                if (hatraIdo[sorsz] < 0)
                {


                    this.dataGridView1.Rows.Add(item.jelmez.jelmezNev, $"{item.jelmez.minMeret}-{item.jelmez.maxMeret}", item.jelmez.napiAr, item.db, item.napokSzama * item.db * item.jelmez.napiAr, item.ugyfel.nev, item.ugyfel.cim, item.ugyfel.adoszam, item.ugyfel.email, "Késés!", hatraIdo[0] * -1);
                }
                else
                {
                    dataGridView1.Rows.Add(item.jelmez.jelmezNev, $"{item.jelmez.minMeret}-{item.jelmez.maxMeret}", item.jelmez.napiAr, item.db, item.napokSzama * item.db * item.jelmez.napiAr, item.ugyfel.nev, item.ugyfel.cim, item.ugyfel.adoszam, item.ugyfel.email, hatraIdo[0], "nincs");
                }
                sorsz++;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kezdolap kezdolap = new Kezdolap(); kezdolap.Show(); this.Hide();
        }
    }
}
