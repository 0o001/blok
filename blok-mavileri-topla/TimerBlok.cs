using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblok
{
    public partial class TimerBlok : Timer
    {
        public TimerBlok()
        {
            InitializeComponent();
        }
        private int i;
        public int Don
        {
            get
            {
                return i;
            }
            set
            {
                i = value;
            }
        }
    }
}
