using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblok
{
    public partial class frm_ssblok : Form
    {
        Timer tmr_yarat;
        Timer tmr_skor;
        Label lbl_skor;
        PictureBox pcr_blok;
        public frm_ssblok()
        {
            InitializeComponent();

            //####### Form #####
            this.Size = new System.Drawing.Size(600, 654);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPress += new KeyPressEventHandler(frm_ssblok_KeyPress);
            //##################

            //####### Timerlar #######
            tmr_skor = new Timer();
            tmr_yarat = new Timer();
            tmr_yarat.Interval = 500; //Blok yaratma hızı
            tmr_skor.Interval = 100; //skor labelinin güncellenme hızı
            tmr_yarat.Tick += new EventHandler(tmr_yarat_Tick);
            tmr_skor.Tick += new EventHandler(tmr_skor_Tick);
            //########################

            //####### PictureBox (mavi kutu) ####
            pcr_blok = new PictureBox();
            pcr_blok.Size = new Size(120, 50); //Mavi kutu boyutu
            pcr_blok.Location = new Point(this.Height - (pcr_blok.Size.Height * 8), (this.Height - (pcr_blok.Height / 2)) - pcr_blok.Size.Height); //mavi kutunun yeri
            pcr_blok.BackColor = Color.CornflowerBlue;
            this.Controls.Add(pcr_blok);
            //######################

            //####### Label (skor) #######
            lbl_skor = new Label();
            lbl_skor.Text = "Skor: 0";
            lbl_skor.Location = new Point(this.Width - lbl_skor.Size.Width, 20);
            this.Controls.Add(lbl_skor);
            //#####################

            tmr_yarat.Start();
            tmr_skor.Start();
        }

        Random renk = new Random();
        void BlokYarat()
        {
            Color[] renkler = { Color.LightCoral, Color.CornflowerBlue };
            PictureBox pcr_dusm = new PictureBox();
            pcr_dusm.BackColor = renkler[renk.Next(renkler.Length)]; //rastgele renkli bloklar
            pcr_dusm.Size = new Size(30, 30); //hareketli kutunun boyutu
            Random yer = new Random();
            pcr_dusm.Location = new Point(yer.Next(0, this.Width - pcr_blok.Size.Width), (pcr_blok.Size.Width * 2) * -1); //hareketli  kutuların rastgele yerlerden çıkartma

            TimerBlok hareket = new TimerBlok();
            hareket.Interval = 300; //hareketlerin tekrarlanma sıklığı
            hareket.Tick += new EventHandler((sender, e) => hareket_Tick(sender, e, pcr_dusm)); //hareketli kutunun bilgisini metoda aktarma
            this.Controls.Add(pcr_dusm);
            hareket.Start();
        }

        void hareket_Tick(object sender, EventArgs e, PictureBox pcr)
        {
            //Çarpma kontrolü
            float yerX = Math.Abs((((PictureBox)pcr).Left + (((PictureBox)pcr).Width / 2)) - (pcr_blok.Left + (pcr_blok.Width / 2)));
            float yerY = Math.Abs((((PictureBox)pcr).Top + ((((PictureBox)pcr).Height / 2)) - (pcr_blok.Top + (pcr_blok.Height / 2))));

            float farkGenislik = ((((PictureBox)pcr).Width / 2) + (pcr_blok.Width / 2));
            float farkYukselik = ((((PictureBox)pcr).Height / 2) + (pcr_blok.Height / 2));
            //------

            if ((farkGenislik > yerX) && (farkYukselik > yerY)) //Çarpma işlemi gerçekleşirse yapılacak işlemler
            {
                if (((PictureBox)pcr).BackColor == Color.CornflowerBlue) //eğer çarpan kutunun rengi mavi ise
                {
                    skor++; //skoru arttır
                }
                else
                {
                    this.Controls.Clear(); //formu temizle
                }

                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Dispose();
                ((PictureBox)pcr).Dispose();
            }
            ((TimerBlok)sender).Don += 1; //hareketli kutunun ilerleme boyutu
            ((PictureBox)pcr).Location = new Point(((PictureBox)pcr).Location.X, ((PictureBox)pcr).Location.Y + ((TimerBlok)sender).Don);
            if (((PictureBox)pcr).Bottom - this.Bottom >= 0) //eğer hareketli kutu çarpmadan formun dışına çıkarsa yapılacak işlemler
            {
                ((PictureBox)pcr).Dispose();
                ((TimerBlok)sender).Don = 0;
                ((TimerBlok)sender).Stop();
            }
        }

        int skor;
        void tmr_yarat_Tick(object sender, EventArgs e)
        {
            BlokYarat();
        }

        void tmr_skor_Tick(object sender, EventArgs e)
        {
            lbl_skor.Text = "Skor: " + skor;
        }

        void frm_ssblok_KeyPress(object sender, KeyPressEventArgs e)
        {

                if (e.KeyChar == 'a' || e.KeyChar == 'A') //a tuşuna basılırsa sola doğru kaydır
                {
                    if (pcr_blok.Left > 0)
                        pcr_blok.Left = pcr_blok.Left - 20;
                }
                if (e.KeyChar == 'd' || e.KeyChar == 'D') // d tuşuna basılırsa sağa doğru kaydır
                {
                    if (pcr_blok.Right < this.Width)
                        pcr_blok.Left = pcr_blok.Left + 20;
                }
        }
    }
}
