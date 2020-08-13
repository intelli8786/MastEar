using Mastear.DataBase;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastear.Views.Page_Monitor
{
    /// <summary>
    /// Page_Monitor_Actuator.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Monitor_Actuator : UserControl
    {
        Actuator db_actuator;
        public Page_Monitor_Actuator(Actuator db_actuator)
        {
            InitializeComponent();

            //동기화
            this.db_actuator = Page_Monitor.databaseSystem.system.actuatorList.Find(obj => obj.ActuatorName == db_actuator.ActuatorName);

            xml_ActuatorName.Text = db_actuator.ActuatorName;
            if (db_actuator.ThumbNailPath != null)
            {
                BitmapImage thumbNail = new BitmapImage();
                thumbNail.BeginInit();
                thumbNail.UriSource = new Uri(db_actuator.ThumbNailPath);
                thumbNail.EndInit();
                xml_ThumbNail.Source = thumbNail;
            }
        }

        public String ActuatorName
        {
            get
            {
                return xml_ActuatorName.Text;
            }
            set
            {
                xml_ActuatorName.Text = value;
            }
        }

        public Double Probability
        {
            get
            {
                return Double.Parse(xml_ProbabilityText.Text);
            }
            set
            {
                xml_ProbabilityText.Text = ((int)value).ToString() + "%";

                xml_ProbabilityView.BeginAnimation(Border.HeightProperty, new DoubleAnimation(215 * value / 100, new Duration(TimeSpan.FromMilliseconds(300))));

            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.View2.Children.Clear();
            MainWindow.View2.Children.Add(new Views.Page_Actuator.Page_Actuator(db_actuator));
            MainWindow.DoMoveRight();
        }
    }
}
