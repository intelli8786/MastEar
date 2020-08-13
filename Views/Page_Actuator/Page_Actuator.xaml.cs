using Mastear.DataBase;
using Mastear.Views.Page_Monitor;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Mastear.Views.Page_Actuator
{
    /// <summary>
    /// Page_Actuator.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Actuator : UserControl
    {
        Actuator db_actuator;

        public Page_Actuator(Actuator db_actuator)
        {
            InitializeComponent();

            //동기화
            this.db_actuator = Page_Monitor.Page_Monitor.databaseSystem.system.actuatorList.Find(obj => obj.ActuatorName == db_actuator.ActuatorName);

            

            //이미지 로드
            if(db_actuator.ThumbNailPath != null)
            {
                BitmapImage thumbNail = new BitmapImage();
                thumbNail.BeginInit();
                thumbNail.UriSource = new Uri(db_actuator.ThumbNailPath);
                thumbNail.EndInit();
                xml_ThumbNail.Source = thumbNail;
            }

            /*
            //디렉토리검사 타이머
            DispatcherTimer timer = new DispatcherTimer();    //객체생성

            timer.Interval = TimeSpan.FromMilliseconds(2000);    //시간간격 설정
            timer.Tick += new EventHandler((send, ee) =>
            {
               
            });
            timer.Start();
            */
            String path = System.Environment.CurrentDirectory + @"\Record\";
            if (System.IO.Directory.Exists(path))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                xml_CurrentSoundList.Children.Clear();
                foreach (var item in di.GetFiles())
                {
                    xml_CurrentSoundList.Children.Add(new Page_Actuator_CurrentSound()
                    {
                        SoundName = item.Name
                    });
                }
            }



            //속성 로드
            xml_ActuatorName.Text = db_actuator.ActuatorName;
            xml_NotificationMax.Text = db_actuator.NotificationMax.ToString();
            xml_NotificationMin.Text = db_actuator.NotificationMin.ToString();

        }

        private void xml_Back_MouseUp(object sender, MouseButtonEventArgs e)
        {
            db_actuator.ActuatorName = xml_ActuatorName.Text;
            db_actuator.NotificationMax = float.Parse(xml_NotificationMax.Text);
            db_actuator.NotificationMin = float.Parse(xml_NotificationMin.Text);


            //1페이지 다시 로드
            Page_Monitor.Page_Monitor.databaseSystem.Save();
            //MainWindow.View1.Children.Clear();
            //MainWindow.View1.Children.Add(new Views.Page_Monitor.Page_Monitor());
            Page_Monitor.Page_Monitor.ReDraw();
            MainWindow.DoMoveLeft();

        }

        private void xml_Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Page_Monitor.Page_Monitor.databaseSystem.system.actuatorList.Remove(db_actuator);

            //1페이지 다시 로드
            Page_Monitor.Page_Monitor.databaseSystem.Save();
            //MainWindow.View1.Children.Clear();
            //MainWindow.View1.Children.Add(new Views.Page_Monitor.Page_Monitor());
            Page_Monitor.Page_Monitor.ReDraw();
            MainWindow.DoMoveLeft();
        }

        private void xml_ThumbNail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".*";
            dlg.Filter = "All Files (*.*)|*.*|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                db_actuator.ThumbNailPath = dlg.FileName;

                //이미지 로드
                BitmapImage thumbNail = new BitmapImage();
                thumbNail.BeginInit();
                thumbNail.UriSource = new Uri(db_actuator.ThumbNailPath);
                thumbNail.EndInit();
                xml_ThumbNail.Source = thumbNail;
            }
        }

        private void StyledButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            String FilePath = System.Environment.CurrentDirectory + @"\Learned\"+ db_actuator.ActuatorName+@"\";

            DirectoryInfo directoryInfo = new DirectoryInfo(FilePath);
            if (directoryInfo.Exists == false)
                directoryInfo.Create();


            List<Page_Actuator_CurrentSound> Temp = new List<Page_Actuator_CurrentSound>();

            foreach (Page_Actuator_CurrentSound pac in xml_CurrentSoundList.Children)
            {
                if (pac.IsSelected)
                {
                    Temp.Add(pac);
                }
            }

            foreach(Page_Actuator_CurrentSound pac in Temp)
            {
                xml_CurrentSoundList.Children.Remove(pac);
                xml_LearndSoundList.Children.Add(pac);
            }

        }
    }
}
