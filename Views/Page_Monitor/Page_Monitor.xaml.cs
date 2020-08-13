using Mastear.DataBase;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace Mastear.Views.Page_Monitor
{
    /// <summary>
    /// Page_Monitor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Monitor : UserControl
    {
        public static DataBaseSystem databaseSystem = new DataBaseSystem();
        public static Action ReDraw;


        public Page_Monitor()
        {
            InitializeComponent();
            //데이터베이스 로드
            databaseSystem.Load();
            Draw();



            //녹음 시작
            Task.Run(() =>
            {
                WaveIn waveSource = null;
                WaveFileWriter waveFile = null;
                int a = 0;
                while (true)
                {
                    String FileName = DateTime.Now.ToString().Replace(":", "") + ".wav";
                    String FilePath = System.Environment.CurrentDirectory + @"\Record\";
                    String FileFullName = FilePath + FileName;
                    

                    DirectoryInfo directoryInfo = new DirectoryInfo(FilePath);
                    if (directoryInfo.Exists == false)
                        directoryInfo.Create();

                    this.Dispatcher.Invoke(() =>
                    {
                        waveSource = new WaveIn();
                        waveSource.WaveFormat = new WaveFormat(44100, 1);
                        waveSource.DataAvailable += new EventHandler<WaveInEventArgs>((send,ee)=> 
                        {
                            if (waveFile != null)
                            {
                                waveFile.Write(ee.Buffer, 0, ee.BytesRecorded);
                                waveFile.Flush();
                            }
                        });
                        waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>((send,ee)=>
                        {
                            if (waveSource != null)
                            {
                                waveSource.Dispose();
                                waveSource = null;
                            }

                            if (waveFile != null)
                            {
                                waveFile.Dispose();
                                waveFile = null;
                            }
                        });
                        waveFile = new WaveFileWriter(FileFullName, waveSource.WaveFormat);
                    });

                    waveSource.StartRecording();
                    Thread.Sleep(2000);
                    waveSource.StopRecording();
                    Thread.Sleep(1000);

                    //분류요청
                    WebClient myWebClient = new WebClient();
                    String uriString = "http://10.10.97.210/upload";

                    byte[] responseArray = myWebClient.UploadFile(uriString, FileFullName);
                    String result = Encoding.ASCII.GetString(responseArray);
                    JObject jo;
                    try
                    {
                        jo = JObject.Parse(result);
                    }
                    catch
                    {
                        continue;
                    }

                    this.Dispatcher.Invoke(() =>
                    {
                        for(int i=0;i<xml_ActuatorList.Children.Count - 1;i++)
                        {
                            ((Page_Monitor_Actuator)xml_ActuatorList.Children[i]).Probability = float.Parse(jo["prob"][i].ToString().Substring(0, 4)) * 100;
                        }
                    });

                    //분류이후 Temp에서 Record로 이동
                    FileInfo fileMove = new FileInfo(FilePath);
                    if (fileMove.Exists)
                    {
                        fileMove.MoveTo((FilePath+FileName));
                    }

                }
            });



            //전역화
            ReDraw = Draw;
        }

        public void Draw()
        {
            xml_ActuatorList.Children.Clear();
            foreach (Actuator db_actuator in databaseSystem.system.actuatorList)
            {
                xml_ActuatorList.Children.Add(new Page_Monitor_Actuator(db_actuator));
            }

            xml_ActuatorList.Children.Add(new Page_Monitor_NewActuator());
            xml_LearnDateTime.Text = "" + databaseSystem.system.learn.LastLearnTime;
        }

        private void StyledButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.View2.Children.Clear();
            MainWindow.View2.Children.Add(new Views.Page_Learner.Page_Learner(databaseSystem.system.learn));
            MainWindow.DoMoveRight();
        }
    }
}
