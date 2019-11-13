using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace blok
{
    public partial class frm_blok : Form
    {
        public frm_blok()
        {
            InitializeComponent();
        }
       
        private void frm_blok_Load(object sender, EventArgs e)
        {
            usr_giris ac = new usr_giris();
            ac.Dock = DockStyle.Fill;
            this.Controls.Add(ac);
        }
    }
}
