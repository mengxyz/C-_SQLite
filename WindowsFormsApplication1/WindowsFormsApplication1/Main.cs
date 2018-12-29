using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        SQLiteConnection conn = new SQLiteConnection
                (@"Data Source=C:\Users\MENGX\Desktop\sqlite-autoconf-3260000\Test.db;Version=3");
        static int CheckUser(int x,string a)
        {
            SQLiteConnection conn = new SQLiteConnection
                (@"Data Source=C:\Users\MENGX\Desktop\sqlite-autoconf-3260000\Test.db;Version=3");
            conn.Open();
            string sql = "Select User from User where User = '"+a+"'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql,conn);
            SQLiteDataReader dr = sqlcmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Username ซ้ำ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }
            else
            {
                dr.Close();
                conn.Close();
                return 0;
            }
            dr.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = CheckUser(1, txtUser.Text);
            if (x == 1)
            {
                return;
            }
            int Status;
            if (rdbAdmin.Checked == true)
            {
                Status = 0;
            }
            else
            {
                Status = 1;
            }
            conn.Open();
            string sql = "insert into User values('" + txtUser.Text + "','" + txtName.Text + "','" + Status + "','" + txtPass.Text + "')";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, conn);
            sqlcmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Save Complete", "Info");
            showdata();
            txtName.Enabled = false ;
            txtUser.Enabled = false ;
            txtPass.Enabled = false ;
            rdbAdmin.Enabled = false ;
            rdbUser.Enabled = false ;
            rdbAdmin.Checked = false;
            rdbUser.Checked = false;
        }
        void showdata()
        {
            conn.Open();
            string sql = "select User,Name,cast(Status as text) as Status,Pass from User";
            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Test");
            for (int i = 0; i <= ds.Tables["Test"].Rows.Count - 1; i++ )
            {
                if (ds.Tables["Test"].Rows[i][2].ToString() == "0")
                {
                    ds.Tables["Test"].Rows[i][2] = "Admin";
                }
                else
                {
                    ds.Tables["Test"].Rows[i][2] = "User";
                }
            }
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Test";
            conn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            showdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            rdbAdmin.Enabled = true;
            rdbUser.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "delete from User where User = '"+txtUser.Text+"'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, conn);
            sqlcmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Delete Complete","info");
            showdata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUser.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPass.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Admin")
            {
                rdbAdmin.Checked = true;
            }
            else
            {
                rdbAdmin.Checked = true;
            }
        }
    }
}
