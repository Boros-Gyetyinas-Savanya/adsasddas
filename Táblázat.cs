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

            DateTime now = new DateTime();




            foreach (var item in Form1.kolcsonzesList)
            {
                int ev = DateTime.Now.Year;
                int honap = DateTime.Now.Month;
                int nap = DateTime.Now.Day;
                int elteltIdo = DateTime.Now.Year * 365 - item.ev * 365 + DateTime.Now.Month * 30 - item.honap * 30 + DateTime.Now.Day - item.nap;
                int idoHatra = item.napokSzama - elteltIdo;

                if (idoHatra < 0)
                {
                    this.dataGridView1.Rows.Add(item.jelmez.jelmezNev, $"{item.jelmez.minMeret}-{item.jelmez.maxMeret} cm", item.jelmez.napiAr, item.db, item.napokSzama * item.db * item.jelmez.napiAr, item.ugyfel.nev, item.ugyfel.cim, item.ugyfel.adoszam, item.ugyfel.email, "Késés!", idoHatra);
                }
                else
                {
                    this.dataGridView1.Rows.Add(item.jelmez.jelmezNev, $"{item.jelmez.minMeret}-{item.jelmez.maxMeret} cm", item.jelmez.napiAr, item.db, item.napokSzama * item.db * item.jelmez.napiAr, item.ugyfel.nev, item.ugyfel.cim, item.ugyfel.adoszam, item.ugyfel.email, idoHatra, "nincs");
                }

                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kezdolap kezdolap = new Kezdolap(); kezdolap.Show(); this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
