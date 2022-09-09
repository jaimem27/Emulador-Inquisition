using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editeur
{
    public  partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditionItem editeur = new EditionItem();
            editeur.Show();
            //ItemTemplate templa = Singleton<ItemManager>.Instance.TryGetTemplate(5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditionPanoplie editeur = new EditionPanoplie();
            editeur.Show();
            //Application.Run(new EditionPanoplie());
        }
    }
}
