namespace blok
{
    partial class usr_blok
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
            this.components = new System.ComponentModel.Container();
            this.lbl_skor = new System.Windows.Forms.Label();
            this.tmr_yarat = new System.Windows.Forms.Timer(this.components);
            this.tmr_skor = new System.Windows.Forms.Timer(this.components);
            this.pcr_blok = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcr_blok)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_skor
            // 
            this.lbl_skor.AutoSize = true;
            this.lbl_skor.Font = new System.Drawing.Font("Miramonte", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_skor.Location = new System.Drawing.Point(569, 12);
            this.lbl_skor.Name = "lbl_skor";
            this.lbl_skor.Size = new System.Drawing.Size(80, 26);
            this.lbl_skor.TabIndex = 1;
            this.lbl_skor.Text = "Skor: 0";
            // 
            // tmr_yarat
            // 
            this.tmr_yarat.Interval = 600;
            this.tmr_yarat.Tick += new System.EventHandler(this.tmr_yarat_Tick);
            // 
            // tmr_skor
            // 
            this.tmr_skor.Tick += new System.EventHandler(this.tmr_skor_Tick);
            // 
            // pcr_blok
            // 
            this.pcr_blok.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pcr_blok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcr_blok.Location = new System.Drawing.Point(533, 102);
            this.pcr_blok.Name = "pcr_blok";
            this.pcr_blok.Size = new System.Drawing.Size(50, 50);
            this.pcr_blok.TabIndex = 2;
            this.pcr_blok.TabStop = false;
            this.pcr_blok.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcr_blok_MouseDown);
            this.pcr_blok.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pcr_blok_MouseMove);
            this.pcr_blok.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcr_blok_MouseUp);
            // 
            // usr_blok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pcr_blok);
            this.Controls.Add(this.lbl_skor);
            this.Name = "usr_blok";
            this.Size = new System.Drawing.Size(707, 360);
            this.Load += new System.EventHandler(this.usr_blok_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcr_blok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_skor;
        private System.Windows.Forms.PictureBox pcr_blok;
        private System.Windows.Forms.Timer tmr_yarat;
        private System.Windows.Forms.Timer tmr_skor;
    }
}
