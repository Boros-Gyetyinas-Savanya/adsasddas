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
    public partial class Kezdolap : Form
    {
        public Kezdolap()
        {
            InitializeComponent();
            label2.Text = "Készítette: Boros Barnabás, Gyetyinás Dániel, Savanya Gergő";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); Form1.Show(); this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ujjelmez ujjelmez= new ujjelmez(); ujjelmez.Show(); this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Jelmezmod Jelmezmod = new Jelmezmod(); Jelmezmod.Show(); this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Torles Torles = new Torles(); Torles.Show(); this.Hide();
        }
    }
}
