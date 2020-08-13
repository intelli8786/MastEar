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
    /// Page_Actuator_LearnedSound.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Actuator_LearnedSound : UserControl
    {
        public Page_Actuator_LearnedSound()
        {
            InitializeComponent();
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
    }
}
