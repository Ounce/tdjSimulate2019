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
using tdjClassLibrary.Profile;

namespace ProfileDesigner
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
        
        ProfileDesign ProfileDesign;

        public MainWindow()
        {
            InitializeComponent();
            ProfileDesign = new ProfileDesign();


        }

        public void OpenFile_Click(object sender, RoutedEventArgs e)
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
                ProfileDesign.DesignPolylineDrawing.Profile.ReadXML((XmlElement)xmlDesignNode);
                XmlNode xmlExistNode = xmlDocument.SelectSingleNode("Profiles/ExistProfile");
                ProfileDesign.ExistPolylineDrawing.Profile.ReadXML((XmlElement)xmlDesignNode);
            }
        }

        private void SetFixAltitude(ProfileViewModel profile, DataGrid dataGrid, TabItem tabItem)
        {
          
        }

        public void ImportExist_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ImportDesign_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel文件(*.xls)|*.xls";
            if (dialog.ShowDialog() == true)
            {

                //TODO: 完善导入设计纵断面
            }
        }

        public void ExportDXF_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Split_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void AppendSlopes(ProfileViewModel profile, XmlElement element)
        {
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

        private void EditExist_Click(object sender, RoutedEventArgs e)
        {
            ExistTableItem.IsSelected = true;
        }

        private void EditDesign_Click(object sender, RoutedEventArgs e)
        {
            DesignTableItem.IsSelected = true;
        }

        private void ShowOption(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 将Excel中的数据读取到Profile中，并补充缺失的数据，然后将Profile绑定到DataGrid的ItemsSource。应该移到ProfileViewModel中。
        /// </summary>
        /// <param name="pPath"></param>
        private void ReadExcel(string pPath)
        {

        }

        // 获取工作表名称  
        private string GetExcelSheetName(string pPath)
        {
            //打开一个Excel应用  
     
            return "";
        }

        // 释放资源  
        private void ReleaseCOM(object pObj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
            }
            catch
            {
                throw new Exception("释放资源时发生错误！");
            }
            finally
            {
                pObj = null;
            }
        }

        private void existDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void designDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
        }

        /// <summary>
        /// 处理Profile编辑后的数据。
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="e"></param>
        private void CellEditEnding(ProfileViewModel profile, object sender, DataGridCellEditEndingEventArgs e)
        {
            int oldCol;
   
        }

        private void ZoomAllButton_Click(object sender, RoutedEventArgs e)
        {
  
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
         }

        private void UpdatePolyline()
        {
       }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
         }

        private void ProfileCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ProfileCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
         }

        private void ProfileCanvas_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void ProfileCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
         }

        private void SetTableRectangleWidth()
        {
        }


        private void AddElements(ProfileViewModel profile)
        {
            foreach (SlopeViewModel slope in profile.Slopes)
            {
                AddElements(slope);
            }
        }

        /// <summary>
        /// 在TableCanvas中添加Slope的坡段表中线段、数字等。
        /// 线段、数字被赋予ExistTableTransform。
        /// </summary>
        /// <param name="slope"></param>
        private void AddElements(SlopeViewModel slope)
        {
         }

        private void ExistDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void DesignDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
         }


        /// <summary>
        /// 用于插入和删除数据行。
        /// DataGrid自动响应Delete，但是不响应Insert。
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridKeyUp(ProfileViewModel profile, object sender, KeyEventArgs e)
        {

        }

        private void ExistDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {


        }

        private void DesignDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

    
