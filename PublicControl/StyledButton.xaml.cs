﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastear.PublicControl
{
    public partial class StyledButton : UserControl
    {
        public StyledButton()
        {
            InitializeComponent();
        }



        public String Text
        {
            get
            {
                return xml_Text.Text;
            }
            set
            {
                xml_Text.Text = value;
            }
        }

    }
}