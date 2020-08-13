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

namespace Mastear.Views.Page_Actuator
{
    /// <summary>
    /// Page_Actuator_CurrentSound.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Actuator_CurrentSound : UserControl
    {
        public Page_Actuator_CurrentSound()
        {
            InitializeComponent();
        }

        Boolean isSelected;
        public Boolean IsSelected
        {
            get
            {
                return isSelected;
            }
        }

        public String SoundName
        {
            get
            {
                return xml_SoundName.Text;
            }
            set
            {
                xml_SoundName.Text = value;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isSelected)
            {
                xml_Body.Background = new SolidColorBrush(Color.FromRgb(255,192,203));
            }
            else
            {
                xml_Body.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            }
            isSelected = !isSelected;
        }
    }
}
