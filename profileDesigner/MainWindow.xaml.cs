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

using Point = System.Windows.Point;

using tdjWpfClassLibrary;
using tdjWpfClassLibrary.Draw;
using tdjWpfClassLibrary.Profile;

namespace profileDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
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

        string xlsFileName;
        string xlsFilter = "Excel文件(*.xlsx)|*.xlsx|Excel2003文件(*.xls)|*.xls|所有文件(*.*)|*.*";

        ProfileViewModelCollection Profiles;
        public ProfileViewModel DesignProfile, ExistProfile;
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
            DesignProfile = new ProfileViewModel();
            DesignProfile.Name = "DesignProfile";
            DesignProfile.SlopeTableTop = 1;
            DesignProfile.SlopeTableBottom = 48;
            ExistProfile = new ProfileViewModel();
            ExistProfile.Name = "ExistProfile";
            ExistProfile.SlopeTableTop = ExistGrideLine.Y2 + 1;
            ExistProfile.SlopeTableBottom = ExistProfile.SlopeTableTop + 44;
            Profiles.Add(DesignProfile);
            Profiles.Add(ExistProfile);
            AltitudeDifferences = new AltitudeDifferences();
            AltitudeDifferences.AssignEvent();
            AltitudeDifferences.DesignProfile = DesignProfile;
            AltitudeDifferences.ExistProfile = ExistProfile;
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
            Ticks1.DataContext = VerticalAxis.Graduations[0];
            Ticks2.ItemsSource = VerticalAxis.Graduations[1];
            Ticks3.ItemsSource = VerticalAxis.Graduations[2];
            Ticks4.DataContext = HorizontalAxis.Graduations[0];
            Ticks5.ItemsSource = HorizontalAxis.Graduations[1];
            Ticks6.ItemsSource = HorizontalAxis.Graduations[2];
            startMovePosition = new Point();
            DesignTableItem.DataContext = DesignProfile.Slopes;
            ExistTableItem.DataContext = ExistProfile.Slopes;
            //ExistDataGrid.ItemsSource = ExistProfile.Slopes;
            //DesignStackPanel.DataContext = DesignProfile.Slopes;
            //ExistStackPanel.DataContext = ExistProfile.Slopes;
            AltitudeDifferenceStackPanel.DataContext = AltitudeDifferences.Items;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            int col;
            int v;
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
                DesignProfile.ReadXML((XmlElement)xmlDesignNode);
                XmlNode xmlExistNode = xmlDocument.SelectSingleNode("Profiles/ExistProfile");
                ExistProfile.ReadXML((XmlElement)xmlExistNode);

                //设置 修改高程位置的颜色。是否可以改成绑定？
                v = ProfileTablControl.SelectedIndex;
                DesignTableItem.IsSelected = true;
                col = DesignProfile.FixBeginOrEndAltitude ? 2 : 3;
                SetCellColor(DesignProfile.FixAltitudePosition, col, DesignDataGrid as object, Colors.Red);
                ExistTableItem.IsSelected = true;
                col = ExistProfile.FixBeginOrEndAltitude ? 2 : 3;
                SetCellColor(ExistProfile.FixAltitudePosition, col, ExistDataGrid as object, Colors.Red);
                ProfileTablControl.SelectedIndex = v;

                UpdateProfiles();
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
            Scale.Horizontal *= 1.2;
            Scale.Vertical *= 1.2;
            UpdateScale();
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            Scale.Horizontal *= 0.8;
            Scale.Vertical *= 0.8;
            UpdateScale();
        }

        private void ZoomAllButton_Click(object sender, RoutedEventArgs e)
        {
            Scale.SetScale(GradeCanvasRectangle.ActualHeight, GradeCanvasRectangle.ActualWidth, Profiles.MaxAltitude, Profiles.MinAltitude, Profiles.Length);
            UpdateScale();
        }

        private void UpdateScale()
        {
            Profiles.UpdateScale();
            UpdateProfiles();
        }

        private void Split_Click(object sender, RoutedEventArgs e)
        {

        }

        private void existDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                UpdateCellColor(1, e.Row.GetIndex(), e.Column.DisplayIndex, sender);
                UpdateProfiles();
            }
        }

        /// <summary>
        /// 更新Profile的绘图涉及到的各项参数。
        /// </summary>
        private void UpdateProfiles()
        {
            Profiles.SetLeftTop();
            HorizontalAxis.SetValue(0, Profiles.Length, Scale.Horizontal);
            VerticalAxis.SetValue(Profiles.MinAltitude, Profiles.MaxAltitude, Scale.Vertical);
            ExistPolylineTranslate.Y = -Profiles.LeftTop.Y;
            ExistPolylineTranslate.X = Profiles.LeftTop.X;
            AltitudeDifferences.Update();
        }

        private void designDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                UpdateCellColor(0, e.Row.GetIndex(), e.Column.DisplayIndex, sender);
                UpdateProfiles();
            }
        }

        /// <summary>
        /// 改变编辑的高程的单元格的颜色。—— FixAltitude
        /// </summary>
        /// <param name="profileIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="sender"></param>
        private void UpdateCellColor(int profileIndex, int row, int col, object sender)
        {
            if (col < 2 || col > 3) return;
            int oldCol = Profiles[profileIndex].FixBeginOrEndAltitude ? 2 : 3;
            SetCellColor(Profiles[profileIndex].FixAltitudePosition, oldCol, sender, Colors.Black);
            Profiles[profileIndex].FixAltitudePosition = row;
            Profiles[profileIndex].FixBeginOrEndAltitude = col == 2 ? true : false;
            SetCellColor(row, col, sender, Colors.Red);
        }

        /// <summary>
        /// 设置DataGrid单元格的颜色。要求此时DataGrid可见，否则无法修改。
        /// </summary>
        /// <param name="row">行号</param>
        /// <param name="col">列号</param>
        /// <param name="sender">DataGrid</param>
        /// <param name="color">颜色</param>
        private void SetCellColor(int row, int col, object sender, Color color)
        {
            DataGridCell cell = DataGridPlus.GetCell(sender as DataGrid, row, col);
            if (cell != null)
                cell.Foreground = new SolidColorBrush(color);
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
            if (e.Delta > 0)
            {
                Scale.Horizontal *= 1.1;
                Scale.Vertical *= 1.1;
            }
            else
            {
                Scale.Horizontal *= 0.9;
                Scale.Vertical *= 0.9;
            }
            UpdateScale();
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

        private void Import_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = xlsFilter;
            dialog.Title = "导入Excel纵断面数据";
            if (dialog.ShowDialog() == true)
            {

            }


        }
    }
}
