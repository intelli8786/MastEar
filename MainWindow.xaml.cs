using Mastear.DataBase;
using Mastear.Views.Page_Monitor;
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

namespace Mastear
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static Action DoMoveRight;
        public static Action DoMoveLeft;
        public static Grid View1;
        public static Grid View2;
        public static Grid View3;

        public MainWindow()
        {
            InitializeComponent();
           
            //UI클래스접근변수화
            DoMoveRight = MoveRight;
            DoMoveLeft = MoveLeft;
            View1 = xml_View1;
            View2 = xml_View2;
            View3 = xml_View3;

            View1.Children.Add(new Page_Monitor());
        }

        //우측이동 애니메이션
        void MoveRight()
        {

            DoubleAnimation MyDouble = new DoubleAnimation();

            MyDouble.To = xml_Scroll.HorizontalOffset + SystemParameters.PrimaryScreenWidth;
            MyDouble.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
            MyDouble.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            Mediator.BeginAnimation(ScrollViewerOffsetMediator.ScrollableWidthMultiplierProperty, MyDouble);
        }
        //좌측이동 애니메이션
        void MoveLeft()
        {
            DoubleAnimation MyDouble = new DoubleAnimation();

            MyDouble.To = xml_Scroll.HorizontalOffset - SystemParameters.PrimaryScreenWidth;
            MyDouble.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
            MyDouble.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            Mediator.BeginAnimation(ScrollViewerOffsetMediator.ScrollableWidthMultiplierProperty, MyDouble);
        }
    }
}
