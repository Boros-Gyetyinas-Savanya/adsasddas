using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
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

            int kolcsonzesekSzama = 0;
            sr = new StreamReader("kolcsonzesek.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                kolcsonzesekSzama += 1;
            }
            sr.Close();
            int maradt = kolcsonzesekSzama-1;
            int[] megvan = new int[kolcsonzesekSzama];
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
                                    Kolcsonzesek kolcsonzesek = new Kolcsonzesek(int.Parse(values[2]), int.Parse(values[3]), int.Parse(values[4]), int.Parse(values[5]), int.Parse(values[6]), item2, item);
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


            sw = new StreamWriter("szamla.txt", false);
            sw.WriteLine("Jelmezkölcsönzés számla");
            sw.WriteLine($"Név: {kolcsonzesList[kolcsonzesList.Count-1].ugyfel.nev}");
            sw.WriteLine($"Lakcíme: {kolcsonzesList[kolcsonzesList.Count - 1].ugyfel.cim}");
            sw.WriteLine($"Adószáma: {kolcsonzesList[kolcsonzesList.Count - 1].ugyfel.adoszam}");
            sw.WriteLine($"Email-címe: {kolcsonzesList[kolcsonzesList.Count - 1].ugyfel.email}");
            sw.WriteLine($"Kikölcsönzött jelmez: {kolcsonzesList[kolcsonzesList.Count - 1].jelmez.jelmezNev} ({kolcsonzesList[kolcsonzesList.Count - 1].jelmez.minMeret}-{kolcsonzesList[kolcsonzesList.Count - 1].jelmez.maxMeret} cm)");
            sw.WriteLine($"Megrendelt mennyiség: {kolcsonzesList[kolcsonzesList.Count - 1].db} db");
            sw.WriteLine($"Napi ára: {kolcsonzesList[kolcsonzesList.Count - 1].jelmez.napiAr} Ft");
            sw.WriteLine($"Napok száma: {kolcsonzesList[kolcsonzesList.Count - 1].napokSzama} Ft");
            sw.WriteLine($"Fizetendő összeg: {kolcsonzesList[kolcsonzesList.Count - 1].jelmez.napiAr * kolcsonzesList[kolcsonzesList.Count - 1].db * kolcsonzesList[kolcsonzesList.Count - 1].napokSzama} Ft");



            sw.Flush(); sw.Close();
            
            
            Táblázat táblázat = new Táblázat(); táblázat.Show(); this.Hide();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            selecteditem = listBox1.SelectedIndex;
        }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        //connection
        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=employee.db;Version=3;New=False;Compress=True;");
        }

        //set execute query
        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        //Add
        private void Insert()
        {
            string txtQuery = $"INSERT INTO employees (ID, Name) VALUES ('{textBox1.Text}', '{textBox2.Text}');";
            ExecuteQuery(txtQuery);
        }

        private void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from employees";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            //dataGridView1.DataSource = DT;
            sql_con.Close();
        }


        /*private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }                

        //set load DB
        

        
        //Edit
        private void button2_Click(object sender, EventArgs e)
        {
            string txtQuery = $"UPDATE employees SET Name='{textBox2.Text}' WHERE ID='{textBox1.Text}';";
            ExecuteQuery(txtQuery);
            LoadData();
        }
        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            string txtQuery = $"DELETE FROM employees WHERE ID='{textBox1.Text}';";
            ExecuteQuery(txtQuery);
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }*/
    }
}
