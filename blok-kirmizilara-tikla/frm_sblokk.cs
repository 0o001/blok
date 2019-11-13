using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sblokk
{
    public partial class frm_sblokk : Form
    {
        FlowLayoutPanel flp_goster;
        PictureBox pcr_kutu;
        Timer tmr_sure;
        int kutu_sayi = 48;
        public frm_sblokk()
        {
            InitializeComponent();

            //##### Form #####
            this.Size = new Size(625, 495);
            this.BackColor = Color.White;
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            //#################

            //##### Flowlayoutpanel ####
            flp_goster = new FlowLayoutPanel();
            flp_goster.Dock = DockStyle.Fill;
            this.Controls.Add(flp_goster);
            //#################

            //###### Picturebox ####
            Random renk = new Random();
            Color[] renkler = { Color.CornflowerBlue, Color.LightCoral };
            for (int i = 0; i < kutu_sayi; i++)
            {
                pcr_kutu = new PictureBox();
                pcr_kutu.Cursor = Cursors.Hand;
                pcr_kutu.BackColor = renkler[renk.Next(renkler.Length)];
                pcr_kutu.Click += new EventHandler(pcr_kutu_Click);
                pcr_kutu.MouseDown += new MouseEventHandler(pcr_kutu_MouseDown);
                pcr_kutu.Size = new System.Drawing.Size(70, 70);
                flp_goster.Controls.Add(pcr_kutu);
            }
            //#######################

            //##### Timer ##########
            tmr_sure = new Timer();
            tmr_sure.Interval = 1000;
            tmr_sure.Tick += new EventHandler(tmr_sure_Tick);
            //##########################
            tmr_sure.Start();
        }

        int sure = 10;
        void tmr_sure_Tick(object sender, EventArgs e) //süre kontrolü
        {
            sure--;
            if (sure == 0 && !kazandimi)
            {
                MessageBox.Show("Oyun Bitti", "Uyarı");
                this.Controls.Clear();
                tmr_sure.Stop();
            }
        }

        void pcr_kutu_MouseDown(object sender, MouseEventArgs e) //kutulara tıklandığında üzerlerinde çarpı işaretnin çıkması
        {
            Graphics g = ((PictureBox)sender).CreateGraphics();
            using (Font myFont = new Font("Arial", 50,FontStyle.Italic))
            {
                g.DrawString("x", myFont, Brushes.White, new Point(7 ,-5));
            }
        }

        bool kazandimi = false;
        void pcr_kutu_Click(object sender, EventArgs e) 
        {
            ((PictureBox)sender).Dispose(); //kutuyu sil
            if (((PictureBox)sender).BackColor == Color.CornflowerBlue) //kutunun rengi mavi ise
            {
                tmr_sure.Stop();
                MessageBox.Show("Oyun bitti", "Uyarı");
                this.Controls.Clear();
            }
            else
            {
                int sayi = 0;
                for (int i = 0; i < flp_goster.Controls.Count; i++) //flowlayoutpanel içindeki nesnleri say
                {
                    if (flp_goster.Controls[i].BackColor == Color.CornflowerBlue) sayi++; //nesne mavi ise sayiyi 1 arttır
                }
                if (sayi == flp_goster.Controls.Count) //eğer sayi ile flowlayoutpanel içindekiler aynıysa (hepsi aynı renkse)
                {
                    kazandimi = true;
                    MessageBox.Show("Oyunu kazandınız", "Tebrikler");
                }
            }
        }
    }
}
