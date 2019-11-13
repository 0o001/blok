using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblokk
{
    public partial class frm_ssblokk : Form
    {
        Label lbl_skor;
        PictureBox pcr_blok;
        Timer tmr_yarat;
        Timer tmr_skor;
        Timer tmr_puanyarat;

        public frm_ssblokk()
        {
            InitializeComponent();

            //##### Form #######
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Paint += new PaintEventHandler(frm_ssblokk_Paint);
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            //##################

            //####### PictureBox (mavi kutu) ####
            pcr_blok = new PictureBox();
            pcr_blok.Size = new Size(50, 50); //Mavi kutu boyutu
            pcr_blok.Cursor = Cursors.Hand;
            pcr_blok.Location = new Point(this.Width - (pcr_blok.Size.Width * 3), (this.Height / 2) - pcr_blok.Size.Height); //mavi kutunun yeri
            pcr_blok.BackColor = Color.CornflowerBlue;
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
            lbl_skor.Location = new Point(this.Width - lbl_skor.Size.Width, 20);
            this.Controls.Add(lbl_skor);
            //#####################

            //####### Timerlar #######
            tmr_skor = new Timer();
            tmr_yarat = new Timer();
            tmr_puanyarat = new Timer();
            tmr_puanyarat.Interval = 3000;
            tmr_yarat.Interval = 1000; //Blok yaratma hızı
            tmr_skor.Interval = 100; //skor labelinin güncellenme hızı
            tmr_yarat.Tick += new EventHandler(tmr_yarat_Tick);
            tmr_skor.Tick += new EventHandler(tmr_skor_Tick);
            tmr_puanyarat.Tick += new EventHandler(tmr_puanyarat_Tick);
            //########################

            tmr_skor.Start();
            tmr_puanyarat.Start();
            tmr_yarat.Start();
        }

        void frm_ssblokk_Paint(object sender, PaintEventArgs e)
        {
            Graphics cizgi = this.CreateGraphics();
            Pen ciz = new Pen(System.Drawing.Color.LightGray, 7);
            cizgi.DrawLine(ciz, this.Width - 200, this.Height, this.Width - 200, 0); //gri çizgiyi çiz
        }

        void BlokYarat()
        {
            PictureBox pcr_dusm = new PictureBox();
            Random boyut = new Random();
            pcr_dusm.BackColor = Color.LightCoral;
            pcr_dusm.Size = new Size(boyut.Next(50, 50), boyut.Next(50, 50)); //hareketli kutunun boyutu
            Random yer = new Random();
            pcr_dusm.Location = new Point(yer.Next(this.Width / 2 + 90), yer.Next(this.Height / 2 + 90)); //hareketli  kutuların rastgele yerlerden çıkartma

            TimerBlok hareket = new TimerBlok();
            hareket.Interval = 300; //hareketlerin tekrarlanma sıklığı
            hareket.Tick += new EventHandler((sender, e) => hareket_Tick(sender, e, pcr_dusm)); //hareketli kutunun evetini oluşturma ve bilgisini metoda aktarma 
            this.Controls.Add(pcr_dusm);
            hareket.Start();
        }

        void PuanYarat()
        {
            PictureBox pcr_puan = new PictureBox();
            Random boyut = new Random();
            pcr_puan.BackColor = Color.CornflowerBlue;
            pcr_puan.Size = new Size(30, 30); //hareketli kutunun boyutu
            Random yer = new Random();
            pcr_puan.Location = new Point(yer.Next(this.Width / 2 + 90), yer.Next(this.Height / 2 + 90)); //hareketli  kutuların rastgele yerlerden çıkartma
            pcr_puan.Paint += new PaintEventHandler(pcr_blok_Paint);
            TimerBlok hareket = new TimerBlok();
            hareket.Interval = 300; //hareketlerin tekrarlanma sıklığı
            hareket.Tick += new EventHandler((sender, e) => puan_Tick(sender, e, pcr_puan)); 
            this.Controls.Add(pcr_puan);
            hareket.Start();
        }

        void pcr_blok_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e); //pictureboxa çizdirmek için açılan metot
            Yazi(e);
        }

        private bool Yazi(PaintEventArgs e)
        {
            Graphics yazi = e.Graphics;
            using (Font myFont = new Font("Arial", 15, FontStyle.Bold))
            {
                yazi.DrawString("+1", myFont, Brushes.White, new Point(0, 3)); //mavi kutuların yazısı
            }
            return true;
        }

        void puan_Tick(object sender, EventArgs e, PictureBox pcr)
        {
            //Çarpma kontrolü
            float yerX = Math.Abs((((PictureBox)pcr).Left + (((PictureBox)pcr).Width / 2)) - (pcr_blok.Left + (pcr_blok.Width / 2)));
            float yerY = Math.Abs((((PictureBox)pcr).Top + ((((PictureBox)pcr).Height / 2)) - (pcr_blok.Top + (pcr_blok.Height / 2))));
            float farkGenislik = ((((PictureBox)pcr).Width / 2) + (pcr_blok.Width / 2));
            float farkYukselik = ((((PictureBox)pcr).Height / 2) + (pcr_blok.Height / 2));
            //------
            if ((farkGenislik > yerX) && (farkYukselik > yerY)) //Çarpma işlemi gerçekleşirse yapılacak işlemler
            {
                skor++;
                ((TimerBlok)sender).Stop();
                ((PictureBox)pcr).Dispose();
            }
            ((TimerBlok)sender).Don++; ; //hareketli kutunun ilerleme boyutu
            if (((TimerBlok)sender).Don == 10)
            {
                ((PictureBox)pcr).Hide();
                ((PictureBox)pcr).Dispose();
            }
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
                ((TimerBlok)sender).Stop();
                tmr_yarat.Tick -= new EventHandler(tmr_yarat_Tick);
                tmr_puanyarat.Tick -= new EventHandler(tmr_puanyarat_Tick);
                MessageBox.Show("Oyun Bitti", "Uyarı");
                this.Controls.Clear();

            }
            ((TimerBlok)sender).Don++; ; //hareketli kutunun ilerleme boyutu
            if (((TimerBlok)sender).Don == 5)
            {
                ((TimerBlok)sender).Stop();
                ((PictureBox)pcr).Dispose();
                ((PictureBox)pcr).Hide();
            }
        }

        //------ kutuyu sürüklemek -------
        void pcr_blok_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu)
            {
                pcr_blok.Left = e.X + pcr_blok.Left - (ilkkonum.X);
                pcr_blok.Top = e.Y + pcr_blok.Top - (ilkkonum.Y);
            }
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
        //-------------------------

        void tmr_yarat_Tick(object sender, EventArgs e)
        {
            BlokYarat();
        }

        int skor;
        void tmr_skor_Tick(object sender, EventArgs e)
        {
            lbl_skor.Text = "Skor: " + skor;
        }

        void tmr_puanyarat_Tick(object sender, EventArgs e)
        {
            PuanYarat();
        }
    }
}
