using Microsoft.Win32;
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
using System.Xml;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Draw;

namespace tdjWpfClassLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlDocument xmlDocument;
        XmlElement root;
        string FileName;
        string Filter = "纵断面文件(*.profile)|*.profile";

        //ProfilePolylineDrawing ProfileDrawing;

        ProfileViewModelCollection Profiles;
        ProfileViewModel profile;

        public double SlopeTableTop, SlopeTableBottom;

        public MainWindow()
        {
            InitializeComponent();
            /*
            ProfileDrawing = new ProfilePolylineDrawing();
            label.DataContext = ProfileDrawing.Profile;
            ExistPolyline.Points = ProfileDrawing.Profile.PolylinePoints;
            */
            
            Profiles = new ProfileViewModelCollection();
            
            profile = new ProfileViewModel();
            label.DataContext = profile;
            Profiles.Items.Add(profile);
            profile.SlopeTableRectange = SlopeRectangle;
            //SlopeTableTop = Canvas.GetTop(SlopeRectangle);
            SlopeTableTop = 0;
            SlopeTableBottom = SlopeTableTop + SlopeRectangle.Height;
            Profiles.Items[0].SlopeTableTop = 0;
            Profiles.Items[0].SlopeTableBottom = 100;
            SlopeRectangle.DataContext = profile.ProfileOption;


            //SlopeRectange.Stroke = profile.ProfileOption.SlopeTableColor;

            //ItemsControl1.ItemsSource = Profiles.Items[0].Slopes;
            GradeGrid.DataContext = Profiles.Items[0].Slopes;
            /*
            ItemsControl2.ItemsSource = Profiles.Items[0].Slopes;
            ItemsControl3.ItemsSource = Profiles.Items[0].Slopes;
            ItemsControl4.ItemsSource = Profiles.Items[0].Slopes;
            */

            Profiles.Items[0].SlopeTableBottom = SlopeTableBottom;
            Profiles.Items[0].SlopeTableTop = SlopeTableTop;

            TickMarks ts = new TickMarks(AxisDirection.Vertical, 200, 10);
            ts.Add(10);
            ts.Add(20);
            Ticks.ItemsSource = ts;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Filter;
            dialog.Title = "打开纵断面编辑文件";
            if (dialog.ShowDialog() == true)
            {
                FileName = dialog.FileName;
                xmlDocument = new XmlDocument();
                xmlDocument.Load(FileName);
                root = (XmlElement)xmlDocument.SelectSingleNode("Profiles");
                XmlNode xmlDesignNode = xmlDocument.SelectSingleNode("Profiles/DesignProfile");
                /*   使用 Profile的函数
                ProfileDrawing.Profile.ReadXML((XmlElement)xmlDesignNode);
                ProfileDrawing.Profile.SetPolylineFullSize(PolylineCanvas.ActualHeight, PolylineCanvas.ActualWidth);

                */
                //  使用 ProfileViewModelCollection的函数。
                Profiles.Items[0].ReadXML((XmlElement)xmlDesignNode);
                //Profiles.PolylineVerticalAlignment = VerticalAlignment.Top;
                //Profiles.SetPolylineFullSize(PolylineCanvas.ActualHeight, PolylineCanvas.ActualWidth);
                //Profiles.UpdateMaxMinAltitude();
                Profiles.SetPolylineOriginPoint(PolylineCanvas.ActualHeight, PolylineCanvas.ActualWidth);
                Canvas.SetLeft(FrameRectangle, 0);
                Canvas.SetTop(FrameRectangle, Profiles.PolylineOriginPoint.Y);
                ExistPolylineTranslate.X = Profiles.PolylineOriginPoint.X;
                ExistPolylineTranslate.Y = -Profiles.PolylineOriginPoint.Y;

                //测试 SlopeTable
                /*
                ExistPolylineTranslate.X = Profiles.PolylineOriginPoint.X;
                ExistPolylineTranslate.Y = -Profiles.PolylineOriginPoint.Y;
                */

                // 测试 SlopeTable


            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Profiles.Items[0].Slopes[3].Grade = -Profiles.Items[0].Slopes[3].Grade;
            return;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Profiles.Items[0].Slopes[3].Length += 50;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //SlopeLines lines = new SlopeLines();
            //ItemsControl1.DataContext = lines;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            profile.ProfileOption.SlopeTableColor = Brushes.Yellow;
        }
    }
}
