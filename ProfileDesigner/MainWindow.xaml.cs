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

using tdjWpfClassLibrary;
using tdjWpfClassLibrary.Profile;
using ProfileDesigner;

namespace profileDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand CustomRoutedCommandOpenFile = new RoutedCommand();
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

        public MainWindow()
        {
            InitializeComponent();
            Profiles = new ProfileViewModelCollection();
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
                pd.ReadXML((XmlElement)xmlDesignNode);
                Profiles.Add(pd);
                XmlNode xmlExistNode = xmlDocument.SelectSingleNode("Profiles/ExistProfile");
                ProfileViewModel pe = new ProfileViewModel();
                pe.ReadXML((XmlElement)xmlDesignNode);
                Profiles.Add(pe);
                ExistTableItem.DataContext = Profiles[1].Slopes;
                DesignTableItem.DataContext = Profiles[0].Slopes;
                ExistPolylineTranslate.Y = 5200;
            }
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

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void ProfileCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ProfileCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ProfileCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
    }
}
