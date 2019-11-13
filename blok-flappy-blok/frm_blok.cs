using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblokk11
{
    public partial class frm_blok : Form
    {
        Timer tmr_yarat;
        Timer tmr_skor;
        PictureBox pcr_blok;
        Label lbl_skor;
        public frm_blok()
        {
            InitializeComponent();

            //####### Form #####
            this.Size = new System.Drawing.Size(610, 530);
            this.BackColor = Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPress += new KeyPressEventHandler(frm_blok_KeyPress);
            //##################

            //####### Timerlar #######
            tmr_skor = new Timer();
            tmr_yarat = new Timer();
            tmr_yarat.Interval = 2700; //Blok yaratma hızı
            tmr_skor.Interval = 100; //skor labelinin güncellenme hızı
            tmr_yarat.Tick += new EventHandler(tmr_yarat_Tick);
            tmr_skor.Tick += new EventHandler(tmr_skor_Tick);
            //########################

            //####### PictureBox (mavi kutu) ####
            pcr_blok = new PictureBox();
            pcr_blok.Size = new Size(40, 40); //Mavi kutu boyutu
            pcr_blok.Location = new Point(20, (this.Height / 2) - pcr_blok.Size.Height); //mavi kutunun yeri
            pcr_blok.BackColor = Color.CornflowerBlue;
            this.Controls.Add(pcr_blok);
            //######################

            //####### Label (skor) #######
            lbl_skor = new Label();
            lbl_skor.Text = "Skor: 0";
            lbl_skor.Location = new Point(20, 20);
            this.Controls.Add(lbl_skor);
            //#####################

            tmr_yarat.Start();
            tmr_skor.Start();
        }

        Random boyut = new Random();
        int ustalt = 0;
        void BlokYarat()
        {
            PictureBox pcr_dusm = new PictureBox();
            pcr_dusm.BackColor = Color.LightCoral;
            pcr_dusm.Size = new Size(50,boyut.Next(200,300)); //hareketli kutunun boyutu
            TimerBlok hareket = new TimerBlok();

            if (ustalt == 0)
            {
                pcr_dusm.Location = new Point(this.Right +(pcr_blok.Size.Width), 0); //hareketli  kutuların rastgele yerlerden çıkartma
                hareket.Ust = 1;
                ustalt = 1;
            }
            else
            {
                 pcr_dusm.Location = new Point(this.Right +(pcr_blok.Size.Width) ,this.Height -50); //hareketli  kutuların rastgele yerlerden çıkartma
                 hareket.Ust = 0;
                 ustalt = 0;
            }

            hareket.Interval = 200; //hareketlerin tekrarlanma sıklığı
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
                 this.Controls.Clear();
                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Stop();
                ((TimerBlok)sender).Dispose();
                ((PictureBox)pcr).Dispose();
            }
            ((TimerBlok)sender).Don += 10; //hareketli kutunun ilerleme boyutu
            if (((TimerBlok)sender).Ust == 0)
            {
                ((PictureBox)pcr).Location = new Point(this.Right + (pcr_blok.Size.Width/2) - ((TimerBlok)sender).Don, 0);
            }
            else
            {
                ((PictureBox)pcr).Location = new Point(this.Right + (pcr_blok.Size.Width/2) - ((TimerBlok)sender).Don, this.Height - ((PictureBox)pcr).Height);
            }
            if ((((PictureBox)pcr).Left * 2) + ((PictureBox)pcr).Size.Width+ this.Left <= 0) //eğer hareketli kutu çarpmadan formun dışına çıkarsa yapılacak işlemler
            {
                skor++;
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

        void frm_blok_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w' || e.KeyChar == 'W')
            {
                if (pcr_blok.Top > 0)
                    pcr_blok.Top = pcr_blok.Top - 10;
            }
            if (e.KeyChar == 'a' || e.KeyChar == 'A')
            {
                if (pcr_blok.Left > 0)
                    pcr_blok.Left = pcr_blok.Left - 10;
            }
            if (e.KeyChar == 's' || e.KeyChar == 'S')
            {
                if (pcr_blok.Bottom < this.Height)
                    pcr_blok.Top = pcr_blok.Top + 10;
            }
            if (e.KeyChar == 'd' || e.KeyChar == 'D')
            {
                if (pcr_blok.Right < this.Width)
                    pcr_blok.Left = pcr_blok.Left + 10;
            }
        }

    }
}