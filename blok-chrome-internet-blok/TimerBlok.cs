﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ssblokk12
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
        private bool dus;
        public bool Dus
        {
            get
            {
                return dus;
            }
            set
            {
                dus = value;
            }
        }
    }
}
