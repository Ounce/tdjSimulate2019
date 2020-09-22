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

        public Scale Scale;

        public MainWindow()
        {
            InitializeComponent();
            /*
            ProfileDrawing = new ProfilePolylineDrawing();
            label.DataContext = ProfileDrawing.Profile;
            ExistPolyline.Points = ProfileDrawing.Profile.PolylinePoints;
            */
            Profiles = new ProfileViewModelCollection();
            ProfileViewModel profile = new ProfileViewModel();
            label.DataContext = profile;
            Profiles.Items.Add(profile);
            ExistPolyline.Points = profile.PolylinePoints;
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
                ExistPolylineTranslate.X = ProfileDrawing.Profile.OriginPoint.X;
                ExistPolylineTranslate.Y = - ProfileDrawing.Profile.OriginPoint.Y;
                */
                //  使用 ProfileViewModelCollection的函数。
                Profiles.Items[0].ReadXML((XmlElement)xmlDesignNode);
                Profiles.PolylineVerticalAlignment = VerticalAlignment.Top;
                Profiles.SetPolylineFullSize(PolylineCanvas.ActualHeight, PolylineCanvas.ActualWidth);
                ExistPolylineTranslate.X = Profiles.PolylineOriginPoint.X;
                ExistPolylineTranslate.Y = -Profiles.PolylineOriginPoint.Y;
            }
        }
    }
}
