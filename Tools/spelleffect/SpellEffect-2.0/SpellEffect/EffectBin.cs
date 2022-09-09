using MySql.Data.MySqlClient;
using SpellEffect.EffectInstances;
using Stump.DofusProtocol.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpellEffect
{
    public partial class EffectBin : Form
    {
        List<EffectBase> effectDataList = new List<EffectBase>();
        static bool EffectBinLoaded = false;
        byte[] buffer;
        MySqlConn mysqlCon;
        int Id_row;
        string effectBinType;

        public EffectBin()
        {
            InitializeComponent();
            EffectBinLoaded = false;
            description.Text = "";
        }

        public EffectBin(byte[] _buffer, MySqlConn _mysqlCon, int _Id, string _effectBinType)
        {
            mysqlCon = _mysqlCon;
            Id_row = _Id;
            buffer = _buffer;
            effectBinType = _effectBinType;
            EffectBinLoaded = false;
            InitializeComponent();
            EffectBinHex.Text = BitConverter.ToString(buffer).Replace("-", string.Empty);
            effectDataList = EffectManager.DeserializeEffects(buffer);
            AddTabControl();
            description.Text = "";
        }


        public void AddTabControl()
        {
            ParamsValuePanel.Controls.RemoveByKey("EffectTabControl");
            if (effectDataList.Count > 0)
            {
                TabControl tabControl = new TabControl();
                tabControl.Location = new Point(13, 11);
                tabControl.Name = "EffectTabControl";
                tabControl.SelectedIndex = 0;
                tabControl.Size = new Size(523, 298);
                tabControl.TabIndex = 0;
                tabControl.SuspendLayout();

                for (int cnt = 0; cnt < effectDataList.Count; cnt++)
                {
                    effectDataList[cnt].ParseTargets();
                    effectDataList[cnt].ParseTriggersBuff();
                    #region Designer wtf
                    // 
                    // tabPage
                    // 
                    TabPage tabPage1 = new TabPage();
                    tabPage1.Location = new Point(4, 22);
                    tabPage1.Name = "EffectPage" + cnt;
                    tabPage1.Padding = new Padding(3);
                    tabPage1.Size = new Size(515, 272);
                    tabPage1.TabIndex = cnt;
                    tabPage1.Text = "Effect " + cnt;
                    tabPage1.UseVisualStyleBackColor = true;

                    // TargetEditPB
                    // 
                    PictureBox delPB = new PictureBox();
                    delPB.Image = global::SpellEffect.Properties.Resources.del;
                    delPB.Location = new System.Drawing.Point(502, 2);
                    delPB.Name = "delPB";
                    delPB.Size = new System.Drawing.Size(15, 15);
                    delPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                    delPB.TabIndex = 0;
                    delPB.TabStop = false;
                    delPB.Cursor = Cursors.Hand;
                    delPB.Click += DelPB_Click;
                    delPB.Tag = cnt;
                    tabPage1.Controls.Add(delPB);

                    Button rightArrow = new Button();
                    rightArrow.Text = ">>";
                    rightArrow.Location = new Point(delPB.Location.X - 30, delPB.Location.Y);
                    rightArrow.Width = 30;
                    rightArrow.MouseEnter += RightArrow_MouseEnter;
                    rightArrow.MouseLeave += RightArrow_MouseLeave;
                    rightArrow.Tag = cnt;
                    rightArrow.Click += RightArrow_Click;
                    tabPage1.Controls.Add(rightArrow);
                    
                    Button leftArrow = new Button();
                    leftArrow.Text = "<<";
                    leftArrow.Location = new Point(rightArrow.Location.X - 30, delPB.Location.Y);
                    leftArrow.Width = 30;
                    leftArrow.MouseEnter += LeftArrow_MouseEnter;
                    leftArrow.MouseLeave += LeftArrow_MouseLeave;
                    leftArrow.Tag = cnt;
                    leftArrow.Click += LeftArrow_Click;
                    tabPage1.Controls.Add(leftArrow);

                    // EffectBaseLabel
                    // 
                    Label EffectBaseLabel = new Label();
                    EffectBaseLabel.AutoSize = true;
                    EffectBaseLabel.Location = new System.Drawing.Point(4, 14);
                    EffectBaseLabel.Name = "EffectBaseLabel";
                    EffectBaseLabel.Size = new System.Drawing.Size(59, 13);
                    EffectBaseLabel.TabIndex = 0;
                    EffectBaseLabel.Text = "EffectBase";
                    tabPage1.Controls.Add(EffectBaseLabel);

                    // EffectBaseTB
                    //
                    TextBox EffectBaseTB = new TextBox();
                    EffectBaseTB.Location = new System.Drawing.Point(65, 11);
                    EffectBaseTB.Name = "EffectBaseTB";
                    EffectBaseTB.Size = new System.Drawing.Size(80, 20);
                    EffectBaseTB.TabIndex = 1;
                    EffectBaseTB.Enabled = false;
                    EffectBaseTB.Text = effectDataList[cnt].GetType().ToString();
                    tabPage1.Controls.Add(EffectBaseTB);

                    // TriggersLabel
                    // 
                    Label TriggersLabel = new Label();
                    TriggersLabel.AutoSize = true;
                    TriggersLabel.Location = new System.Drawing.Point(200, 150);
                    TriggersLabel.Name = "TriggersLabel";
                    TriggersLabel.Size = new System.Drawing.Size(38, 13);
                    TriggersLabel.TabIndex = 2;
                    TriggersLabel.Text = "Triggers";
                    tabPage1.Controls.Add(TriggersLabel);

                    // TriggersTB
                    // 
                    TextBox TriggersTB = new TextBox();
                    TriggersTB.Location = new System.Drawing.Point(175, 170);
                    TriggersTB.Name = "TriggersTB";
                    TriggersTB.Size = new System.Drawing.Size(100, 20);
                    TriggersTB.TabIndex = 5;
                    TriggersTB.Text = string.Join(",", effectDataList[cnt].TriggersBuff);
                    TriggersTB.Enabled = false;
                    tabPage1.Controls.Add(TriggersTB);

                    // TriggersEditPB
                    // 
                    PictureBox TriggersEditPB = new PictureBox();
                    TriggersEditPB.Image = global::SpellEffect.Properties.Resources.Edit;
                    TriggersEditPB.Location = new System.Drawing.Point(280, 170);
                    TriggersEditPB.Name = "TriggersEditPB";
                    TriggersEditPB.Size = new System.Drawing.Size(15, 15);
                    TriggersEditPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                    TriggersEditPB.TabIndex = 0;
                    TriggersEditPB.TabStop = false;
                    TriggersEditPB.Cursor = Cursors.Hand;
                    TriggersEditPB.Click += TriggersEditPB_Click;
                    TriggersEditPB.Tag = effectDataList[cnt];
                    tabPage1.Controls.Add(TriggersEditPB);

                    // TargetLabel
                    // 
                    Label TargetLabel = new Label();
                    TargetLabel.AutoSize = true;
                    TargetLabel.Location = new System.Drawing.Point(4, 41);
                    TargetLabel.Name = "TargetLabel";
                    TargetLabel.Size = new System.Drawing.Size(38, 13);
                    TargetLabel.TabIndex = 2;
                    TargetLabel.Text = "Target";
                    tabPage1.Controls.Add(TargetLabel);

                    // TargetTB
                    // 
                    TextBox TargetTB = new TextBox();
                    TargetTB.Location = new System.Drawing.Point(65, 41);
                    TargetTB.Name = "TargetTB";
                    TargetTB.Size = new System.Drawing.Size(100, 20);
                    TargetTB.TabIndex = 5;
                    TargetTB.Text = string.Join(",", effectDataList[cnt].Targets);
                    TargetTB.Enabled = false;
                    tabPage1.Controls.Add(TargetTB);

                    // TargetEditPB
                    // 
                    PictureBox TargetEditPB = new PictureBox();
                    TargetEditPB.Image = global::SpellEffect.Properties.Resources.Edit;
                    TargetEditPB.Location = new System.Drawing.Point(170, 42);
                    TargetEditPB.Name = "TargetEditPB";
                    TargetEditPB.Size = new System.Drawing.Size(15, 15);
                    TargetEditPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                    TargetEditPB.TabIndex = 0;
                    TargetEditPB.TabStop = false;
                    TargetEditPB.Cursor = Cursors.Hand;
                    TargetEditPB.Click += TargetEditPB_Click;
                    TargetEditPB.Tag = effectDataList[cnt];
                    tabPage1.Controls.Add(TargetEditPB);

                    // IdLabel
                    // 
                    Label IdLabel = new Label();
                    IdLabel.AutoSize = true;
                    IdLabel.Location = new System.Drawing.Point(4, 78);
                    IdLabel.Name = "IdLabel";
                    IdLabel.Size = new System.Drawing.Size(16, 13);
                    IdLabel.TabIndex = 4;
                    IdLabel.Text = "Id";
                    tabPage1.Controls.Add(IdLabel);

                    // IdTB
                    // 
                    TextBox IdTB = new TextBox();
                    IdTB.Location = new System.Drawing.Point(65, 75);
                    IdTB.Name = "IdTB";
                    IdTB.Size = new System.Drawing.Size(38, 20);
                    IdTB.TabIndex = 5;
                    IdTB.Enabled = false;
                    IdTB.Text = effectDataList[cnt].Id.ToString();
                    tabPage1.Controls.Add(IdTB);

                    // IdCB
                    // 
                    ComboBox IdCB = new ComboBox();
                    IdCB.FormattingEnabled = true;
                    IdCB.Location = new System.Drawing.Point(110, 75);
                    IdCB.Name = "IdCB";
                    IdCB.Size = new System.Drawing.Size(210, 20);
                    IdCB.TabIndex = 5;
                    IdCB.DropDownStyle = ComboBoxStyle.DropDownList;
                    IdCB.SelectedIndexChanged += IdCB_SelectedIndexChanged;
                    
                    IdCB.Tag = (short)cnt;

                    string[] effectsEnum = Enum.GetNames(typeof(EffectsEnum));
                    IdCB.Items.AddRange(effectsEnum);
                    EffectBinLoaded = false;
                    IdCB.SelectedIndex = effectsEnum.ToList().IndexOf(((EffectsEnum)effectDataList[cnt].Id).ToString());
                    EffectBinLoaded = true;
                    tabPage1.Controls.Add(IdCB);

                    // DurationLabel
                    //
                    Label DurationLabel = new Label();
                    DurationLabel.AutoSize = true;
                    DurationLabel.Location = new System.Drawing.Point(4, 107);
                    DurationLabel.Name = "DurationLabel";
                    DurationLabel.Size = new System.Drawing.Size(47, 13);
                    DurationLabel.TabIndex = 6;
                    DurationLabel.Text = "Duration";
                    tabPage1.Controls.Add(DurationLabel);

                    // DurationTB
                    // 
                    TextBox DurationTB = new TextBox();
                    DurationTB.Location = new System.Drawing.Point(65, 104);
                    DurationTB.Name = "DurationTB";
                    DurationTB.Size = new System.Drawing.Size(38, 20);
                    DurationTB.TabIndex = 7;
                    DurationTB.Text = effectDataList[cnt].Duration.ToString();
                    tabPage1.Controls.Add(DurationTB);

                    // DelayLabel
                    // 
                    Label DelayLabel = new Label();
                    DelayLabel.AutoSize = true;
                    DelayLabel.Location = new System.Drawing.Point(4, 140);
                    DelayLabel.Name = "DelayLabel";
                    DelayLabel.Size = new System.Drawing.Size(34, 13);
                    DelayLabel.TabIndex = 8;
                    DelayLabel.Text = "Delay";
                    tabPage1.Controls.Add(DelayLabel);

                    // DelayTB
                    // 
                    TextBox DelayTB = new TextBox();
                    DelayTB.Location = new System.Drawing.Point(65, 137);
                    DelayTB.Name = "DelayTB";
                    DelayTB.Size = new System.Drawing.Size(38, 20);
                    DelayTB.TabIndex = 9;
                    DelayTB.Text = effectDataList[cnt].Delay.ToString();
                    tabPage1.Controls.Add(DelayTB);

                    // RandomLabel
                    // 
                    Label RandomLabel = new Label();
                    RandomLabel.AutoSize = true;
                    RandomLabel.Location = new System.Drawing.Point(4, 172);
                    RandomLabel.Name = "RandomLabel";
                    RandomLabel.Size = new System.Drawing.Size(47, 13);
                    RandomLabel.TabIndex = 10;
                    RandomLabel.Text = "Random";
                    tabPage1.Controls.Add(RandomLabel);

                    // RandomTB
                    // 
                    TextBox RandomTB = new TextBox();
                    RandomTB.Location = new System.Drawing.Point(65, 169);
                    RandomTB.Name = "RandomTB";
                    RandomTB.Size = new System.Drawing.Size(38, 20);
                    RandomTB.TabIndex = 11;
                    RandomTB.Text = effectDataList[cnt].Random.ToString();
                    tabPage1.Controls.Add(RandomTB);

                    // GroupLabel
                    //
                    Label GroupLabel = new Label();
                    GroupLabel.AutoSize = true;
                    GroupLabel.Location = new System.Drawing.Point(4, 203);
                    GroupLabel.Name = "GroupLabel";
                    GroupLabel.Size = new System.Drawing.Size(36, 13);
                    GroupLabel.TabIndex = 12;
                    GroupLabel.Text = "Group";
                    tabPage1.Controls.Add(GroupLabel);

                    // GroupTB
                    // 
                    TextBox GroupTB = new TextBox();
                    GroupTB.Location = new System.Drawing.Point(65, 200);
                    GroupTB.Name = "GroupTB";
                    GroupTB.Size = new System.Drawing.Size(38, 20);
                    GroupTB.TabIndex = 13;
                    GroupTB.Text = effectDataList[cnt].Group.ToString();
                    tabPage1.Controls.Add(GroupTB);

                    // ModificatorLabel
                    // 
                    Label ModificatorLabel = new Label();
                    ModificatorLabel.AutoSize = true;
                    ModificatorLabel.Location = new System.Drawing.Point(4, 236);
                    ModificatorLabel.Name = "ModificatorLabel";
                    ModificatorLabel.Size = new System.Drawing.Size(59, 13);
                    ModificatorLabel.TabIndex = 14;
                    ModificatorLabel.Text = "Modificator";
                    tabPage1.Controls.Add(ModificatorLabel);

                    // ModificatorTB
                    // 
                    TextBox ModificatorTB = new TextBox();
                    ModificatorTB.Location = new System.Drawing.Point(65, 233);
                    ModificatorTB.Name = "ModificatorTB";
                    ModificatorTB.Size = new System.Drawing.Size(38, 20);
                    ModificatorTB.TabIndex = 15;
                    ModificatorTB.Text = effectDataList[cnt].Modificator.ToString();
                    tabPage1.Controls.Add(ModificatorTB);

                    //////////////////////////////////// 2eme colone ////////////////////////////
                    // TriggerLabel
                    // 
                    Label TriggerLabel = new Label();
                    TriggerLabel.AutoSize = true;
                    TriggerLabel.Location = new System.Drawing.Point(330, 28);
                    TriggerLabel.Name = "TriggerLabel";
                    TriggerLabel.Size = new System.Drawing.Size(40, 13);
                    TriggerLabel.TabIndex = 0;
                    TriggerLabel.Text = "Trigger";
                    tabPage1.Controls.Add(TriggerLabel);

                    // TriggerTB
                    // 
                    TextBox TriggerTB = new TextBox();
                    TriggerTB.Location = new System.Drawing.Point(414, 25);
                    TriggerTB.Name = "TriggerTB";
                    TriggerTB.Size = new System.Drawing.Size(47, 20);
                    TriggerTB.TabIndex = 1;
                    TriggerTB.Text = effectDataList[cnt].Trigger.ToString();
                    tabPage1.Controls.Add(TriggerTB);

                    // HiddenLabel
                    // 
                    Label HiddenLabel = new Label();
                    HiddenLabel.AutoSize = true;
                    HiddenLabel.Location = new System.Drawing.Point(330, 59);
                    HiddenLabel.Name = "HiddenLabel";
                    HiddenLabel.Size = new System.Drawing.Size(41, 13);
                    HiddenLabel.TabIndex = 2;
                    HiddenLabel.Text = "Hidden";
                    tabPage1.Controls.Add(HiddenLabel);

                    // HiddenTB
                    // 
                    TextBox HiddenTB = new TextBox();
                    HiddenTB.Location = new System.Drawing.Point(414, 56);
                    HiddenTB.Name = "HiddenTB";
                    HiddenTB.Size = new System.Drawing.Size(47, 20);
                    HiddenTB.TabIndex = 3;
                    HiddenTB.Text = effectDataList[cnt].Hidden.ToString();
                    tabPage1.Controls.Add(HiddenTB);

                    // ZoneShapeLabel
                    // 
                    Label ZoneShapeLabel = new Label();
                    ZoneShapeLabel.AutoSize = true;
                    ZoneShapeLabel.Location = new System.Drawing.Point(330, 90);
                    ZoneShapeLabel.Name = "ZoneShapeLabel";
                    ZoneShapeLabel.Size = new System.Drawing.Size(63, 13);
                    ZoneShapeLabel.TabIndex = 4;
                    ZoneShapeLabel.Text = "ZoneShape";
                    tabPage1.Controls.Add(ZoneShapeLabel);

                    // ZoneShapeTB
                    // 
                    ComboBox ZoneShapeCB = new ComboBox();
                    ZoneShapeCB.FormattingEnabled = true;
                    ZoneShapeCB.Location = new System.Drawing.Point(414, 87);
                    ZoneShapeCB.Name = "ZoneShapeCB";
                    ZoneShapeCB.Size = new System.Drawing.Size(60, 20);
                    ZoneShapeCB.TabIndex = 5;
                    ZoneShapeCB.DropDownStyle = ComboBoxStyle.DropDownList;
                    #endregion
                    string[] spellShapeEnum = Enum.GetNames(typeof(SpellShapeEnum));
                    ZoneShapeCB.Items.AddRange(spellShapeEnum);
                    ZoneShapeCB.SelectedIndex = spellShapeEnum.ToList().IndexOf(effectDataList[cnt].ZoneShape.ToString());

                    tabPage1.Controls.Add(ZoneShapeCB);

                    // ZoneSizeLabel
                    // 
                    Label ZoneSizeLabel = new Label();
                    ZoneSizeLabel.AutoSize = true;
                    ZoneSizeLabel.Location = new System.Drawing.Point(330, 119);
                    ZoneSizeLabel.Name = "ZoneSizeLabel";
                    ZoneSizeLabel.Size = new System.Drawing.Size(52, 13);
                    ZoneSizeLabel.TabIndex = 6;
                    ZoneSizeLabel.Text = "ZoneSize";
                    tabPage1.Controls.Add(ZoneSizeLabel);


                    // ZoneSizeTB
                    // 
                    TextBox ZoneSizeTB = new TextBox();
                    ZoneSizeTB.Location = new System.Drawing.Point(414, 116);
                    ZoneSizeTB.Name = "ZoneSizeTB";
                    ZoneSizeTB.Size = new System.Drawing.Size(33, 20);
                    ZoneSizeTB.TabIndex = 7;
                    ZoneSizeTB.Text = effectDataList[cnt].ZoneSize.ToString();
                    tabPage1.Controls.Add(ZoneSizeTB);

                    // ZoneMinSizeLabel
                    // 
                    Label ZoneMinSizeLabel = new Label();
                    ZoneMinSizeLabel.AutoSize = true;
                    ZoneMinSizeLabel.Location = new System.Drawing.Point(330, 149);
                    ZoneMinSizeLabel.Name = "ZoneMinSizeLabel";
                    ZoneMinSizeLabel.Size = new System.Drawing.Size(69, 13);
                    ZoneMinSizeLabel.TabIndex = 8;
                    ZoneMinSizeLabel.Text = "ZoneMinSize";
                    tabPage1.Controls.Add(ZoneMinSizeLabel);

                    // ZoneMinSizeTB
                    // 
                    TextBox ZoneMinSizeTB = new TextBox();
                    ZoneMinSizeTB.Location = new System.Drawing.Point(414, 146);
                    ZoneMinSizeTB.Name = "ZoneMinSizeTB";
                    ZoneMinSizeTB.Size = new System.Drawing.Size(33, 20);
                    ZoneMinSizeTB.TabIndex = 9;
                    ZoneMinSizeTB.Text = effectDataList[cnt].ZoneMinSize.ToString();
                    tabPage1.Controls.Add(ZoneMinSizeTB);

                    // ValueLabel
                    // 
                    Label ValueLabel = new Label();
                    ValueLabel.AutoSize = true;
                    ValueLabel.Location = new System.Drawing.Point(330, 180);
                    ValueLabel.Name = "ValueLabel";
                    ValueLabel.Size = new System.Drawing.Size(34, 13);
                    ValueLabel.TabIndex = 10;
                    ValueLabel.Text = "Value";
                    tabPage1.Controls.Add(ValueLabel);

                    // ValueTB
                    // 
                    TextBox ValueTB = new TextBox();
                    ValueTB.Location = new System.Drawing.Point(414, 177);
                    ValueTB.Name = "ValueTB";
                    ValueTB.Size = new System.Drawing.Size(33, 20);
                    ValueTB.TabIndex = 11;
                    if (effectDataList[cnt] is EffectInteger)
                    {

                        ValueTB.Text = (effectDataList[cnt] as EffectInteger).Value.ToString();
                    }
                    tabPage1.Controls.Add(ValueTB);

                    // DicenumLabel
                    // 
                    Label DicenumLabel = new Label();
                    DicenumLabel.AutoSize = true;
                    DicenumLabel.Location = new System.Drawing.Point(330, 210);
                    DicenumLabel.Name = "DicenumLabel";
                    DicenumLabel.Size = new System.Drawing.Size(49, 13);
                    DicenumLabel.TabIndex = 12;
                    DicenumLabel.Text = "Dicenum";
                    tabPage1.Controls.Add(DicenumLabel);

                    // DicenumTB
                    // 
                    TextBox DicenumTB = new TextBox();
                    DicenumTB.Location = new System.Drawing.Point(414, 207);
                    DicenumTB.Name = "DicenumTB";
                    DicenumTB.Size = new System.Drawing.Size(33, 20);
                    DicenumTB.TabIndex = 13;
                    if (effectDataList[cnt] is EffectDice)
                    {
                        DicenumTB.Text = (effectDataList[cnt] as EffectDice).DiceNum.ToString();
                    }
                    tabPage1.Controls.Add(DicenumTB);

                    // DicefaceLabel
                    // 
                    Label DicefaceLabel = new Label();
                    DicefaceLabel.AutoSize = true;
                    DicefaceLabel.Location = new System.Drawing.Point(330, 239);
                    DicefaceLabel.Name = "DicefaceLabel";
                    DicefaceLabel.Size = new System.Drawing.Size(50, 13);
                    DicefaceLabel.TabIndex = 14;
                    DicefaceLabel.Text = "Diceface";
                    tabPage1.Controls.Add(DicefaceLabel);

                    // DicefaceTB
                    // 
                    TextBox DicefaceTB = new TextBox();
                    DicefaceTB.Location = new System.Drawing.Point(414, 236);
                    DicefaceTB.Name = "DicefaceTB";
                    DicefaceTB.Size = new System.Drawing.Size(33, 20);
                    DicefaceTB.TabIndex = 15;
                    if (effectDataList[cnt] is EffectDice)
                    {
                        DicefaceTB.Text = (effectDataList[cnt] as EffectDice).DiceFace.ToString();
                    }
                    tabPage1.Controls.Add(DicefaceTB);

                    tabControl.Controls.Add(tabPage1);
                }

                TabPage AddNewEffectPage = new TabPage();
                AddNewEffectPage.Location = new Point(4, 22);
                AddNewEffectPage.Name = "EffectPage" + effectDataList.Count;
                AddNewEffectPage.Padding = new Padding(3);
                AddNewEffectPage.Size = new Size(515, 272);
                AddNewEffectPage.TabIndex = effectDataList.Count;
                AddNewEffectPage.Text = "Add New Effect";
                AddNewEffectPage.UseVisualStyleBackColor = true;

                Button AddNewEffectBtn = new Button();
                AddNewEffectBtn.Size = new Size(200, 70);
                AddNewEffectBtn.Location = new Point((AddNewEffectPage.Size.Width / 2) - 100, (AddNewEffectPage.Size.Height / 2) - 35);
                AddNewEffectBtn.Name = "AddNewEffecrBtn";
                AddNewEffectBtn.TabIndex = effectDataList.Count + 1;
                AddNewEffectBtn.Text = "Add New Effect";
                AddNewEffectBtn.Click += AddNewEffectBtn_Click;
                AddNewEffectPage.Controls.Add(AddNewEffectBtn);

                tabControl.Controls.Add(AddNewEffectPage);
                ParamsValuePanel.Controls.Add(tabControl);
            }
        }

        private void LeftArrow_MouseLeave(object sender, EventArgs e)
        {
            description.Text = "";
        }

        private void RightArrow_MouseLeave(object sender, EventArgs e)
        {
            description.Text = "";
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            ////////////////////////////////// tabControl ///////////////////////
            object[] tabControl = ParamsValuePanel.Controls.Find("EffectTabControl", false);
            TabControl EffectTabControl = tabControl[0] as TabControl;
            int index = EffectTabControl.SelectedIndex;

            if (EffectTabControl.TabPages.Count - 1 > 0 && EffectTabControl.SelectedIndex > 0 && EffectTabControl.SelectedTab.Text != "Add New Effect")
            {
                EffectBase currentEffectData = effectDataList[index];
                effectDataList.RemoveAt(index);
                effectDataList.Insert(index - 1, currentEffectData);
                AddTabControl();
            }
        }

        private void RightArrow_Click(object sender, EventArgs e)
        {
            ////////////////////////////////// tabControl ///////////////////////
            object[] tabControl = ParamsValuePanel.Controls.Find("EffectTabControl", false);
            TabControl EffectTabControl = tabControl[0] as TabControl;
            int index = EffectTabControl.SelectedIndex;

            if (EffectTabControl.TabPages.Count - 1 > 0 && EffectTabControl.SelectedIndex < EffectTabControl.TabPages.Count - 1 && EffectTabControl.SelectedTab.Text != "Add New Effect")
            {
                var currentEffectData = effectDataList[index];
                effectDataList.RemoveAt(index);
                effectDataList.Insert(index + 1, currentEffectData);
                AddTabControl();
            }
        }

        private void RightArrow_MouseEnter(object sender, EventArgs e)
        {
            description.Text = "Déplacer l'effet en cours à droite";
        }

        private void LeftArrow_MouseEnter(object sender, EventArgs e)
        {
            description.Text = "Déplacer l'effet en cours à gauche";
        }

        private void DelPB_Click(object sender, EventArgs e)
        {
            PictureBox delPB = sender as PictureBox;
            int id = (int)delPB.Tag;
            effectDataList.RemoveAt(id);

            AddTabControl();
        }

        private void AddNewEffectBtn_Click(object sender, EventArgs e)
        {
            // add new spell effect
            ////////////////////////////////// tabControl ///////////////////////
            object[] tabControl = ParamsValuePanel.Controls.Find("EffectTabControl", false);
            TabControl EffectTabControl = tabControl[0] as TabControl;

            /////////////////////////// tabPage//////////////////////////////
            object[] tabPage = EffectTabControl.Controls.Find("EffectPage" + (EffectTabControl.TabPages.Count - 1), false);
            TabPage EffectPage = tabPage[0] as TabPage;
            EffectPage.Controls.RemoveByKey("AddNewEffecrBtn");

            EffectPage.Text = "Effect " + (EffectTabControl.TabPages.Count - 1);

            EffectDice effectData = new EffectDice(EffectsEnum.Effect_DamageEarth, 0, 0, 0);
            effectData.Delay = 0;
            effectData.Duration = 0;
            effectData.Group = 0;
            effectData.Hidden = false;
            effectData.Modificator = 0;
            effectData.Random = 0;
            effectData.Trigger = false;
            effectData.Value = 0;
            effectData.ZoneMinSize = 0;
            effectData.ZoneShape = SpellShapeEnum.P;
            effectData.ZoneSize = 0;
            //////////////////////////////////////////////////////////////////////


            effectDataList.Add(effectData);

            //////////////////////////
            // add tab's controls
            // EffectBaseLabel
            // 

            // TriggersLabel
            // 
            Label TriggersLabel = new Label();
            TriggersLabel.AutoSize = true;
            TriggersLabel.Location = new System.Drawing.Point(200, 150);
            TriggersLabel.Name = "TriggersLabel";
            TriggersLabel.Size = new System.Drawing.Size(38, 13);
            TriggersLabel.TabIndex = 2;
            TriggersLabel.Text = "Triggers";
            EffectPage.Controls.Add(TriggersLabel);

            // TriggersTB
            // 
            TextBox TriggersTB = new TextBox();
            TriggersTB.Location = new System.Drawing.Point(175, 170);
            TriggersTB.Name = "TriggersTB";
            TriggersTB.Size = new System.Drawing.Size(100, 20);
            TriggersTB.TabIndex = 5;
            TriggersTB.Text = string.Join(",", effectData.TriggersBuff);
            TriggersTB.Enabled = false;
            EffectPage.Controls.Add(TriggersTB);

            // TriggersEditPB
            // 
            PictureBox TriggersEditPB = new PictureBox();
            TriggersEditPB.Image = global::SpellEffect.Properties.Resources.Edit;
            TriggersEditPB.Location = new System.Drawing.Point(280, 170);
            TriggersEditPB.Name = "TriggersEditPB";
            TriggersEditPB.Size = new System.Drawing.Size(15, 15);
            TriggersEditPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            TriggersEditPB.TabIndex = 0;
            TriggersEditPB.TabStop = false;
            TriggersEditPB.Cursor = Cursors.Hand;
            TriggersEditPB.Click += TriggersEditPB_Click;
            TriggersEditPB.Tag = effectData;
            EffectPage.Controls.Add(TriggersEditPB);

            // TargetEditPB
            // 
            PictureBox delPB = new PictureBox();
            delPB.Image = global::SpellEffect.Properties.Resources.del;
            delPB.Location = new System.Drawing.Point(502, 2);
            delPB.Name = "delPB";
            delPB.Size = new System.Drawing.Size(15, 15);
            delPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            delPB.TabIndex = 0;
            delPB.TabStop = false;
            delPB.Cursor = Cursors.Hand;
            delPB.Click += DelPB_Click;
            delPB.Tag = EffectTabControl.TabPages.Count - 1;
            EffectPage.Controls.Add(delPB);

            Label EffectBaseLabel = new Label();
            EffectBaseLabel.AutoSize = true;
            EffectBaseLabel.Location = new System.Drawing.Point(4, 14);
            EffectBaseLabel.Name = "EffectBaseLabel";
            EffectBaseLabel.Size = new System.Drawing.Size(59, 13);
            EffectBaseLabel.TabIndex = 0;
            EffectBaseLabel.Text = "EffectBase";
            EffectPage.Controls.Add(EffectBaseLabel);

            // EffectBaseTB
            //
            TextBox EffectBaseTB = new TextBox();
            EffectBaseTB.Location = new System.Drawing.Point(65, 11);
            EffectBaseTB.Name = "EffectBaseTB";
            EffectBaseTB.Size = new System.Drawing.Size(80, 20);
            EffectBaseTB.TabIndex = 1;
            EffectBaseTB.Enabled = false;
            EffectBaseTB.Text = effectData.GetType().ToString();
            EffectPage.Controls.Add(EffectBaseTB);

            // TargetLabel
            // 
            Label TargetLabel = new Label();
            TargetLabel.AutoSize = true;
            TargetLabel.Location = new System.Drawing.Point(4, 41);
            TargetLabel.Name = "TargetLabel";
            TargetLabel.Size = new System.Drawing.Size(38, 13);
            TargetLabel.TabIndex = 2;
            TargetLabel.Text = "Target";
            EffectPage.Controls.Add(TargetLabel);

            // TargetTB
            // 
            TextBox TargetTB = new TextBox();
            TargetTB.Location = new System.Drawing.Point(65, 41);
            TargetTB.Name = "TargetTB";
            TargetTB.Size = new System.Drawing.Size(100, 20);
            TargetTB.TabIndex = 5;
            TargetTB.Text = string.Join(",", effectData.Targets);
            TargetTB.Enabled = false;
            EffectPage.Controls.Add(TargetTB);

            // TargetEditPB
            // 
            PictureBox TargetEditPB = new PictureBox();
            TargetEditPB.Image = global::SpellEffect.Properties.Resources.Edit;
            TargetEditPB.Location = new System.Drawing.Point(170, 42);
            TargetEditPB.Name = "TargetEditPB";
            TargetEditPB.Size = new System.Drawing.Size(15, 15);
            TargetEditPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            TargetEditPB.TabIndex = 0;
            TargetEditPB.TabStop = false;
            TargetEditPB.Cursor = Cursors.Hand;
            TargetEditPB.Click += TargetEditPB_Click;
            TargetEditPB.Tag = effectDataList[EffectTabControl.TabPages.Count - 1];
            EffectPage.Controls.Add(TargetEditPB);

            // IdLabel
            // 
            Label IdLabel = new Label();
            IdLabel.AutoSize = true;
            IdLabel.Location = new System.Drawing.Point(4, 78);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new System.Drawing.Size(16, 13);
            IdLabel.TabIndex = 4;
            IdLabel.Text = "Id";
            EffectPage.Controls.Add(IdLabel);

            // IdTB
            // 
            TextBox IdTB = new TextBox();
            IdTB.Location = new System.Drawing.Point(65, 75);
            IdTB.Name = "IdTB";
            IdTB.Size = new System.Drawing.Size(38, 20);
            IdTB.TabIndex = 5;
            IdTB.Enabled = false;
            IdTB.Text = effectData.Id.ToString();
            EffectPage.Controls.Add(IdTB);

            // IdCB
            // 
            ComboBox IdCB = new ComboBox();
            IdCB.FormattingEnabled = true;
            IdCB.Location = new System.Drawing.Point(110, 75);
            IdCB.Name = "IdCB";
            IdCB.Size = new System.Drawing.Size(210, 20);
            IdCB.TabIndex = 5;
            IdCB.DropDownStyle = ComboBoxStyle.DropDownList;
            IdCB.SelectedIndexChanged += IdCB_SelectedIndexChanged;

            IdCB.Tag = (short)EffectTabControl.TabPages.Count - 1;

            string[] effectsEnum = Enum.GetNames(typeof(EffectsEnum));
            IdCB.Items.AddRange(effectsEnum);
            EffectBinLoaded = false;
            IdCB.SelectedIndex = effectsEnum.ToList().IndexOf((EffectsEnum.Effect_DamageEarth).ToString());
            EffectBinLoaded = true;
            EffectPage.Controls.Add(IdCB);

            // DurationLabel
            //
            Label DurationLabel = new Label();
            DurationLabel.AutoSize = true;
            DurationLabel.Location = new System.Drawing.Point(4, 107);
            DurationLabel.Name = "DurationLabel";
            DurationLabel.Size = new System.Drawing.Size(47, 13);
            DurationLabel.TabIndex = 6;
            DurationLabel.Text = "Duration";
            EffectPage.Controls.Add(DurationLabel);

            // DurationTB
            // 
            TextBox DurationTB = new TextBox();
            DurationTB.Location = new System.Drawing.Point(65, 104);
            DurationTB.Name = "DurationTB";
            DurationTB.Size = new System.Drawing.Size(38, 20);
            DurationTB.TabIndex = 7;
            DurationTB.Text = effectData.Duration.ToString();
            EffectPage.Controls.Add(DurationTB);

            // DelayLabel
            // 
            Label DelayLabel = new Label();
            DelayLabel.AutoSize = true;
            DelayLabel.Location = new System.Drawing.Point(4, 140);
            DelayLabel.Name = "DelayLabel";
            DelayLabel.Size = new System.Drawing.Size(34, 13);
            DelayLabel.TabIndex = 8;
            DelayLabel.Text = "Delay";
            EffectPage.Controls.Add(DelayLabel);

            // DelayTB
            // 
            TextBox DelayTB = new TextBox();
            DelayTB.Location = new System.Drawing.Point(65, 137);
            DelayTB.Name = "DelayTB";
            DelayTB.Size = new System.Drawing.Size(38, 20);
            DelayTB.TabIndex = 9;
            DelayTB.Text = effectData.Delay.ToString();
            EffectPage.Controls.Add(DelayTB);

            // RandomLabel
            // 
            Label RandomLabel = new Label();
            RandomLabel.AutoSize = true;
            RandomLabel.Location = new System.Drawing.Point(4, 172);
            RandomLabel.Name = "RandomLabel";
            RandomLabel.Size = new System.Drawing.Size(47, 13);
            RandomLabel.TabIndex = 10;
            RandomLabel.Text = "Random";
            EffectPage.Controls.Add(RandomLabel);

            // RandomTB
            // 
            TextBox RandomTB = new TextBox();
            RandomTB.Location = new System.Drawing.Point(65, 169);
            RandomTB.Name = "RandomTB";
            RandomTB.Size = new System.Drawing.Size(38, 20);
            RandomTB.TabIndex = 11;
            RandomTB.Text = effectData.Random.ToString();
            EffectPage.Controls.Add(RandomTB);

            // GroupLabel
            //
            Label GroupLabel = new Label();
            GroupLabel.AutoSize = true;
            GroupLabel.Location = new System.Drawing.Point(4, 203);
            GroupLabel.Name = "GroupLabel";
            GroupLabel.Size = new System.Drawing.Size(36, 13);
            GroupLabel.TabIndex = 12;
            GroupLabel.Text = "Group";
            EffectPage.Controls.Add(GroupLabel);

            // GroupTB
            // 
            TextBox GroupTB = new TextBox();
            GroupTB.Location = new System.Drawing.Point(65, 200);
            GroupTB.Name = "GroupTB";
            GroupTB.Size = new System.Drawing.Size(38, 20);
            GroupTB.TabIndex = 13;
            GroupTB.Text = effectData.Group.ToString();
            EffectPage.Controls.Add(GroupTB);

            // ModificatorLabel
            // 
            Label ModificatorLabel = new Label();
            ModificatorLabel.AutoSize = true;
            ModificatorLabel.Location = new System.Drawing.Point(4, 236);
            ModificatorLabel.Name = "ModificatorLabel";
            ModificatorLabel.Size = new System.Drawing.Size(59, 13);
            ModificatorLabel.TabIndex = 14;
            ModificatorLabel.Text = "Modificator";
            EffectPage.Controls.Add(ModificatorLabel);

            // ModificatorTB
            // 
            TextBox ModificatorTB = new TextBox();
            ModificatorTB.Location = new System.Drawing.Point(65, 233);
            ModificatorTB.Name = "ModificatorTB";
            ModificatorTB.Size = new System.Drawing.Size(38, 20);
            ModificatorTB.TabIndex = 15;
            ModificatorTB.Text = effectData.Modificator.ToString();
            EffectPage.Controls.Add(ModificatorTB);

            //////////////////////////////////// 2eme colone ////////////////////////////
            // TriggerLabel
            // 
            Label TriggerLabel = new Label();
            TriggerLabel.AutoSize = true;
            TriggerLabel.Location = new System.Drawing.Point(330, 28);
            TriggerLabel.Name = "TriggerLabel";
            TriggerLabel.Size = new System.Drawing.Size(40, 13);
            TriggerLabel.TabIndex = 0;
            TriggerLabel.Text = "Trigger";
            EffectPage.Controls.Add(TriggerLabel);

            // TriggerTB
            // 
            TextBox TriggerTB = new TextBox();
            TriggerTB.Location = new System.Drawing.Point(414, 25);
            TriggerTB.Name = "TriggerTB";
            TriggerTB.Size = new System.Drawing.Size(47, 20);
            TriggerTB.TabIndex = 1;
            TriggerTB.Text = string.Join(",", effectData.Targets);
            EffectPage.Controls.Add(TriggerTB);

            // HiddenLabel
            // 
            Label HiddenLabel = new Label();
            HiddenLabel.AutoSize = true;
            HiddenLabel.Location = new System.Drawing.Point(330, 59);
            HiddenLabel.Name = "HiddenLabel";
            HiddenLabel.Size = new System.Drawing.Size(41, 13);
            HiddenLabel.TabIndex = 2;
            HiddenLabel.Text = "Hidden";
            EffectPage.Controls.Add(HiddenLabel);

            // HiddenTB
            // 
            TextBox HiddenTB = new TextBox();
            HiddenTB.Location = new System.Drawing.Point(414, 56);
            HiddenTB.Name = "HiddenTB";
            HiddenTB.Size = new System.Drawing.Size(47, 20);
            HiddenTB.TabIndex = 3;
            HiddenTB.Text = effectData.Hidden.ToString();
            EffectPage.Controls.Add(HiddenTB);

            // ZoneShapeLabel
            // 
            Label ZoneShapeLabel = new Label();
            ZoneShapeLabel.AutoSize = true;
            ZoneShapeLabel.Location = new System.Drawing.Point(330, 90);
            ZoneShapeLabel.Name = "ZoneShapeLabel";
            ZoneShapeLabel.Size = new System.Drawing.Size(63, 13);
            ZoneShapeLabel.TabIndex = 4;
            ZoneShapeLabel.Text = "ZoneShape";
            EffectPage.Controls.Add(ZoneShapeLabel);

            // ZoneShapeTB
            // 
            ComboBox ZoneShapeCB = new ComboBox();
            ZoneShapeCB.FormattingEnabled = true;
            ZoneShapeCB.Location = new System.Drawing.Point(414, 87);
            ZoneShapeCB.Name = "ZoneShapeCB";
            ZoneShapeCB.Size = new System.Drawing.Size(60, 20);
            ZoneShapeCB.TabIndex = 5;
            ZoneShapeCB.DropDownStyle = ComboBoxStyle.DropDownList;

            string[] spellShapeEnum = Enum.GetNames(typeof(SpellShapeEnum));
            ZoneShapeCB.Items.AddRange(spellShapeEnum);
            ZoneShapeCB.SelectedIndex = spellShapeEnum.ToList().IndexOf(effectData.ZoneShape.ToString());

            EffectPage.Controls.Add(ZoneShapeCB);

            // ZoneSizeLabel
            // 
            Label ZoneSizeLabel = new Label();
            ZoneSizeLabel.AutoSize = true;
            ZoneSizeLabel.Location = new System.Drawing.Point(330, 119);
            ZoneSizeLabel.Name = "ZoneSizeLabel";
            ZoneSizeLabel.Size = new System.Drawing.Size(52, 13);
            ZoneSizeLabel.TabIndex = 6;
            ZoneSizeLabel.Text = "ZoneSize";
            EffectPage.Controls.Add(ZoneSizeLabel);

            // ZoneSizeTB
            // 
            TextBox ZoneSizeTB = new TextBox();
            ZoneSizeTB.Location = new System.Drawing.Point(414, 116);
            ZoneSizeTB.Name = "ZoneSizeTB";
            ZoneSizeTB.Size = new System.Drawing.Size(33, 20);
            ZoneSizeTB.TabIndex = 7;
            ZoneSizeTB.Text = effectData.ZoneSize.ToString();
            EffectPage.Controls.Add(ZoneSizeTB);

            // ZoneMinSizeLabel
            // 
            Label ZoneMinSizeLabel = new Label();
            ZoneMinSizeLabel.AutoSize = true;
            ZoneMinSizeLabel.Location = new System.Drawing.Point(330, 149);
            ZoneMinSizeLabel.Name = "ZoneMinSizeLabel";
            ZoneMinSizeLabel.Size = new System.Drawing.Size(69, 13);
            ZoneMinSizeLabel.TabIndex = 8;
            ZoneMinSizeLabel.Text = "ZoneMinSize";
            EffectPage.Controls.Add(ZoneMinSizeLabel);

            // ZoneMinSizeTB
            // 
            TextBox ZoneMinSizeTB = new TextBox();
            ZoneMinSizeTB.Location = new System.Drawing.Point(414, 146);
            ZoneMinSizeTB.Name = "ZoneMinSizeTB";
            ZoneMinSizeTB.Size = new System.Drawing.Size(33, 20);
            ZoneMinSizeTB.TabIndex = 9;
            ZoneMinSizeTB.Text = effectData.ZoneMinSize.ToString();
            EffectPage.Controls.Add(ZoneMinSizeTB);

            // ValueLabel
            // 
            Label ValueLabel = new Label();
            ValueLabel.AutoSize = true;
            ValueLabel.Location = new System.Drawing.Point(330, 180);
            ValueLabel.Name = "ValueLabel";
            ValueLabel.Size = new System.Drawing.Size(34, 13);
            ValueLabel.TabIndex = 10;
            ValueLabel.Text = "Value";
            EffectPage.Controls.Add(ValueLabel);

            // ValueTB
            // 
            TextBox ValueTB = new TextBox();
            ValueTB.Location = new System.Drawing.Point(414, 177);
            ValueTB.Name = "ValueTB";
            ValueTB.Size = new System.Drawing.Size(33, 20);
            ValueTB.TabIndex = 11;
            ValueTB.Text = effectData.Value.ToString();
            EffectPage.Controls.Add(ValueTB);

            // DicenumLabel
            // 
            Label DicenumLabel = new Label();
            DicenumLabel.AutoSize = true;
            DicenumLabel.Location = new System.Drawing.Point(330, 210);
            DicenumLabel.Name = "DicenumLabel";
            DicenumLabel.Size = new System.Drawing.Size(49, 13);
            DicenumLabel.TabIndex = 12;
            DicenumLabel.Text = "Dicenum";
            EffectPage.Controls.Add(DicenumLabel);

            // DicenumTB
            // 
            TextBox DicenumTB = new TextBox();
            DicenumTB.Location = new System.Drawing.Point(414, 207);
            DicenumTB.Name = "DicenumTB";
            DicenumTB.Size = new System.Drawing.Size(33, 20);
            DicenumTB.TabIndex = 13;
            DicenumTB.Text = effectData.DiceNum.ToString();
            EffectPage.Controls.Add(DicenumTB);

            // DicefaceLabel
            // 
            Label DicefaceLabel = new Label();
            DicefaceLabel.AutoSize = true;
            DicefaceLabel.Location = new System.Drawing.Point(330, 239);
            DicefaceLabel.Name = "DicefaceLabel";
            DicefaceLabel.Size = new System.Drawing.Size(50, 13);
            DicefaceLabel.TabIndex = 14;
            DicefaceLabel.Text = "Diceface";
            EffectPage.Controls.Add(DicefaceLabel);

            // DicefaceTB
            // 
            TextBox DicefaceTB = new TextBox();
            DicefaceTB.Location = new System.Drawing.Point(414, 236);
            DicefaceTB.Name = "DicefaceTB";
            DicefaceTB.Size = new System.Drawing.Size(33, 20);
            DicefaceTB.TabIndex = 15;
            DicefaceTB.Text = effectData.DiceFace.ToString();
            EffectPage.Controls.Add(DicefaceTB);

            /////////////////////////////
            TabPage AddNewEffectPage = new TabPage();
            AddNewEffectPage.Location = new Point(4, 22);
            AddNewEffectPage.Name = "EffectPage" + effectDataList.Count;
            AddNewEffectPage.Padding = new Padding(3);
            AddNewEffectPage.Size = new Size(515, 272);
            AddNewEffectPage.TabIndex = effectDataList.Count;
            AddNewEffectPage.Text = "Add New Effect";
            AddNewEffectPage.UseVisualStyleBackColor = true;

            Button AddNewEffectBtn = new Button();
            AddNewEffectBtn.Size = new Size(200, 70);
            AddNewEffectBtn.Location = new Point((AddNewEffectPage.Size.Width / 2) - 100, (AddNewEffectPage.Size.Height / 2) - 35);
            AddNewEffectBtn.Name = "AddNewEffecrBtn";
            AddNewEffectBtn.TabIndex = effectDataList.Count + 1;
            AddNewEffectBtn.Text = "Add New Effect";
            AddNewEffectBtn.Click += AddNewEffectBtn_Click;
            AddNewEffectPage.Controls.Add(AddNewEffectBtn);

            EffectTabControl.Controls.Add(AddNewEffectPage);
        }

        private void IdCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!EffectBinLoaded)
                return;
            ComboBox cb = sender as ComboBox;
            short index = Convert.ToInt16(cb.Tag);

            ////////////////////////////////// tabControl ///////////////////////
            object[] tabControl = ParamsValuePanel.Controls.Find("EffectTabControl", false);
            if (tabControl == null)
            {
                MessageBox.Show("Erreur Interne, aucun control 'EffectTabControl' n'a été trouvé");
                return;
            }
            TabControl EffectTabControl = tabControl[0] as TabControl;

            /////////////////////////// tabPage//////////////////////////////
            object[] tabPage = EffectTabControl.Controls.Find("EffectPage" + index, false);

            TabPage EffectPage = tabPage[0] as TabPage;

            /////////////////////////////// IdTB /////////////////////////////////////
            Object[] IdTB = EffectPage.Controls.Find("IdTB", false);
            if (IdTB == null)
            {
                MessageBox.Show("Erreur Interne, pas de control 'IdTB' trouvé");
                return;
            }
            else if (IdTB.Count() == 0)
            {
                MessageBox.Show("Erreur Interne, 'IdTB' = 0");
                return;
            }
            else if (IdTB[0].GetType() != typeof(TextBox))
            {
                MessageBox.Show("Erreur Interne, 'IdTB' n'est pas un control TextBox");
                return;
            }

            TextBox _IdTB = IdTB[0] as TextBox;
            EffectsEnum ee = (EffectsEnum)Enum.Parse(typeof(EffectsEnum), cb.SelectedItem.ToString());
            _IdTB.Text = ((short)ee).ToString();
            effectDataList[index].Id = (short)ee;
        }

        private void TargetEditPB_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            EffectBase effectdata = (sender as PictureBox).Tag as EffectBase;
            TargetEditor targetEditor = new TargetEditor(ref effectdata, mysqlCon);
            targetEditor.Show();
            targetEditor.FormClosed += TargetEditor_FormClosed;
        }

        private void TriggersEditPB_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            EffectBase effectdata = (sender as PictureBox).Tag as EffectBase;
            TriggersEditor targetEditor = new TriggersEditor(ref effectdata, mysqlCon);
            targetEditor.Show();
            targetEditor.FormClosed += TargetEditor_FormClosed;
        }

        private void TargetEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
            EffectBinLoaded = false;
            AddTabControl();
            EffectBinLoaded = true;
        }


        public static byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return raw;
        }

        private void HexValue_CheckedChanged(object sender, EventArgs e)
        {
            HexValuePanel.Enabled = true;
            ParamsValuePanel.Enabled = false;
        }

        private void paramsValue_CheckedChanged(object sender, EventArgs e)
        {
            HexValuePanel.Enabled = false;
            ParamsValuePanel.Enabled = true;
        }

        private void modifyHex_Click(object sender, EventArgs e)
        {
            if (modifyHex.Text == "Modifier")
            {
                EffectBinHex.Enabled = true;
                modifyHex.Text = "Annuler";
                SaveHexa.Enabled = true;
            }
            else
            {
                EffectBinHex.Enabled = false;
                modifyHex.Text = "Modifier";
                SaveHexa.Enabled = false;
            }
        }

        private void SaveHexa_Click(object sender, EventArgs e)
        {
            try
            {
                EffectBinHex.Enabled = false;
                SaveHexa.Enabled = false;
                modifyHex.Text = "Modifier";
                byte[] hexToStr = FromHex(EffectBinHex.Text);
                buffer = hexToStr;
                mysqlCon.reader.Close();
                
                mysqlCon.cmd.CommandText = "update " + Form1.spells_levels + " set " + effectBinType + " = ?hexToStr where Id = '" + Id_row + "'";
                MySqlParameter fileContentParameter = new MySqlParameter("?hexToStr", MySqlDbType.Blob, hexToStr.Length);
                fileContentParameter.Value = hexToStr;
                mysqlCon.cmd.Parameters.Add(fileContentParameter);
                mysqlCon.reader = mysqlCon.cmd.ExecuteReader();
                mysqlCon.cmd.Parameters.Remove(fileContentParameter);
                mysqlCon.reader.Close();
                EffectManager.DeserializeEffects(buffer);
                MessageBox.Show("Modifié avec succèes");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de modifier.\n" + ex.ToString());
            }
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////
        /// </summary>

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn2_Click(object sender, EventArgs e)
        {
            ////////////////////////////////// tabControl ///////////////////////
            object[] tabControl = ParamsValuePanel.Controls.Find("EffectTabControl", false);
            if (tabControl == null)
            {
                MessageBox.Show("Erreur Interne, aucun control 'EffectTabControl' n'a été trouvé");
                return;
            }
            TabControl EffectTabControl = tabControl[0] as TabControl;
            /////////////////////////////////////////////////////////////

            for (int cnt = 0; cnt < effectDataList.Count; cnt++)
            {
                /////////////////////////// tabPage//////////////////////////////
                object[] tabPage = EffectTabControl.Controls.Find("EffectPage" + cnt, false);
                if(tabPage == null)
                {
                    MessageBox.Show("Erreur Interne, aucun control nommé 'EffectPage" + cnt + " n'a été trouvé");
                    return;
                }
                
                TabPage EffectPage = tabPage[0] as TabPage;
                ////////////////////////////////////////////////////////////

                /////////////////////////////// DurationTB /////////////////////////////////////
                Object[] DurationTB = EffectPage.Controls.Find("DurationTB", false);
                if(DurationTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'DurationTB' trouvé");
                    return;
                }
                else if(DurationTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'DurationTB' = 0");
                    return;
                }
                else if (DurationTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'DurationTB' n'est pas un control TextBox");
                    return;
                }
                int DurationTBValue;
                if(!int.TryParse((DurationTB[0] as TextBox).Text, out DurationTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur duration :'" + (DurationTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].Duration = DurationTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// DelayTB /////////////////////////////////////
                Object[] DelayTB = EffectPage.Controls.Find("DelayTB", false);
                if (DelayTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'DelayTB' trouvé");
                    return;
                }
                else if (DelayTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'DelayTB' = 0");
                    return;
                }
                else if (DelayTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'DelayTB' n'est pas un control TextBox");
                    return;
                }
                int DelayTBValue;
                if (!int.TryParse((DelayTB[0] as TextBox).Text, out DelayTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur Delay :'" + (DelayTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].Delay = DelayTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// RandomTB /////////////////////////////////////
                Object[] RandomTB = EffectPage.Controls.Find("RandomTB", false);
                if (RandomTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'RandomTB' trouvé");
                    return;
                }
                else if (RandomTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'RandomTB' = 0");
                    return;
                }
                else if (RandomTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'RandomTB' n'est pas un control TextBox");
                    return;
                }
                int RandomTBValue;
                if (!int.TryParse((RandomTB[0] as TextBox).Text, out RandomTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur Random :'" + (RandomTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].Random = RandomTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// GroupTB /////////////////////////////////////
                Object[] GroupTB = EffectPage.Controls.Find("GroupTB", false);
                if (GroupTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'GroupTB' trouvé");
                    return;
                }
                else if (GroupTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'GroupTB' = 0");
                    return;
                }
                else if (GroupTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'GroupTB' n'est pas un control TextBox");
                    return;
                }
                int GroupTBValue;
                if (!int.TryParse((GroupTB[0] as TextBox).Text, out GroupTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur Group :'" + (GroupTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].Group = GroupTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ModificatorTB /////////////////////////////////////
                Object[] ModificatorTB = EffectPage.Controls.Find("ModificatorTB", false);
                if (ModificatorTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'ModificatorTB' trouvé");
                    return;
                }
                else if (ModificatorTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'ModificatorTB' = 0");
                    return;
                }
                else if (ModificatorTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'ModificatorTB' n'est pas un control TextBox");
                    return;
                }
                int ModificatorTBValue;
                if (!int.TryParse((ModificatorTB[0] as TextBox).Text, out ModificatorTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur Modificator :'" + (ModificatorTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].Modificator = ModificatorTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// TriggerTB /////////////////////////////////////
                Object[] TriggerTB = EffectPage.Controls.Find("TriggerTB", false);
                if (TriggerTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'TriggerTB' trouvé");
                    return;
                }
                else if (TriggerTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'TriggerTB' = 0");
                    return;
                }
                else if (TriggerTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'TriggerTB' n'est pas un control TextBox");
                    return;
                }
                bool TriggerTBValue;
                if (!bool.TryParse((TriggerTB[0] as TextBox).Text, out TriggerTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur TriggerTB :'" + (TriggerTB[0] as TextBox).Text + "' en un boolean logique valide");
                    return;
                }
                effectDataList[cnt].Trigger = TriggerTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// HiddenTB /////////////////////////////////////
                Object[] HiddenTB = EffectPage.Controls.Find("HiddenTB", false);
                if (HiddenTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'HiddenTB' trouvé");
                    return;
                }
                else if (HiddenTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'HiddenTB' = 0");
                    return;
                }
                else if (HiddenTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'HiddenTB' n'est pas un control TextBox");
                    return;
                }
                bool HiddenTBValue;
                if (!bool.TryParse((HiddenTB[0] as TextBox).Text, out HiddenTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur HiddenTB :'" + (HiddenTB[0] as TextBox).Text + "' en un boolean logique valide");
                    return;
                }
                effectDataList[cnt].Hidden = HiddenTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ZoneShapeCB /////////////////////////////////////
                Object[] ZoneShapeCB = EffectPage.Controls.Find("ZoneShapeCB", false);
                if (ZoneShapeCB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'ZoneShapeCB' trouvé");
                    return;
                }
                else if (ZoneShapeCB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'ZoneShapeCB' = 0");
                    return;
                }
                else if (ZoneShapeCB[0].GetType() != typeof(ComboBox))
                {
                    MessageBox.Show("Erreur Interne, 'ZoneShapeCB' n'est pas un control TextBox");
                    return;
                }
                string ZoneShapeCBValue = (ZoneShapeCB[0] as ComboBox).SelectedItem.ToString();

                effectDataList[cnt].ZoneShape = (SpellShapeEnum)Enum.Parse(typeof(SpellShapeEnum), ZoneShapeCBValue);
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ZoneSizeTB /////////////////////////////////////
                Object[] ZoneSizeTB = EffectPage.Controls.Find("ZoneSizeTB", false);
                if (ZoneSizeTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'ZoneSizeTB' trouvé");
                    return;
                }
                else if (ZoneSizeTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'ZoneSizeTB' = 0");
                    return;
                }
                else if (ZoneSizeTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'ZoneSizeTB' n'est pas un control TextBox");
                    return;
                }
                uint ZoneSizeTBValue;
                if (!uint.TryParse((ZoneSizeTB[0] as TextBox).Text, out ZoneSizeTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur ZoneSizeTB :'" + (ZoneSizeTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].ZoneSize = ZoneSizeTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ZoneMinSizeTB /////////////////////////////////////
                Object[] ZoneMinSizeTB = EffectPage.Controls.Find("ZoneMinSizeTB", false);
                if (ZoneMinSizeTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'ZoneMinSizeTB' trouvé");
                    return;
                }
                else if (ZoneMinSizeTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'ZoneMinSizeTB' = 0");
                    return;
                }
                else if (ZoneMinSizeTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'ZoneMinSizeTB' n'est pas un control TextBox");
                    return;
                }
                uint ZoneMinSizeTBValue;
                if (!uint.TryParse((ZoneMinSizeTB[0] as TextBox).Text, out ZoneMinSizeTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur ZoneMinSizeTB :'" + (ZoneMinSizeTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                effectDataList[cnt].ZoneMinSize = ZoneMinSizeTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ValueTB /////////////////////////////////////
                Object[] ValueTB = EffectPage.Controls.Find("ValueTB", false);
                if (ValueTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'ValueTB' trouvé");
                    return;
                }
                else if (ValueTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'ValueTB' = 0");
                    return;
                }
                else if (ValueTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'ValueTB' n'est pas un control TextBox");
                    return;
                }
                int ValueTBValue;
                if (!int.TryParse((ValueTB[0] as TextBox).Text, out ValueTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur ValueTB :'" + (ValueTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                if(effectDataList[cnt] is EffectInteger)
                    (effectDataList[cnt] as EffectInteger).Value = ValueTBValue;
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ValueTB /////////////////////////////////////
                Object[] DiceNumTB = EffectPage.Controls.Find("DiceNumTB", false);
                if (ValueTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'DiceNumTB' trouvé");
                    return;
                }
                else if (DiceNumTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'DiceNumTB' = 0");
                    return;
                }
                else if (DiceNumTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'DiceNumTB' n'est pas un control TextBox");
                    return;
                }
                short DiceNumTBValue;
                if (!short.TryParse((DiceNumTB[0] as TextBox).Text, out DiceNumTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur DiceNumTB :'" + (DiceNumTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                if (effectDataList[cnt] is EffectDice)
                {
                    (effectDataList[cnt] as EffectDice).DiceNum = DiceNumTBValue;
                }
                /////////////////////////////////////////////////////////////////////////

                /////////////////////////////// ValueTB /////////////////////////////////////
                Object[] DiceFaceTB = EffectPage.Controls.Find("DiceFaceTB", false);
                if (ValueTB == null)
                {
                    MessageBox.Show("Erreur Interne, pas de control 'DiceFaceTB' trouvé");
                    return;
                }
                else if (DiceFaceTB.Count() == 0)
                {
                    MessageBox.Show("Erreur Interne, 'DiceFaceTB' = 0");
                    return;
                }
                else if (DiceFaceTB[0].GetType() != typeof(TextBox))
                {
                    MessageBox.Show("Erreur Interne, 'DiceFaceTB' n'est pas un control TextBox");
                    return;
                }
                short DiceFaceTBValue;
                if (!short.TryParse((DiceFaceTB[0] as TextBox).Text, out DiceFaceTBValue))
                {
                    MessageBox.Show("Impossible de convertir la valeur DiceFaceTB :'" + (DiceFaceTB[0] as TextBox).Text + "' en un entier numérique valide");
                    return;
                }
                if (effectDataList[cnt] is EffectDice)
                {
                    (effectDataList[cnt] as EffectDice).DiceFace = DiceFaceTBValue;
                }
                /////////////////////////////////////////////////////////////////////////
            }

            // serialisation
            var SerializedEffects = EffectManager.SerializeEffects(effectDataList);
            string trimResult = BitConverter.ToString(SerializedEffects).Replace("-", string.Empty);


            mysqlCon.cmd.CommandText = "update " + Form1.spells_levels + " set " + effectBinType + " = ?hexToStr where Id = '" + Id_row + "'";
            MySqlParameter fileContentParameter = new MySqlParameter("?hexToStr", MySqlDbType.Blob, SerializedEffects.Length);
            fileContentParameter.Value = SerializedEffects;
            mysqlCon.cmd.Parameters.Add(fileContentParameter);
            mysqlCon.reader = mysqlCon.cmd.ExecuteReader();
            mysqlCon.cmd.Parameters.Remove(fileContentParameter);
            mysqlCon.reader.Close();

            EffectBinHex.Text = trimResult;
            MessageBox.Show("Modifié avec succèe");
        }

        private void EffectBin_Load(object sender, EventArgs e)
        {
            EffectBinLoaded = true;
        }
    }
}
