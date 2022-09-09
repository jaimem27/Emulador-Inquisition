namespace Editeur
{
    partial class EditionItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxEffect = new System.Windows.Forms.CheckedListBox();
            this.TxtBoxId = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnAppliquer = new System.Windows.Forms.Button();
            this.txtBoxMinValue = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnChercher = new System.Windows.Forms.Button();
            this.BtnColler = new System.Windows.Forms.Button();
            this.BtnCopie = new System.Windows.Forms.Button();
            this.BtnActu = new System.Windows.Forms.Button();
            this.txtBoxMaxValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "By : Momondo";
            // 
            // checkedListBoxEffect
            // 
            this.checkedListBoxEffect.FormattingEnabled = true;
            this.checkedListBoxEffect.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxEffect.Name = "checkedListBoxEffect";
            this.checkedListBoxEffect.Size = new System.Drawing.Size(356, 259);
            this.checkedListBoxEffect.TabIndex = 1;
            this.checkedListBoxEffect.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxEffect_SelectedIndexChanged);
            // 
            // TxtBoxId
            // 
            this.TxtBoxId.Location = new System.Drawing.Point(374, 176);
            this.TxtBoxId.Name = "TxtBoxId";
            this.TxtBoxId.Size = new System.Drawing.Size(82, 20);
            this.TxtBoxId.TabIndex = 3;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(383, 160);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(60, 13);
            this.labelId.TabIndex = 4;
            this.labelId.Text = "Id de l\'item ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Min";
            // 
            // BtnAppliquer
            // 
            this.BtnAppliquer.Enabled = false;
            this.BtnAppliquer.Location = new System.Drawing.Point(374, 131);
            this.BtnAppliquer.Name = "BtnAppliquer";
            this.BtnAppliquer.Size = new System.Drawing.Size(82, 20);
            this.BtnAppliquer.TabIndex = 6;
            this.BtnAppliquer.Text = "Appliquer";
            this.BtnAppliquer.UseVisualStyleBackColor = true;
            this.BtnAppliquer.Click += new System.EventHandler(this.BtnAppliquer_Click);
            // 
            // txtBoxMinValue
            // 
            this.txtBoxMinValue.Enabled = false;
            this.txtBoxMinValue.Location = new System.Drawing.Point(374, 105);
            this.txtBoxMinValue.Name = "txtBoxMinValue";
            this.txtBoxMinValue.Size = new System.Drawing.Size(28, 20);
            this.txtBoxMinValue.TabIndex = 7;
            // 
            // BtnSave
            // 
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(374, 228);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(82, 20);
            this.BtnSave.TabIndex = 8;
            this.BtnSave.Text = "Sauvegarder";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnChercher
            // 
            this.BtnChercher.Location = new System.Drawing.Point(374, 202);
            this.BtnChercher.Name = "BtnChercher";
            this.BtnChercher.Size = new System.Drawing.Size(82, 20);
            this.BtnChercher.TabIndex = 9;
            this.BtnChercher.Text = "Chercher";
            this.BtnChercher.UseVisualStyleBackColor = true;
            this.BtnChercher.Click += new System.EventHandler(this.BtnChercher_Click);
            // 
            // BtnColler
            // 
            this.BtnColler.Enabled = false;
            this.BtnColler.Location = new System.Drawing.Point(374, 66);
            this.BtnColler.Name = "BtnColler";
            this.BtnColler.Size = new System.Drawing.Size(82, 20);
            this.BtnColler.TabIndex = 10;
            this.BtnColler.Text = "Coller l\'effect";
            this.BtnColler.UseVisualStyleBackColor = true;
            this.BtnColler.Click += new System.EventHandler(this.BtnColler_Click);
            // 
            // BtnCopie
            // 
            this.BtnCopie.Enabled = false;
            this.BtnCopie.Location = new System.Drawing.Point(374, 40);
            this.BtnCopie.Name = "BtnCopie";
            this.BtnCopie.Size = new System.Drawing.Size(82, 20);
            this.BtnCopie.TabIndex = 11;
            this.BtnCopie.Text = "Copier l\'effect";
            this.BtnCopie.UseVisualStyleBackColor = true;
            this.BtnCopie.Click += new System.EventHandler(this.BtnCopie_Click);
            // 
            // BtnActu
            // 
            this.BtnActu.Enabled = false;
            this.BtnActu.Location = new System.Drawing.Point(374, 12);
            this.BtnActu.Name = "BtnActu";
            this.BtnActu.Size = new System.Drawing.Size(82, 20);
            this.BtnActu.TabIndex = 12;
            this.BtnActu.Text = "Actualiser";
            this.BtnActu.UseVisualStyleBackColor = true;
            this.BtnActu.Click += new System.EventHandler(this.BtnActu_Click);
            // 
            // txtBoxMaxValue
            // 
            this.txtBoxMaxValue.Enabled = false;
            this.txtBoxMaxValue.Location = new System.Drawing.Point(421, 105);
            this.txtBoxMaxValue.Name = "txtBoxMaxValue";
            this.txtBoxMaxValue.Size = new System.Drawing.Size(28, 20);
            this.txtBoxMaxValue.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Max";
            // 
            // EditionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 277);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxMaxValue);
            this.Controls.Add(this.BtnActu);
            this.Controls.Add(this.BtnCopie);
            this.Controls.Add(this.BtnColler);
            this.Controls.Add(this.BtnChercher);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.txtBoxMinValue);
            this.Controls.Add(this.BtnAppliquer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.TxtBoxId);
            this.Controls.Add(this.checkedListBoxEffect);
            this.Controls.Add(this.label1);
            this.Name = "EditionItem";
            this.Text = "EditionItem";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBoxEffect;
        private System.Windows.Forms.TextBox TxtBoxId;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnAppliquer;
        private System.Windows.Forms.TextBox txtBoxMinValue;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnChercher;
        private System.Windows.Forms.Button BtnColler;
        private System.Windows.Forms.Button BtnCopie;
        private System.Windows.Forms.Button BtnActu;
        private System.Windows.Forms.TextBox txtBoxMaxValue;
        private System.Windows.Forms.Label label3;
    }
}