using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblokk123
{
    public partial class frm_blok : Form
    {
        PictureBox pcr_blok;
        Timer tmr_yarat;
        Timer tmr_skor;
        Label lbl_skor;
        public frm_blok()
        {
            InitializeComponent();

            //####### Form #####
            this.Size = new System.Drawing.Size(700, 600);
            this.BackColor = Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            //##################

            //####### PictureBox (mavi kutu) ####
            pcr_blok = new PictureBox();
            pcr_blok.Size = new Size(50, 50); //Mavi kutu boyutu
            pcr_blok.Location = new Point((this.Width / 2) - pcr_blok.Size.Width, (this.Height / 2) - pcr_blok.Size.Height); //mavi kutunun yeri
            pcr_blok.BackColor = Color.CornflowerBlue;
            this.Controls.Add(pcr_blok);
            //######################

            //####### Timerlar #######
            tmr_skor = new Timer();
            tmr_yarat = new Timer();
            tmr_yarat.Interval = 1500; //Blok yaratma hızı
            tmr_skor.Interval = 100; //skor labelinin güncellenme hızı
            tmr_yarat.Tick += new EventHandler(tmr_yarat_Tick);
            tmr_skor.Tick += new EventHandler(tmr_skor_Tick);
            //########################

            //####### Label (skor) #######
            lbl_skor = new Label();
            lbl_skor.Text = "Skor: 0";
            lbl_skor.Location = new Point(20, 20);
            this.Controls.Add(lbl_skor);
            //#####################

            tmr_yarat.Start();
            tmr_skor.Start();
        }

        Random yer = new Random();
        void BlokYarat()
        {
            PictureBox pcr_dusm = new PictureBox();  
            TimerBlok tmr_hareket = new TimerBlok();
            pcr_dusm.BackColor = Color.LightCoral;
            pcr_dusm.Cursor = Cursors.Hand;
            pcr_dusm.Size = new Size(50,50); //hareketli kutunun boyutu
            pcr_dusm.Click += new EventHandler((sender, e) => pcr_dusm_Click(sender, e, tmr_hareket));
            int neresi = yer.Next(4);
            if (neresi == 0) //sağ
            {
                pcr_dusm.Location = new Point((this.Right/2)+(pcr_dusm.Size.Width*3) , yer.Next(this.Height - pcr_blok.Size.Width)); //hareketli  kutuların rastgele yerlerden çıkartma
            }
            else if (neresi == 1) //sol
            {
                pcr_dusm.Location = new Point((pcr_blok.Size.Width * 2) * -1, yer.Next(this.Height - pcr_blok.Size.Width));
            }
            else if (neresi == 2) //aşağı
            {
                pcr_dusm.Location = new Point(yer.Next(0, this.Width - pcr_blok.Size.Width), (pcr_blok.Size.Width * 2) * -1); //hareketli  kutuların rastgele yerlerden çıkartma
            }
            else //yukarı
            {
                pcr_dusm.Location = new Point(yer.Next(0, this.Width - pcr_blok.Size.Width), pcr_blok.Size.Width+this.Bottom); //hareketli  kutuların rastgele yerlerden çıkartma
            }
           
            tmr_hareket.Interval = 200; //hareketlerin tekrarlanma sıklığı
            tmr_hareket.Tick += new EventHandler((sender, e) => hareket_Tick(sender, e, pcr_dusm)); //hareketli kutunun bilgisini metoda aktarma
            this.Controls.Add(pcr_dusm);
            tmr_hareket.Start();
        }

        void pcr_dusm_Click(object sender, EventArgs e, TimerBlok tmr)
        {
            skor++;
            ((PictureBox)sender).Dispose();
            ((TimerBlok)tmr).Stop();
            ((TimerBlok)tmr).Dispose();
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
                this.Controls.Clear();
                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Dispose();
                ((PictureBox)pcr).Dispose();
            }
           
                int x = pcr_blok.Left;
                int y = pcr_blok.Top;

                int eX = ((PictureBox)pcr).Left;
                int eY = ((PictureBox)pcr).Top;

                if (eX > x) eX -= 20;
                if (eX < x) eX += 20;

                if (eY > y) eY -= 20;
                if (eY < y) eY += 20;

                ((PictureBox)pcr).Left = eX;
                ((PictureBox)pcr).Top = eY;
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

    }
}
