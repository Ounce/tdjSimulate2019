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
using System.Reflection;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        string FileName 
        {
            get { return _fileName; }
            set
            {
                if (value != _fileName)
                {
                    _fileName = value;
                    this.Title = "纵断面辅助设计 -- " + _fileName;
                }
            } 
        }
        private string _fileName;
        string Filter = "纵断面文件(*.profile)|*.profile";

        bool CanClose = true;

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

        private Point MousePosition; //鼠标在Grid中的位置。
        private double MouseMileage;
        private double MouseAltitude;

        private DataGrid ActiveDataGrid
        {
            get
            {
                if (DesignTableItem.IsSelected)
                    return DesignDataGrid;
                else if (ExistTableItem.IsSelected)
                    return ExistDataGrid;
                else
                    return null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "纵断面辅助设计";
            //与前台Window定义重复。
            //this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
            Profiles = new ProfileViewModelCollection();
            //Profiles.PropertyChanged += ProfilesPropertyChanged;
            DesignProfile = new ProfileViewModel();
            DesignProfile.Name = "DesignProfile";
            DesignProfile.Title = "设计纵断面";
            DesignProfile.SlopeTableTop = 1;
            DesignProfile.SlopeTableBottom = 48;
            DesignProfile.PropertyChanged += ProfilesPropertyChanged;
            ExistProfile = new ProfileViewModel();
            ExistProfile.Name = "ExistProfile";
            ExistProfile.Title = "既有纵断面";
            ExistProfile.SlopeTableTop = ExistGrideLine.Y2 + 1;
            ExistProfile.SlopeTableBottom = ExistProfile.SlopeTableTop + 44;
            ExistProfile.PropertyChanged += ProfilesPropertyChanged;
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

            //activeDataGrid = ExistDataGrid;
        }

        private void ProfilesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Updated":
                    UpdateProfiles();
                    break;
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            int col;
            int v;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Filter;
            dialog.Title = "打开纵断面编辑文件";
            // todo: 判断数据是否已被修改。
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
                /*UpdateProfiles();*/
                CanClose = true;
            }
            e.Handled = true;   //说是可以避免降低性能，但似乎没啥效果。
        }

        private void ShowOption(object sender, RoutedEventArgs e)
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
            CanClose = true;
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
            SaveAs();
        }

        public bool SaveAs()
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
                root = xmlDocument.CreateElement("Profiles");
                xmlDocument.AppendChild(root);
                SaveFile();
                return true;
            }
            return false;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            MousePosition.X = 0.5 * Grid.ActualWidth;
            MousePosition.Y = 0.5 * Grid.ActualHeight;
            Zoom(1.2);
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            MousePosition.X = 0.5 * Grid.ActualWidth;
            MousePosition.Y = 0.5 * Grid.ActualHeight;
            Zoom(0.8);
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
                //UpdateProfiles();
                CanClose = false;
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
                // 由ProfileViewModel等类的Updated消息传递修改状态，并触发ProfilesPropertyChanged调用UpdateProfiles。
                //UpdateProfiles(); 
                CanClose = false;
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
            //其他功能需要随时确定鼠标位置。
            MousePosition = e.GetPosition((Grid)sender);//当前鼠标位置
            if (isMoving)
            {
                Point deltaPt = new Point(0, 0);
                deltaPt.X = MousePosition.X - startMovePosition.X;
                deltaPt.Y = MousePosition.Y - startMovePosition.Y;
                ExistPolylineTranslate.X += deltaPt.X;
                ExistPolylineTranslate.Y += deltaPt.Y;
                startMovePosition = MousePosition;
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
                Zoom(1.1);
            }
            else
            {
                Zoom(0.9);
            }
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

        private void Copy_Execute(object sender, RoutedEventArgs e)
        {
            if (ActiveDataGrid == null)
                return;
            ActiveDataGrid.SelectAllCells();
            ActiveDataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, ActiveDataGrid);
            ActiveDataGrid.UnselectAllCells();
        }

        private void ExportDXF_Execute(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "DXF文件(*.dxf)|*.dxf";
            if (sfd.ShowDialog() == true)
            {
                DXFIO dxfIO = new DXFIO();
                dxfIO.ExportToDXF(Profiles, sfd.FileName, AltitudeDifferences);
                MessageBox.Show("导出已完成！", "提示!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Paste_Execute(object sender, RoutedEventArgs e)
        {
            if (ActiveDataGrid == null) return;
            int rowIndex = -1;
            int colIndex = -1;
            var _cells = ActiveDataGrid.SelectedCells;
            if (_cells.Any())
            {
                rowIndex = ActiveDataGrid.Items.IndexOf(_cells.First().Item);
                colIndex = _cells.First().Column.DisplayIndex;
            }
            else
                return;
            string pasteText = Clipboard.GetText();
            string[] Rinfo = pasteText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int rSum = Math.Min(Rinfo.Length, ActiveDataGrid.Items.Count - rowIndex);
            for (int i = 0; i < rSum; i++)
            {
                string[] Cinfo = Rinfo[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                int cSum = Math.Min(Cinfo.Length, ActiveDataGrid.Columns.Count - colIndex);
                SlopeViewModel slope = ActiveDataGrid.Items[i + rowIndex] as SlopeViewModel;
                for (int j = 0; j < cSum; j++)
                {
                    switch(j + colIndex)
                    {
                        case 0:
                            slope.Length = StringConvert.ToDouble(Cinfo[j]);
                            break;
                        case 1:
                            slope.Grade = StringConvert.ToDouble(Cinfo[j]);
                            break;
                        case 2:
                            slope.BeginAltitude = StringConvert.ToDouble(Cinfo[j]);
                            break;
                        case 3:
                            slope.EndAltitude = StringConvert.ToDouble(Cinfo[j]);
                            break;
                    }
                }
            }
            UpdateProfiles();
        }

        private int GetSelectedRowIndex(DataGrid dataGrid)
        {
            var _cells = dataGrid.SelectedCells;
            if (_cells.Any())
            {
                return dataGrid.Items.IndexOf(_cells.First().Item);
            }
            else
                return -1 ;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CanClose)
            {
                e.Cancel = false;
                return;
            }
            switch (MessageBox.Show("纵断面已修改，现在退出会丢失数据！是否保存？", "警告！", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    if (SaveAs())
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                    break;
                case MessageBoxResult.No:
                    e.Cancel = false;
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// 更新鼠标说在位置对应的里程
        /// </summary>
        private void UpdateMouseMileage()
        {
            //当前鼠标位置MousePosition由ProfileCanvas_MouseMove函数确定。
            MouseMileage = (MousePosition.X - ExistPolylineTranslate.X) / Scale.Horizontal;
            return;
        }

        /// <summary>
        /// 更新鼠标位置对应的高程
        /// </summary>
        private void UpdateMouseAltitude()
        {
            MouseAltitude = Profiles.MaxAltitude - (MousePosition.Y - (ExistPolylineTranslate.Y + ValueConverter.VerticalValue(Profiles.MaxAltitude * Scale.Vertical) )) / Scale.Vertical;
        }

        private void ExistDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    DeleteSlope(ExistDataGrid);
                    break;
                case Key.Insert:
                    AddSlope(ExistDataGrid);
                    break;
            }
        }

        private void DesignDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    DeleteSlope(DesignDataGrid);
                    break;
                case Key.Insert:
                    AddSlope(DesignDataGrid);
                    break;
            }
        }

        private void AddSlope(DataGrid dataGrid)
        {
            if (dataGrid == null) return;
            SlopeViewModel slope = new SlopeViewModel();
            int rowIndex = GetSelectedRowIndex(dataGrid);
            if (rowIndex == -1) return;
            ((ObservableCollection<SlopeViewModel>)(dataGrid.ItemsSource)).Insert(rowIndex, slope);
            CanClose = false;
        }

        private void InsertSlope(object sender, RoutedEventArgs e)
        {
            AddSlope(ActiveDataGrid);
        }

        private void DeleteSlope(DataGrid dataGrid)
        {
            if (dataGrid == null) return;
            int rowIndex = GetSelectedRowIndex(dataGrid);
            if (rowIndex == -1) return;
            ((ObservableCollection<SlopeViewModel>)(dataGrid.ItemsSource)).RemoveAt(rowIndex);
            CanClose = false;
        }

        private void RemoveSlope(object sender, RoutedEventArgs e)
        {
            DeleteSlope(ActiveDataGrid);
        }

        private void Zoom(double scale)
        {
            UpdateMouseMileage();
            UpdateMouseAltitude();
            Scale.Horizontal *= scale;
            Scale.Vertical *= scale;
            UpdateScale();
            ExistPolylineTranslate.X = MousePosition.X - MouseMileage * Scale.Horizontal;
            ExistPolylineTranslate.Y = -ValueConverter.VerticalValue(MouseAltitude * Scale.Vertical) + MousePosition.Y;
        }
    }
}
