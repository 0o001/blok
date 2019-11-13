using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace blok
{
    public partial class usr_giris : UserControl
    {
        public usr_giris()
        {
            InitializeComponent();
        }

        private void btn_oyna_Click(object sender, EventArgs e)
        {
            this.Dispose();
            usr_blok ac = new usr_blok();
            ac.Dock = DockStyle.Fill;
            frm_blok.ActiveForm.Controls.Add(ac);
        }

        SoundPlayer cal;
        private void usr_giris_Load(object sender, EventArgs e)
        {
            cal = new SoundPlayer();
            cal.Stream = Properties.Resources.Scratch_Overview;
        }
        bool dur = true;
        private void btn_ses_Click(object sender, EventArgs e)
        {
            if (dur)
            {
                cal.PlayLooping();
                btn_ses.Image = Properties.Resources.mute;
                dur = false;
            }
            else
            {
                cal.Stop();
                btn_ses.Image = Properties.Resources.volume;
                dur = true;
            }
        }
    }
}
