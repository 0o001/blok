namespace blok
{
    partial class usr_giris
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_oyna = new System.Windows.Forms.Button();
            this.btn_ses = new System.Windows.Forms.Button();
            this.pcr_baslik = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcr_baslik)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_oyna
            // 
            this.btn_oyna.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_oyna.BackColor = System.Drawing.Color.LightCoral;
            this.btn_oyna.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_oyna.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_oyna.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_oyna.ForeColor = System.Drawing.Color.White;
            this.btn_oyna.Location = new System.Drawing.Point(153, 216);
            this.btn_oyna.Name = "btn_oyna";
            this.btn_oyna.Size = new System.Drawing.Size(230, 47);
            this.btn_oyna.TabIndex = 5;
            this.btn_oyna.Text = "Oyna";
            this.btn_oyna.UseVisualStyleBackColor = false;
            this.btn_oyna.Click += new System.EventHandler(this.btn_oyna_Click);
            // 
            // btn_ses
            // 
            this.btn_ses.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_ses.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_ses.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ses.ForeColor = System.Drawing.Color.White;
            this.btn_ses.Image = global::blok.Properties.Resources.volume;
            this.btn_ses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ses.Location = new System.Drawing.Point(331, 269);
            this.btn_ses.Name = "btn_ses";
            this.btn_ses.Size = new System.Drawing.Size(52, 48);
            this.btn_ses.TabIndex = 6;
            this.btn_ses.UseVisualStyleBackColor = false;
            this.btn_ses.Click += new System.EventHandler(this.btn_ses_Click);
            // 
            // pcr_baslik
            // 
            this.pcr_baslik.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcr_baslik.BackColor = System.Drawing.Color.White;
            this.pcr_baslik.Image = global::blok.Properties.Resources.blok;
            this.pcr_baslik.Location = new System.Drawing.Point(105, 50);
            this.pcr_baslik.Name = "pcr_baslik";
            this.pcr_baslik.Size = new System.Drawing.Size(315, 145);
            this.pcr_baslik.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pcr_baslik.TabIndex = 4;
            this.pcr_baslik.TabStop = false;
            // 
            // usr_giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btn_ses);
            this.Controls.Add(this.btn_oyna);
            this.Controls.Add(this.pcr_baslik);
            this.Name = "usr_giris";
            this.Size = new System.Drawing.Size(524, 366);
            this.Load += new System.EventHandler(this.usr_giris_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcr_baslik)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ses;
        private System.Windows.Forms.Button btn_oyna;
        private System.Windows.Forms.PictureBox pcr_baslik;
    }
}
