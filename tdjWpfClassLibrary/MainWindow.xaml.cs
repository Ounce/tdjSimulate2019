﻿using Microsoft.Win32;
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

        ProfilePolylineDrawing ProfileDrawing;

        public Scale Scale;

        public MainWindow()
        {
            InitializeComponent();
            ProfileDrawing = new ProfilePolylineDrawing();
            label.DataContext = ProfileDrawing.Profile;
            ExistPolyline.Points = ProfileDrawing.Profile.PolylinePoints;
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
                ProfileDrawing.Profile.ReadXML((XmlElement)xmlDesignNode);
                ProfileDrawing.Profile.UpdateMaxMinAltitude();
                ProfileDrawing.SetMaxMinAltitude(ProfileDrawing.Profile);
                ProfileDrawing.SetScale(PolylineCanvas.ActualHeight, PolylineCanvas.ActualWidth);
                ProfileDrawing.Profile.SetHorizontalVerticalScale(Scale);                
                ExistPolylineTranslate.X = 0;

                ExistPolylineTranslate.Y = - ProfileDrawing.MaxAltitude * ProfileDrawing.Scale.Vertical + 400;


            }
        }





    }
}
