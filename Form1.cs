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
    public partial class Form1 : Form
    {
        List<Ugyfel> ugyfelList = new List<Ugyfel>();
        List<Jelmez> jelmezList = new List<Jelmez>();
        public List<int> asd = new List<int>();
        public static List<Kolcsonzesek> kolcsonzesList = new List<Kolcsonzesek>();
        int[] vasarlasAdatokTomb = new int[5];
        List<int[]> vasarlasAdatokList = new List<int[]>();
        int selecteditem = -1;
        
        
        public Form1()
        {
            InitializeComponent();
            kolcsonzesList.Clear();
            listBox1.Items.Clear();
            StreamReader sr = new StreamReader("jelmezek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                bool foglalt = bool.Parse(values[4]);
                if (foglalt == false)
                {
                    listBox1.Items.Add(values[0]);
                }
                
            }
            sr.Close();
        }

        //mégse gomb
        private void button2_Click(object sender, EventArgs e)
        {
            Kezdolap kezdolap = new Kezdolap(); kezdolap.Show(); this.Hide();
        }

        // inditas gomb
        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("ugyfelek.txt", true);
            int ev = DateTime.Now.Year;
            int honap = DateTime.Now.Month;
            int nap = DateTime.Now.Day;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                
                
                sw.WriteLine($"{textBox1.Text};{textBox2.Text};{textBox3.Text};{textBox4.Text}");
                sw.Flush(); sw.Close();
                sw = new StreamWriter("kolcsonzesek.txt", true);
                sw.Write($"{textBox1.Text};{listBox1.Items[selecteditem]};{numericUpDown2.Value};{ev};{honap};{nap};{numericUpDown1.Value}");
                

            }
            sw.Flush(); sw.Close();

            StreamReader sr = new StreamReader("ugyfelek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                Ugyfel ugyfelObject = new Ugyfel(values[0], values[1], values[2], values[3]);
                ugyfelList.Add(ugyfelObject);
            }
            sr.Close();
            sr = new StreamReader("jelmezek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                Jelmez jelmezObject = new Jelmez(values[0], int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), bool.Parse(values[4]));
                jelmezList.Add(jelmezObject);
            }
            sr.Close();
            sr = new StreamReader("kolcsonzesek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] values = line.Split(';');
                for (int i = 2; i < 7; i++)
                {
                    vasarlasAdatokTomb[i-2] = int.Parse(values[i]);
                }
                vasarlasAdatokList.Add(vasarlasAdatokTomb);
            }
            sr.Close();

            int maradt = vasarlasAdatokList.Count-1;
            int[] megvan = new int[vasarlasAdatokList.Count];
            for (int i = 0; i < megvan.Length; i++)
            {
                megvan[i] = -1;
            }

            int vasarlasSorszam = 0;
            

            while (maradt != 0)
            {
                sr = new StreamReader("kolcsonzesek.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(';');
                    bool igaz = false;
                    foreach (var item in ugyfelList)
                    {
                        if (item.nev == values[0] && megvan[vasarlasSorszam] != vasarlasSorszam)
                        {
                            foreach (var item2 in jelmezList)
                            {
                                if (item2.jelmezNev == values[1])
                                {
                                    Kolcsonzesek kolcsonzesek = new Kolcsonzesek(vasarlasAdatokList[vasarlasSorszam][0], vasarlasAdatokList[vasarlasSorszam][1], vasarlasAdatokList[vasarlasSorszam][2], vasarlasAdatokList[vasarlasSorszam][3], vasarlasAdatokList[vasarlasSorszam][4], item2, item);
                                    kolcsonzesList.Add(kolcsonzesek);
                                    igaz = true;
                                    megvan[vasarlasSorszam] = vasarlasSorszam;
                                    vasarlasSorszam = 0;
                                    maradt -= 1;                                    
                                    break;
                                }
                                else
                                {
                                    igaz = false;
                                }                                
                            }
                            break;
                        }
                        else
                        {
                            igaz = false;
                        }

                        
                    }
                    if (igaz == false)
                    {
                        vasarlasSorszam++;
                    }
                    else
                    {
                        break;
                    }

                }
                sr.Close();


            }

            List<string> lineList = new List<string>();
            if (listBox1.SelectedItem != null)
            {
                bool megtalalta = false;
                int sorszam = 0;
                sr = new StreamReader("jelmezek.txt");


                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(';');
                    lineList.Add(line);
                    if (values[0] == listBox1.SelectedItem.ToString())
                    {
                        megtalalta = true;


                        lineList[sorszam] = $"{jelmezList[sorszam].jelmezNev};{jelmezList[sorszam].napiAr};{jelmezList[sorszam].minMeret};{jelmezList[sorszam].maxMeret};true";



                    }
                    if (!megtalalta)
                    {
                        sorszam++;
                    }
                }
                sr.Close();

                sw = new StreamWriter("jelmezek.txt", false);
                for (int i = 0; i < lineList.Count; i++)
                {
                    sw.WriteLine(lineList[i]);
                    sw.Flush();
                }
                sw.Close();
            }

            
            
            Táblázat táblázat = new Táblázat(); táblázat.Show(); this.Hide();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            selecteditem = listBox1.SelectedIndex;
        }
    }
}
