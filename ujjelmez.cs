using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Jelmez2
{
    public partial class ujjelmez : Form
    {
        public ujjelmez()
        {
            InitializeComponent();
            label4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kezdolap Kezdolap = new Kezdolap(); Kezdolap.Show(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool letezik = false;
            List<string> nevek = new List<string>();
            StreamReader sr = new StreamReader("jelmezek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                string nev = values[0];
                nevek.Add(nev);

            }
            sr.Close();
            for (int i = 0; i < nevek.Count; i++)
            {
                if (textBox1.Text == nevek[i]) letezik = true;
            }
            if (letezik) label4.Visible = true;
            else
            {
                StreamWriter sw = new StreamWriter("jelmezek.txt", true);
                sw.WriteLine($"{textBox1.Text};{textBox2.Text};{textBox3.Text}");
                sw.Flush(); sw.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label4.Visible = false;
            }

            
            

        }
    }
}
