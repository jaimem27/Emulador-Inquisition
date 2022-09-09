using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace SpellEffect
{
    public partial class Form1 : Form
    {
        MySqlConn mysqlCon;
        public static string userDB;
        public static string passwordDB;
        public static string adresseDB;
        public static string DBName;
        public static string spells_levels = "spells_levels";
        public static string clientVersion;
        System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();

        public Form1()
        {
            InitializeComponent();
        }


        private void connection_Click(object sender, EventArgs e)
        {
            if (username.Text == "")
            {
                MessageBox.Show("Entrer le nom d'utilisateur");
                return;
            }
            else if (DB.Text == "")
            {
                MessageBox.Show("Entrer la Base de donné");
                return;
            }
            else if (Ip.Text == "")
            {
                MessageBox.Show("Entrer l'adresse Ip");
                return;
            }


            MySqlConn mysqlCon = new MySqlConn(username.Text, Password.Text, DB.Text, Ip.Text);
            
            if(mysqlCon.conn.State == ConnectionState.Open)
            {
                userDB = username.Text;
                passwordDB = Password.Text;
                adresseDB = Ip.Text;
                DBName = DB.Text;
                this.Hide();

                SpellEffect spellEffect = new SpellEffect(mysqlCon);
                spellEffect.Show();
                spellEffect.FormClosed += SpellEffect_FormClosed;
            }
        }

        private void SpellEffect_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mysqlCon != null && mysqlCon.conn.State == ConnectionState.Open)
            {
                mysqlCon.conn.Close();
                mysqlCon.Dispose();
                mysqlCon = null;
            }
        }

        private void username_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                connection_Click(null, null);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            this.Enabled = false;
            about.FormClosed += About_FormClosed;
        }

        private void About_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }

    }
}
