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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastear.Views.Page_Monitor
{
    /// <summary>
    /// Page_Monitor_NewActuator.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Monitor_NewActuator : UserControl
    {
        public Page_Monitor_NewActuator()
        {
            InitializeComponent();
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Actuator newActuator = new Actuator();
            newActuator.ActuatorName = "새 장치";
            newActuator.NotificationMax = 100;
            newActuator.NotificationMin = 0;

            Page_Monitor.databaseSystem.system.actuatorList.Add(newActuator);

            MainWindow.View2.Children.Clear();
            MainWindow.View2.Children.Add(new Views.Page_Actuator.Page_Actuator(newActuator));
            MainWindow.DoMoveRight();
        }
    }
}
