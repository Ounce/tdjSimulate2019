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
        public MainWindow()
        {
            InitializeComponent();
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
                var xmlns = new XmlNamespaceManager(xmlDocument.NameTable);
                xmlns.AddNamespace("a", "http://ounce.gitee.io/tdjsimulate/");
                profileDrawing.Clear();
                root = (XmlElement)xmlDocument.SelectSingleNode("a:Profiles", xmlns);
                ProfileDrawing.GradeUnit = Convert.ToDouble(root.GetAttribute("GradeUnit").ToString());
                XmlNode xmlDesignNode = xmlDocument.SelectSingleNode("a:Profiles/DesignProfile", xmlns);
                ReadProfile(profileDrawing.DesignProfile, (XmlElement)xmlDesignNode);
                XmlNode xmlExistNode = xmlDocument.SelectSingleNode("a:Profiles/ExistProfile", xmlns);
                ReadProfile(profileDrawing.ExistProfile, (XmlElement)xmlExistNode);
                profileDrawing.SetScale();
                profileDrawing.AltitudeDifferences.Update();
                SetFixAltitude(profileDrawing.ExistProfile, ExistDataGrid, ExistTableItem);
                SetFixAltitude(profileDrawing.DesignProfile, DesignDataGrid, DesignTableItem);
            }
        }
    }
}
