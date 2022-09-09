using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Effects.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Editeur
{
    public partial class EditionItem : Form
    {
        private List<string> m_Effects = EffectEnumToString.Convert();
        private DatabaseAccessor DBAccessor;
        private Dictionary<int, ItemTemplate> m_itemTemplates;
        private ItemTemplate itemTemplate;
        private int ItemId;
        private List<EffectBase> CopieEffect;
        private bool ActiveEventChecked;
        public EditionItem()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeBdd();
        }

        private void BtnChercher_Click(object sender, EventArgs e)
        {
            checkedListBoxEffect.Items.Clear();
            if (TxtBoxId.Text == string.Empty)
            {
                MessageBox.Show("Veuiller entre un id valide !");
                return;
            }
            int id;
            if (int.TryParse(TxtBoxId.Text, out id))
            {
                RefreshBdd();
                if (m_itemTemplates.Any(x=>x.Key == id))
                {
                    ItemId = id;
                    itemTemplate = m_itemTemplates[id];
                    DesactiveEditionValue();
                    if (itemTemplate.Effects == null)
                    {
                        itemTemplate.Effects = new List<EffectBase>();
                        itemTemplate.Effects.Add(new EffectBase());
                    }
                    AfficheEffect();
                    ActiveBouton();
                }
                else
                {
                    MessageBox.Show("il ya aucun pano avec ce id : {id}");
                    DesactiveBouton();
                }
            }
            else
            {
                TxtBoxId.Text = string.Empty;
                MessageBox.Show("Veuiller entre un id valide !");
            }
        }
        private void InitializeEvent()
        {
            checkedListBoxEffect.Click += CheckedListBoxEffect_Click;
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
                    itemTemplate.Effects.Remove(itemTemplate.Effects.FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString()));
                    DesactiveEditionValue();
                }
                else
                {
                    for (int i = 0; i < 2010; i++)
                    {
                        EffectsEnum effect = (EffectsEnum)i;
                        if (effect.ToString() == checkedListBoxEffect.Items[e.Index].ToString())
                        {
                            itemTemplate.Effects.Add(new EffectDice((short)(EffectsEnum)i, 0, 0, 0, new EffectBase()));
                            txtBoxMinValue.Text = "0";
                            txtBoxMaxValue.Text = "0";
                            txtBoxMinValue.Enabled = true;
                            txtBoxMaxValue.Enabled = true;
                            break;
                        }
                    }
                }
            }
        }

        private void CheckedListBoxEffect_Click(object sender, EventArgs e)
        {
            var m_effect = itemTemplate.Effects.FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString());
            if (m_effect != null)
            {
                txtBoxMinValue.Enabled = true;
                txtBoxMaxValue.Enabled = true;
                txtBoxMinValue.Text = (m_effect as EffectDice).DiceNum.ToString();
                txtBoxMaxValue.Text = (m_effect as EffectDice).DiceFace.ToString();
            }
            else
                DesactiveEditionValue();
        }
        private void DesactiveEditionValue()
        {
            txtBoxMinValue.Text = string.Empty;
            txtBoxMaxValue.Text = string.Empty;
            txtBoxMaxValue.Enabled = false;
            txtBoxMinValue.Enabled = false;
        }
        //[Variable(Priority = 10)]
        public static DatabaseConfiguration DatabaseConfiguration = new DatabaseConfiguration
        {
            Host = "127.0.0.1",
            DbName = "world",
            User = "root",
            Password = "",
            ProviderName = "MySql.Data.MySqlClient"
        };
        private void InitializeBdd()
        {
            DBAccessor = new DatabaseAccessor(DatabaseConfiguration);
            DBAccessor.Initialize();
            DBAccessor.OpenConnection();
            this.m_itemTemplates = DBAccessor.Database.Query<ItemTemplate>(ItemTemplateRelator.FetchQuery, new object[0]).ToDictionary((ItemTemplate entry) => entry.Id);
            foreach (WeaponTemplate current in DBAccessor.Database.Query<WeaponTemplate>(WeaponTemplateRelator.FetchQuery, new object[0]))
            {
                this.m_itemTemplates.Add(current.Id, current);
            }
        }
        public void ActiveBouton()
        {
            BtnAppliquer.Enabled = true;
            BtnSave.Enabled = true;
            BtnColler.Enabled = true;
            BtnCopie.Enabled = true;
            BtnActu.Enabled = true;
        }
        public void DesactiveBouton()
        {
            BtnAppliquer.Enabled = false;
            BtnSave.Enabled = false;
            BtnColler.Enabled = false;
            BtnCopie.Enabled = false;
            BtnActu.Enabled = false;
        }
        public void AfficheEffect()
        {
            ActiveEventChecked = false;
            checkedListBoxEffect.Items.Clear();
            foreach (var effectName in m_Effects)
            {
                checkedListBoxEffect.Items.Add(effectName, false);
            }
            if (itemTemplate.Effects.Count == 0)
            {
                ActiveEventChecked = true;
                return;
            }
            if (itemTemplate.Effects != null && itemTemplate.Effects.Any())
            {
                foreach (var effect in itemTemplate.Effects)
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
            if (itemTemplate != null)
            {
                DBAccessor.Database.Update(itemTemplate);
                MessageBox.Show("Panoplie sauvegarder avec succes !");
            }
        }

        private void BtnAppliquer_Click(object sender, EventArgs e)
        {
            if (itemTemplate.Effects.Count > 0)
            {
                short minValue = 0;
                short maxValue = 0;
                if (short.TryParse(txtBoxMinValue.Text, out minValue) && short.TryParse(txtBoxMaxValue.Text, out maxValue))
                {
                    (itemTemplate.Effects.FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString()) as EffectDice).DiceNum = minValue;
                    (itemTemplate.Effects.FirstOrDefault(x => x.EffectId.ToString() == checkedListBoxEffect.SelectedItem.ToString()) as EffectDice).DiceFace = maxValue;
                }
                else
                    MessageBox.Show("la valeur entrer n'est pas correct !");
            }
        }

        private void BtnCopie_Click(object sender, EventArgs e)
        {
            CopieEffect = itemTemplate.Effects;
        }

        private void BtnColler_Click(object sender, EventArgs e)
        {
            if (CopieEffect != null)
            {
                itemTemplate.Effects.Clear();
                foreach (var effect in CopieEffect)
                    itemTemplate.Effects.Add(effect);
            }
            else
                MessageBox.Show("aucun valeur sauvegarder !");
        }
        public void RefreshBdd()
        {
            this.m_itemTemplates = DBAccessor.Database.Query<ItemTemplate>(ItemTemplateRelator.FetchQuery, new object[0]).ToDictionary((ItemTemplate entry) => entry.Id);
            foreach (WeaponTemplate current in DBAccessor.Database.Query<WeaponTemplate>(WeaponTemplateRelator.FetchQuery, new object[0]))
            {
                this.m_itemTemplates.Add(current.Id, current);
            }
        }
        private void BtnActu_Click(object sender, EventArgs e)
        {
            RefreshBdd();
            itemTemplate = m_itemTemplates[ItemId];
            AfficheEffect();
            DesactiveEditionValue();
        }

        private void checkedListBoxEffect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void EditionItem_Closed(object sender, EventArgs e)
        {
            Form1 Form = new Form1();
            Form.Show();
        }
    }
}
