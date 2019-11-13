using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace blok
{
    public partial class usr_blok : UserControl
    {
        public usr_blok()
        {
            InitializeComponent();
        }
        void BlokYarat()
        {
            Random renk = new Random();
            Color[] renkler = { Color.LightCoral, Color.WhiteSmoke, Color.Tomato, Color.Peru, Color.DarkSeaGreen, Color.PaleVioletRed, Color.Red, Color.Black, Color.Blue, Color.Yellow };
            PictureBox pcr_dusm = new PictureBox();
            pcr_dusm.BackColor = renkler[renk.Next(0, renkler.Length)];
            pcr_dusm.Size = new Size(100, 50);
            Random yer = new Random();
            pcr_dusm.Location = new Point((pcr_blok.Size.Width * 2) * -1, yer.Next(0, this.Height - pcr_blok.Size.Width));

            TimerBlok hareket = new TimerBlok();
            hareket.Interval = 100;
            hareket.Tick += new EventHandler((sender, e) => hareket_Tick(sender, e, pcr_dusm));
            this.Controls.Add(pcr_dusm);
            hareket.Start();
        } usr_giris ac = new usr_giris();
        void hareket_Tick(object sender, EventArgs e, PictureBox pcr)
        {
            float yerX = Math.Abs((((PictureBox)pcr).Left + (((PictureBox)pcr).Width / 2)) - (pcr_blok.Left + (pcr_blok.Width / 2)));
            float yerY = Math.Abs((((PictureBox)pcr).Top + ((((PictureBox)pcr).Height / 2)) - (pcr_blok.Top + (pcr_blok.Height / 2))));

            float farkGenislik = ((((PictureBox)pcr).Width / 2) + (pcr_blok.Width / 2));
            float farkYukselik = ((((PictureBox)pcr).Height / 2) + (pcr_blok.Height / 2));

            if((farkGenislik>yerX)&&(farkYukselik>yerY))
             {
                tmr_yarat.Tick -= new EventHandler(tmr_yarat_Tick);
                ((TimerBlok)sender).Stop();
                this.Controls.Clear();
                this.Dispose();
                try
                {
                    ac.Dock = DockStyle.Fill;
                    frm_blok.ActiveForm.Controls.Add(ac);
                }
                catch { }
                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Dispose();
             }

            ((PictureBox)pcr).Location = new Point((((PictureBox)pcr).Size.Width * 2) * -1 + ((TimerBlok)sender).Don, ((PictureBox)pcr).Location.Y);
            ((TimerBlok)sender).Don += 15;
            if (((PictureBox)pcr).Left - this.Right >= 0)
            {
                skor++;
                ((PictureBox)pcr).Dispose();
                ((TimerBlok)sender).Don = 0;
                ((TimerBlok)sender).Stop();
            }
        }
        private void usr_blok_Load(object sender, EventArgs e)
        {
            tmr_skor.Start();
            tmr_yarat.Start();
        }

        private void tmr_yarat_Tick(object sender, EventArgs e)
        {
            BlokYarat();
        }
        int skor = 0;
        private void tmr_skor_Tick(object sender, EventArgs e)
        {
            lbl_skor.Text = "Skor: " + skor;
        }

        private void pcr_blok_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu)
                ((PictureBox)sender).Top = e.Y + ((PictureBox)sender).Top - (ilkkonum.Y);
        }

        private void pcr_blok_MouseDown(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = true;
            ilkkonum = e.Location;
        }
        bool suruklenmedurumu = false;
        Point ilkkonum;
        private void pcr_blok_MouseUp(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = false;
        }
    }
}
