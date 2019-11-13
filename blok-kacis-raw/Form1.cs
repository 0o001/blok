using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sblok
{
    public partial class frm_sblok : Form
    {
        Timer tmr_yarat;
        Timer tmr_skor;
        Label lbl_skor;
        PictureBox pcr_blok;
        public frm_sblok()
        { 
            InitializeComponent();

            //####### Form #####
            this.Size = new System.Drawing.Size(700, 500);
            this.BackColor = Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
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
            pcr_blok.Size = new Size(50, 50); //Mavi kutu boyutu
            pcr_blok.Location = new Point(this.Width - (pcr_blok.Size.Width*3), (this.Height / 2) - pcr_blok.Size.Height); //mavi kutunun yeri
            pcr_blok.BackColor = Color.CornflowerBlue;
            pcr_blok.Cursor = Cursors.Hand;
            //Mavi kutunun hareket ettirilmesi
            pcr_blok.MouseUp += new MouseEventHandler(pcr_blok_MouseUp);
            pcr_blok.MouseDown += new MouseEventHandler(pcr_blok_MouseDown);
            pcr_blok.MouseMove += new MouseEventHandler(pcr_blok_MouseMove);
            //----
            this.Controls.Add(pcr_blok);
            //######################

            //####### Label (skor) #######
            lbl_skor = new Label();
            lbl_skor.Text = "Skor: 0";
            lbl_skor.Location = new Point(this.Width-lbl_skor.Size.Width, 20);
            this.Controls.Add(lbl_skor);
            //#####################

            tmr_skor.Start();
            tmr_yarat.Start();
        }

        void BlokYarat()
        {
            Random renk = new Random();
            Color[] renkler = { Color.LightCoral, Color.LightGray, Color.Tomato, Color.Peru, Color.DarkSeaGreen, Color.PaleVioletRed, Color.Red, Color.Black, Color.Blue, Color.Yellow };
            PictureBox pcr_dusm = new PictureBox();
            pcr_dusm.BackColor = renkler[renk.Next(0, renkler.Length)]; //rastgele renkli bloklar
            pcr_dusm.Size = new Size(100, 50); //hareketli kutunun boyutu
            Random yer = new Random();
            pcr_dusm.Location = new Point((pcr_blok.Size.Width * 2) * -1, yer.Next(0, this.Height - pcr_blok.Size.Width)); //hareketli  kutuların rastgele yerlerden çıkartma
    
            TimerBlok hareket = new TimerBlok();
            hareket.Interval = 100; //hareketlerin tekrarlanma sıklığı
            hareket.Tick += new EventHandler((sender, e) => hareket_Tick(sender, e, pcr_dusm)); //hareketli kutunun bilgisini metoda aktarma
            this.Controls.Add(pcr_dusm);
            hareket.Start();
        }

        int skor;
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
                tmr_yarat.Tick -= new EventHandler(tmr_yarat_Tick); 
                ((TimerBlok)sender).Stop();
                this.Controls.Clear();
                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Dispose();
            }

            ((PictureBox)pcr).Location = new Point((((PictureBox)pcr).Size.Width * 2) * -1 + ((TimerBlok)sender).Don, ((PictureBox)pcr).Location.Y);
            ((TimerBlok)sender).Don += 15; //hareketli kutunun ilerleme boyutu

            if (((PictureBox)pcr).Left - this.Right >= 0) //eğer hareketli kutu çarpmadan formun dışına çıkarsa yapılacak işlemler
            {
                skor++; //skoru arttır
                ((PictureBox)pcr).Dispose();
                ((TimerBlok)sender).Don = 0;
                ((TimerBlok)sender).Stop();
            }
        }
        void tmr_yarat_Tick(object sender, EventArgs e)
        {
                BlokYarat();
        }

        void tmr_skor_Tick(object sender, EventArgs e)
        {
            lbl_skor.Text = "Skor: " + skor;
        }

        void pcr_blok_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu)
                ((PictureBox)sender).Top = e.Y + ((PictureBox)sender).Top - (ilkkonum.Y);
        }

        void pcr_blok_MouseDown(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = true;
            ilkkonum = e.Location;
        }

        bool suruklenmedurumu = false;
        Point ilkkonum;
        void pcr_blok_MouseUp(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = false;
        }
    }
}