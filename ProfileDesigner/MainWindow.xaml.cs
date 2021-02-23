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

using tdjWpfClassLibrary;
using tdjWpfClassLibrary.Draw;
using tdjWpfClassLibrary.Profile;

namespace profileDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand CommandExit = new RoutedCommand();

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Control target = e.Source as Control;
            if (target != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        XmlDocument xmlDocument;
        XmlElement root;
        string FileName;
        string Filter = "纵断面文件(*.profile)|*.profile";

        ProfileViewModelCollection Profiles;
        AltitudeDifferences AltitudeDifferences;
        private NumberAxis VerticalAxis;
        private NumberAxis HorizontalAxis;

        //鼠标按下去的位置
        private Point startMovePosition;

        //移动标志
        private bool isMoving = false;

        public MainWindow()
        {
            InitializeComponent();
            Profiles = new ProfileViewModelCollection();
            AltitudeDifferences = new AltitudeDifferences();
            Profiles.VerticalAlignment = VerticalAlignment.Center;
            Profiles.HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAxis = new NumberAxis();
            VerticalAxis.AddGraduation(1);
            VerticalAxis.AddGraduation(0.5);
            VerticalAxis.AddGraduation(0.1);
            HorizontalAxis = new NumberAxis();
            HorizontalAxis.AddGraduation(100);
            HorizontalAxis.AddGraduation(50);
            HorizontalAxis.AddGraduation(10);
            startMovePosition = new Point();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
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
                ProfileViewModel pd = new ProfileViewModel();
                pd.Name = "DesignProfile";
                pd.ReadXML((XmlElement)xmlDesignNode);
                pd.SlopeTableTop = 1;
                pd.SlopeTableBottom = 48;
                Profiles.Add(pd);
                XmlNode xmlExistNode = xmlDocument.SelectSingleNode("Profiles/ExistProfile");
                ProfileViewModel pe = new ProfileViewModel();
                pe.Name = "ExistProfile";
                pe.ReadXML((XmlElement)xmlDesignNode);
                pe.SlopeTableTop = ExistGrideLine.Y2 + 1;
                pe.SlopeTableBottom = pe.SlopeTableTop + 44;
                Profiles.Add(pe);
                ExistTableItem.DataContext = Profiles[1].Slopes;
                DesignTableItem.DataContext = Profiles[0].Slopes;
                ExistPolylineTranslate.Y = -Profiles.LeftTop.Y;
                ExistPolylineTranslate.X = Profiles.LeftTop.X;
                DesignStackPanel.DataContext = pd.Slopes;
                /*
                ItemsControl2.ItemsSource = pd.Slopes;
                ItemsControl3.ItemsSource = pd.Slopes;
                */
                ExistStackPanel.DataContext = pe.Slopes;
                AltitudeDifferences.DesignProfile = pd;
                AltitudeDifferences.ExistProfile = pe;
                AltitudeDifferences.AssignEvent();
                AltitudeDifferences.Update();
                AltitudeDifferenceStackPanel.DataContext = AltitudeDifferences.Items;

                VerticalAxis.SetValue(Profiles.MinAltitude, Profiles.MaxAltitude, Scale.Vertical);
                Ticks1.DataContext = VerticalAxis.Graduations[0];
                Ticks2.ItemsSource = VerticalAxis.Graduations[1];
                Ticks3.ItemsSource = VerticalAxis.Graduations[2];

                HorizontalAxis.SetValue(0, Profiles.Length, Scale.Horizontal);
                Ticks4.DataContext = HorizontalAxis.Graduations[0];
                Ticks5.ItemsSource = HorizontalAxis.Graduations[1];
                Ticks6.ItemsSource = HorizontalAxis.Graduations[2];
            }
            e.Handled = true;   //说是可以避免降低性能，但似乎没啥效果。
        }

        private void ShowOption(object sender, RoutedEventArgs e)
        {

        }

        private void EditDesign_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditExist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            if (xmlDocument == null)
            {
                SaveAs_Click(sender, e);
            }
            else
            {
                root.RemoveAll();
                SaveFile();
            }
            //TODO:1 完善保存profile文件。
        }

        private void SaveFile()
        {
            for (int i = 0; i < Profiles.Count; i++)
            {
                XmlElement designElement = xmlDocument.CreateElement(Profiles[i].Name);
                AppendSlopes(Profiles[i], designElement);
            }
            xmlDocument.Save(FileName);
        }

        private void AppendSlopes(ProfileViewModel profile, XmlElement element)
        {
            element.SetAttribute("GradeUnit", profile.GradeUnit.ToString());
            element.SetAttribute("FixAltitudePosition", profile.FixAltitudePosition.ToString());
            element.SetAttribute("FixBeginOrEndAltitude", profile.FixBeginOrEndAltitude.ToString());
            root.AppendChild(element);
            foreach (SlopeViewModel slope in profile.Slopes)
            {
                XmlElement slopeElement = xmlDocument.CreateElement("Slope");
                slopeElement.SetAttribute("Length", slope.Length.ToString());
                slopeElement.SetAttribute("Grade", slope.Grade.ToString());
                slopeElement.SetAttribute("BeginAltitude", slope.BeginAltitude.ToString());
                element.AppendChild(slopeElement);
            }
        }

        public void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            //TODO: 完善另存profile文件。
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Filter;
            if (sfd.ShowDialog() == true)
            {
                FileName = sfd.FileName;
                xmlDocument = new XmlDocument();
                xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null));
                XmlNamespaceManager xmlns = new XmlNamespaceManager(xmlDocument.NameTable);
                root = xmlDocument.CreateElement("Profiles", "http://ounce.gitee.io/tdjsimulate/");
                xmlDocument.AppendChild(root);
                SaveFile();
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoomAllButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Split_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExistDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void existDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void ExistDataGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void DesignDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void designDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void DesignDataGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ProfileCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseMoveButton.IsChecked == true)
            {
                startMovePosition = e.GetPosition((Grid)sender);
                isMoving = true;
            }
            else
                isMoving = false;
        }

        private void ProfileCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                Point currentMousePosition = e.GetPosition((Grid)sender);//当前鼠标位置
                Point deltaPt = new Point(0, 0);
                deltaPt.X = currentMousePosition.X - startMovePosition.X;
                deltaPt.Y = currentMousePosition.Y - startMovePosition.Y;
                ExistPolylineTranslate.X += deltaPt.X;
                ExistPolylineTranslate.Y += deltaPt.Y;
                startMovePosition = currentMousePosition;
            }
        }

        private void ProfileCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MouseMoveButton.IsChecked == true)
            {
                isMoving = false;
                Point endMovePosition = e.GetPosition((Grid)sender);
                //为了避免跳跃式的变换，单次有效变化 累加入 totalTranslate中。           
                ExistPolylineTranslate.X += endMovePosition.X - startMovePosition.X;
                ExistPolylineTranslate.Y += endMovePosition.Y - startMovePosition.Y;
            }
        }

        private void ProfileCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void ExistCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ExistCanvas_MouseDown");
        }

        private void GradeCanvasRectangle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Profiles.CanvasActualHeight = GradeCanvasRectangle.ActualHeight;
            Profiles.CanvasActualWidth = GradeCanvasRectangle.ActualWidth;
        }
    }
}
