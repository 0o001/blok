using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblokk12
{
    public partial class frm_blok : Form
    {
        Timer tmr_yarat;
        Timer tmr_skor;
        Timer tmr_zipla;
        PictureBox pcr_blok;
        Label lbl_skor;
        public frm_blok()
        {
            InitializeComponent();
            //####### Form #####
            this.Size = new System.Drawing.Size(600, 450);
            this.BackColor = Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyDown += new KeyEventHandler(frm_blok_KeyDown);
            this.KeyPress += new KeyPressEventHandler(frm_blok_KeyPress);
            this.StartPosition = FormStartPosition.CenterScreen;
            //##################

            //####### Timerlar #######
            tmr_skor = new Timer();
            tmr_yarat = new Timer();
            tmr_zipla = new Timer();
            tmr_yarat.Interval = 4000; //Blok yaratma hızı
            tmr_zipla.Interval = 150;
            tmr_skor.Interval = 100; //skor labelinin güncellenme hızı
            tmr_yarat.Tick += new EventHandler(tmr_yarat_Tick);
            tmr_skor.Tick += new EventHandler(tmr_skor_Tick);
            tmr_zipla.Tick +=new EventHandler(tmr_zipla_Tick);
            //########################

            //####### PictureBox (mavi kutu) ####
            pcr_blok = new PictureBox();
            pcr_blok.Size = new Size(40, 40); //Mavi kutu boyutu
            pcr_blok.Location = new Point(100, this.Height - (pcr_blok.Size.Height*2-11)); //mavi kutunun yeri
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

        void frm_blok_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a' || e.KeyChar == 'A')
            {
                if (pcr_blok.Left > 0)
                    pcr_blok.Left = pcr_blok.Left - 10;
            }
            if (e.KeyChar == 'd' || e.KeyChar == 'D')
            {
                if (pcr_blok.Right < this.Width)
                    pcr_blok.Left = pcr_blok.Left + 10;
            }
        }

        void frm_blok_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                tmr_zipla.Start();
            }
        }
        int zipla;
        void tmr_zipla_Tick(object sender, EventArgs e)
        {
            zipla++;
            if (zipla >= 14)
            {
                pcr_blok.Top = pcr_blok.Top + 15;
                if (zipla == 26)
                {
                    zipla = 0;
                    tmr_zipla.Stop();
                }
            }
            else
            {
                pcr_blok.Top = pcr_blok.Top - 15;
            }

        }

        Random boyut = new Random();
        void BlokYarat()
        {
            PictureBox pcr_dusm = new PictureBox();
            pcr_dusm.BackColor = Color.LightCoral;
            pcr_dusm.Size = new Size(50, boyut.Next(50,150)); //hareketli kutunun boyutu
            TimerBlok hareket = new TimerBlok();
            pcr_dusm.Location = new Point(this.Right + (pcr_blok.Size.Width), this.Height - 50); //hareketli  kutuların rastgele yerlerden çıkartma
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
          
                ((PictureBox)pcr).Location = new Point(this.Right + (pcr_blok.Size.Width / 2) - ((TimerBlok)sender).Don, this.Height - ((PictureBox)pcr).Height);
            if ((((PictureBox)pcr).Left * 2) + ((PictureBox)pcr).Size.Width + this.Left <= 0) //eğer hareketli kutu çarpmadan formun dışına çıkarsa yapılacak işlemler
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
    }
}
