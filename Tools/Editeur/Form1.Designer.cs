namespace Editeur
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BtnEditeurItem = new System.Windows.Forms.Button();
            this.BtnEditeurPanoplie = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnEditeurItem
            // 
            this.BtnEditeurItem.Location = new System.Drawing.Point(206, 62);
            this.BtnEditeurItem.Name = "BtnEditeurItem";
            this.BtnEditeurItem.Size = new System.Drawing.Size(219, 23);
            this.BtnEditeurItem.TabIndex = 0;
            this.BtnEditeurItem.Text = "Edité un item";
            this.BtnEditeurItem.UseVisualStyleBackColor = true;
            this.BtnEditeurItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnEditeurPanoplie
            // 
            this.BtnEditeurPanoplie.Location = new System.Drawing.Point(206, 115);
            this.BtnEditeurPanoplie.Name = "BtnEditeurPanoplie";
            this.BtnEditeurPanoplie.Size = new System.Drawing.Size(219, 23);
            this.BtnEditeurPanoplie.TabIndex = 1;
            this.BtnEditeurPanoplie.Text = "Item une panoplie";
            this.BtnEditeurPanoplie.UseVisualStyleBackColor = true;
            this.BtnEditeurPanoplie.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(621, 334);
            this.Controls.Add(this.BtnEditeurPanoplie);
            this.Controls.Add(this.BtnEditeurItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Editor 2.4X";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnEditeurItem;
        private System.Windows.Forms.Button BtnEditeurPanoplie;
    }
}

