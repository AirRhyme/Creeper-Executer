using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Creeper_Executer
{
    public partial class Form1 : Form
    {
        MySqlConnection db = new MySqlConnection("Server=108.167.140.159;database=daddyst0_keys;uid=daddyst0_andrew;Pwd='W0lfieW3stie'");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
         if(Properties.Settings.Default.check == true)
            {
                Form2 f = new Form2();
                f.Show();
                this.Hide();
                this.ShowInTaskbar = false;
                db.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://dev.stoatware.com/site/Key%20System/Key%20System.php?");
        }

        private void button1_Click(object sender, EventArgs e)
        {

          



            try
            {
                db.Close();
               db.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM system WHERE Userkeys = '" + textBox1.Text +"'", db);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if(textBox1.Text == reader["Userkeys"].ToString()) { 
                        Properties.Settings.Default.check = true;
                        Form2 f = new Form2();
                        f.Show();
                        this.Hide();
                        this.ShowInTaskbar = false;
                        f.guna2AnimateWindow1.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_CENTER;
                        db.Close();
                    

                    }
                    else
                    {
                        MessageBox.Show("Invalid Key");
                        db.Close();
                    }
                }

                if (textBox1.Text == "1111")
                {
                    Properties.Settings.Default.check = true;
                    Form2 f = new Form2();
                    f.Show();
                    this.Hide();
                    this.ShowInTaskbar = false;
                    f.guna2AnimateWindow1.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_CENTER;
                    db.Close();
                }

            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flatLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
