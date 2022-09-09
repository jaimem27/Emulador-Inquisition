using MySql.Data.MySqlClient;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Effects.Instances;
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
    public partial class EditionPanoplie : Form
    {

        /// <summary>
        /// LES VARIABLES
        /// </summary>
        /// 
        private List<string> m_Effects = EffectEnumToString.Convert();
        private DatabaseAccessor DBAccessor;
        private System.Collections.Generic.Dictionary<uint, ItemSetTemplate> m_itemsSets = new System.Collections.Generic.Dictionary<uint, ItemSetTemplate>();
        private ItemSetTemplate itemSetTemplate;
        private List<EffectBase> CopieEffect;
        private int BonusIndex;
        private bool ActiveEventChecked;
        /// <summary>
        /// CONSTRUCTEUR
        /// </summary>
        public EditionPanoplie()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeBdd();
        }


        /// <summary>
        ///  LES EVENEMENTS 
        /// </summary>
        private void CheckedListBoxEffect_Click1(object sender, EventArgs e)
        {
            var m_effect = itemSetTemplate.Effects[BonusIndex].FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString());
            if (m_effect != null)
            {
                txtBoxEditionValue.Enabled = true;
                txtBoxEditionValue.Text = (m_effect as EffectInteger).Value.ToString();
            }
            else
                DesactiveEditionValue();
        }
        private void DesactiveEditionValue()
        {
            txtBoxEditionValue.Text = string.Empty;
            txtBoxEditionValue.Enabled = false;
        }
        private void BtnAddBonus_Click(object sender, EventArgs e)
        {
            if (!BtnBonus6.Enabled && itemSetTemplate.Effects != null)
            {
                itemSetTemplate.Effects.Add(new List<EffectBase>());
                ActiveBouton();
                checkedListBoxEffect.Items.Clear();
                DesactiveEditionValue();
            }
            else
                MessageBox.Show("Vous ne pouvez pas ajouer + de 6 bonus !");
        }

        private void BtnChercher_Click(object sender, EventArgs e)
        {
            DesactiveBouton();
            checkedListBoxEffect.Items.Clear();

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Veuiller entre un id valide !");
                return;
            }
            uint id;
            if (uint.TryParse(txtId.Text, out id))
            {
                itemSetTemplate = m_itemsSets[id];
                if (itemSetTemplate != null)
                {
                    if (itemSetTemplate.Effects == null)
                    {
                        itemSetTemplate.Effects = new List<List<EffectBase>>();
                        itemSetTemplate.Effects.Add(new List<EffectBase>());
                    }
                    ActiveBouton();
                }
                else
                    MessageBox.Show("il ya aucun pano avec ce id : {id}");
            }
            else
            {
                txtId.Text = string.Empty;
                MessageBox.Show("Veuiller entre un id valide !");
            }
        }

        private void BtnBonus1_Click(object sender, EventArgs e)
        {
            AfficheEffect(0);
        }

        private void BtnBonus2_Click(object sender, EventArgs e)
        {
            AfficheEffect(1);
        }

        private void BtnBonus3_Click(object sender, EventArgs e)
        {
            AfficheEffect(2);
        }

        private void BtnBonus4_Click(object sender, EventArgs e)
        {
            AfficheEffect(3);
        }

        private void BtnBonus5_Click(object sender, EventArgs e)
        {
            AfficheEffect(4);
        }

        private void BtnBonus6_Click(object sender, EventArgs e)
        {
            AfficheEffect(5);
        }




        /// <summary>
        /// LES METHODES 
        /// </summary>
        private void InitializeEvent()
        {
            checkedListBoxEffect.Click += CheckedListBoxEffect_Click1;
            checkedListBoxEffect.ItemCheck += CheckedListBoxEffect_ItemCheck;
        }

        private void CheckedListBoxEffect_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!ActiveEventChecked)
                return;
            lock (sender)
            {
                if (e.CurrentValue == CheckState.Checked)
                {
                    itemSetTemplate.Effects[BonusIndex].Remove(itemSetTemplate.Effects[BonusIndex].FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString()));
                    DesactiveEditionValue();
                }
                else
                {
                    for (int i = 0; i < 2010; i++)
                    {
                        EffectsEnum effect = (EffectsEnum)i;
                        if (effect.ToString() == checkedListBoxEffect.Items[e.Index].ToString())
                        {
                            itemSetTemplate.Effects[BonusIndex].Add(new EffectInteger((EffectsEnum)i, 0));
                            txtBoxEditionValue.Text = "0";
                            txtBoxEditionValue.Enabled = true;
                            break;
                        }
                    }
                }
            }
        }

        private void InitializeBdd()
        {
            DBAccessor = new DatabaseAccessor(EditionItem.DatabaseConfiguration);
            DBAccessor.Initialize();
            DBAccessor.OpenConnection();
            this.m_itemsSets = DBAccessor.Database.Query<ItemSetTemplate>(ItemSetTemplateRelator.FetchQuery, new object[0]).ToDictionary((ItemSetTemplate entry) => entry.Id);
        }
        public void AfficheEffect(int guid)
        {
            ActiveEventChecked = false;
            txtBoxEditionValue.Text = string.Empty;
            BonusIndex = guid;
            checkedListBoxEffect.Items.Clear();
            foreach (var effectName in m_Effects)
            {
                checkedListBoxEffect.Items.Add(effectName, false);
            }
            if (itemSetTemplate.Effects.Count == 0)
                return;
            var m_effect = itemSetTemplate.Effects[guid];
            if (m_effect != null && m_effect.Any())
            {
                foreach (var effect in m_effect)
                {
                    for (int i = 0; i < checkedListBoxEffect.Items.Count; i++)
                    {
                        object boxEffect = checkedListBoxEffect.Items[i];
                        string test = boxEffect.ToString();
                        if (boxEffect.ToString() == effect.EffectId.ToString())
                        {
                            checkedListBoxEffect.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
            ActiveEventChecked = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (itemSetTemplate != null)
            {
                DBAccessor.Database.Update(itemSetTemplate);
                MessageBox.Show("Panoplie sauvegarder avec succes !");
            }
        }

        private void BtnAppliquer_Click(object sender, EventArgs e)
        {
            if (itemSetTemplate.Effects[BonusIndex].Count > 0)
            {
                short value = 0;
                if (short.TryParse(txtBoxEditionValue.Text, out value))
                {
                    (itemSetTemplate.Effects[BonusIndex].FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString()) as EffectInteger).Value = value;
                }
                else
                    MessageBox.Show("la valeur entrer n'est pas correct !");
            }
        }

        private void ActiveBouton()
        {
            BtnBonus1.Enabled = true;
            BtnAddbonus.Enabled = true;
            BtnDeleteBonus.Enabled = true;
            BtnColler.Enabled = true;
            BtnCopie.Enabled = true;
            if (itemSetTemplate.Effects.Count > 1)
                BtnBonus2.Enabled = true;
            if (itemSetTemplate.Effects.Count > 2)
                BtnBonus3.Enabled = true;
            if (itemSetTemplate.Effects.Count > 3)
                BtnBonus4.Enabled = true;
            if (itemSetTemplate.Effects.Count > 4)
                BtnBonus5.Enabled = true;
            if (itemSetTemplate.Effects.Count > 5)
                BtnBonus6.Enabled = true;
        }
        private void DesactiveBouton()
        {
            BtnBonus1.Enabled = false;
            BtnBonus2.Enabled = false;
            BtnBonus3.Enabled = false;
            BtnBonus4.Enabled = false;
            BtnBonus5.Enabled = false;
            BtnBonus6.Enabled = false;
            BtnDeleteBonus.Enabled = false;
            BtnAddbonus.Enabled = false;
            BtnColler.Enabled = false;
            BtnCopie.Enabled = false;
        }

        private void BtnDeleteBonus_Click(object sender, EventArgs e)
        {
            if (itemSetTemplate.Effects != null && itemSetTemplate.Effects.Count > 0)
            {
                itemSetTemplate.Effects.RemoveAt(itemSetTemplate.Effects.Count - 1);
                DesactiveBouton();
                ActiveBouton();
                checkedListBoxEffect.Items.Clear();
                DesactiveEditionValue();
            }
            else
                MessageBox.Show("vous ne pouvez pas supprimer le bonus 1");
        }

        private void BtnCopie_Click(object sender, EventArgs e)
        {
            CopieEffect = itemSetTemplate.Effects[BonusIndex];
        }

        private void BtnColler_Click(object sender, EventArgs e)
        {
            if (CopieEffect != null)
            {
                itemSetTemplate.Effects[BonusIndex].Clear();
                foreach(var effect in CopieEffect)
                {
                    itemSetTemplate.Effects[BonusIndex].Add(effect);
                }
            }
            else
                MessageBox.Show("aucun valeur sauvegarder !");
        }
        private void EditionPanoplie_Closed(object sender, EventArgs e)
        {
            Form1 Form = new Form1();
            Form.Show();
        }
    }
}
