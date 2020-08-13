using System;
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

namespace Mastear.Views.Page_Learner
{
    public partial class Page_Learner_ModeToggleButton : UserControl
    {
        Boolean isExpert;
        public Boolean IsExpert
        {
            get
            {
                return isExpert;
            }
            set
            {
                isExpert = value;

                if (isExpert)
                {
                    xml_Expert.Background = new SolidColorBrush(Color.FromRgb(135, 206, 250));
                    xml_Default.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                }

                else
                {
                    xml_Expert.Background = new SolidColorBrush(Color.FromRgb(129, 128, 128));
                    xml_Default.Background = new SolidColorBrush(Color.FromRgb(135, 206, 250));
                }


            }
        }
        public Page_Learner_ModeToggleButton()
        {
            InitializeComponent();
        }
    }
}
