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
    public partial class Torles : Form
    {
        public Torles()
        {
            InitializeComponent();
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
            if (checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
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


                        lineList.Remove(lineList[sorszam]);


                    }
                    if (!megtalalta)
                    {
                        sorszam++;
                    }
                }
                sr.Close();

                StreamWriter sw = new StreamWriter("jelmezek.txt", false);
                for (int i = 0; i < lineList.Count; i++)
                {
                    sw.WriteLine(lineList[i]);
                    sw.Flush();
                }
                sw.Close();
                listBox1.Items.RemoveAt(sorszam);
            }
            
            
        }
    }
}
