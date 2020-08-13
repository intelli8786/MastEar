using Mastear.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastear.Views.Page_Learner
{
    /// <summary>
    /// Page_Learner.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Learner : UserControl
    {
        Learn learn;
        public Page_Learner(Learn learn)
        {
            InitializeComponent();

            //동기화
            this.learn = Page_Monitor.Page_Monitor.databaseSystem.system.learn;

            
        }

        private void xml_Back_MouseUp(object sender, MouseButtonEventArgs e)
        {
            /*
            db_actuator.ActuatorName = xml_ActuatorName.Text;
            db_actuator.NotificationMax = float.Parse(xml_NotificationMax.Text);
            db_actuator.NotificationMin = float.Parse(xml_NotificationMin.Text);
            */

            //관측페이지 다시 로드
            Page_Monitor.Page_Monitor.databaseSystem.Save();
            //MainWindow.View1.Children.Clear();
            //MainWindow.View1.Children.Add(new Views.Page_Monitor.Page_Monitor());
            Page_Monitor.Page_Monitor.ReDraw();
            MainWindow.DoMoveLeft();
        }

        private void xml_Learning_MouseUp(object sender, MouseButtonEventArgs e)
        {
            learn.LastLearnTime = DateTime.Now;
            Page_Monitor.Page_Monitor.databaseSystem.Save();
            Page_Monitor.Page_Monitor.ReDraw();
            
              String uriString = "http://10.10.97.210:80/500,0.01,6,300,200,100";
              MyWebClient myWebClient = new MyWebClient(1000000);
         
            byte[] responseArray = myWebClient.UploadFile(uriString, fileName);

            String result = Encoding.ASCII.GetString(responseArray);

            JObject jo = JObject.Parse(result);


            if (uriString.Length > 29)
            {
                int epoch = Int32.Parse(jo["epoch"].ToString());
                float[,] point = new float[epoch, 2];

                for (int i = 0; i < epoch; i++)
                {
                    point[i, 0] = i;

                    Console.WriteLine(float.Parse(jo["cost"][i].ToString()));

                    point[i, 1] = float.Parse(jo["cost"][i].ToString());

                }
               ShowColumnChart(point, epoch);
            }

            else
            {
                String select =jo["select"].ToString();
                String prob = jo["prob"].ToString();

                MessageBox.Show(prob + select);

            }      
            
            xml_loading.Visibility = Visibility.Visible;
        }
    }
}
