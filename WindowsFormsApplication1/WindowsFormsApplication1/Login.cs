using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SQLiteConnection conn = new SQLiteConnection
                (@"Data Source=C:\Users\MENGX\Desktop\sqlite-autoconf-3260000\Test.db;Version=3");
        private void button1_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            string sql = "select * from User where User = '" + txtUser.Text + "' and Pass = '" + txtPass.Text + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Passs", "Info");
                Main f1 = new Main();
                f1.Show();
            }
            else
            {
                MessageBox.Show("Wrong Password", "Info");
            }
            dr.Close();
            conn.Close();
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Module module = new Module();
            module.Connect();
        }
    }
}