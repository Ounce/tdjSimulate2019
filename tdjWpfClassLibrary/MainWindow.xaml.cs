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

        ProfileViewModel Profile;

        private double VerticalHorizontalScale = 50;
        private double VerticalScale;
        private double HorizontalScale;

        public MainWindow()
        {
            InitializeComponent();
            Profile = new ProfileViewModel();
            label.DataContext = Profile;
            ExistPolyline.Points = Profile.PolylinePoints;
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
                Profile.ReadXML((XmlElement)xmlDesignNode);
                Profile.UpdateMaxMinAltitude();
                SetScale();
                ExistPolylineTranslate.X = 0;
                ExistPolylineTranslate.Y = - Profile.MaxAltitude * VerticalScale +400; 
                Profile.SetHorizontalVerticalScale(HorizontalScale, VerticalScale);
            }
        }

        /// <summary>
        /// 设置在Canvas全部显示Profile时的比例。
        /// </summary>
        public void SetScale()
        {
            double v = PolylineCanvas.ActualHeight / (Profile.MaxAltitude - Profile.MinAltitude);
            double h = PolylineCanvas.ActualWidth / Profile.Length;
            if (h * VerticalHorizontalScale < v)
            {
                HorizontalScale = h;
                VerticalScale = h * VerticalHorizontalScale;
            }
            else
            {
                VerticalScale = v;
                HorizontalScale = v / VerticalHorizontalScale;
            }
        }

    }
}