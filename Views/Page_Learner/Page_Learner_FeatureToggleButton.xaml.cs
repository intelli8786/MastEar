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
    public partial class Page_Learner_FeatureToggleButton : UserControl
    {
        Boolean isToggled;
        public Boolean IsToggled
        {
            get
            {
                return isToggled;
            }
            set
            {
                isToggled = value;

                if (isToggled)
                {
                    xml_Body.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }

                else
                {
                    xml_Body.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                }
            }
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

        public Page_Learner_FeatureToggleButton()
        {
            InitializeComponent();
        }
    }
}
