﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gui.UserControls.ABMGridScreen
{
    public partial class ABMGridScreenDelete : UserControl
    {
        public ABMGridScreenDelete()
        {
            InitializeComponent();
            this.AccessibleName = "Borrar";
            this.AccessibleDescription = "Borrar Plano";
        }
    }
}