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
    public class Module
    {
        public void Connect()
        {
            string patch = @"C:\Users\MENGX\Desktop\sqlite-autoconf-3260000\Test.db";
            bool IsExists = File.Exists(patch);

            if (IsExists != true)
            {
                if ((MessageBox.Show("You want to create Database", "Connect Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)) == DialogResult.OK)
                {
                    SQLiteConnection.CreateFile(@"C:\Users\MENGX\Desktop\sqlite-autoconf-3260000\Test.db");
                }
                else { 
                    MessageBox.Show("No Database File !!", "Connect Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); 
                }
            }
            else {
                MessageBox.Show("Connect Sucess !!", "Connect", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }
    }
}
