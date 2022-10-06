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
    public partial class Jelmezmod : Form
    {
        public Jelmezmod()
        {
            InitializeComponent();
            label4.Visible = false;
            listBox1.Items.Clear();
            StreamReader sr = new StreamReader("jelmezek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                listBox1.Items.Add(values[0]);
            }
            sr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kezdolap kezdolap = new Kezdolap(); kezdolap.Show(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool megtalalta = false;
            int sorszam = 0;
            StreamReader sr = new StreamReader("jelmezek.txt");
            List<string> lineList = new List<string>();            

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                lineList.Add(line);
                if (values[0] == listBox1.SelectedItem.ToString())
                {
                    megtalalta = true;
                    label4.Visible = false;

                    lineList[sorszam] = $"{textBox1.Text};{textBox2.Text};{textBox3.Text}";

                    listBox1.Items[sorszam] = textBox1.Text;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";                    
                }
                if (!megtalalta)
                {
                    sorszam++;
                }
            }
            sr.Close();
            if (!megtalalta)
            {
                label4.Visible = true;
            }
            
            else
            {
                StreamWriter sw = new StreamWriter("jelmezek.txt", false);
                for (int i = 0; i < lineList.Count; i++)
                {
                    sw.WriteLine(lineList[i]);
                    sw.Flush();
                }
                sw.Close();
            }
            

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                bool megtalalta = false;
                int sorszam = 0;
                StreamReader sr = new StreamReader("jelmezek.txt");
                List<string> lineList = new List<string>();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(';');
                    lineList.Add(line);
                    if (values[0] == listBox1.SelectedItem.ToString())
                    {
                        megtalalta = true;
                        label4.Visible = false;

                        lineList[sorszam] = $"{textBox1.Text};{textBox2.Text};{textBox3.Text}";
                        textBox1.Text = values[0];
                        textBox2.Text = values[1];
                        textBox3.Text = values[2];
                        break;
                    }
                    if (!megtalalta)
                    {
                        sorszam++;
                    }
                }
                sr.Close();
            }

        }
    }
}
