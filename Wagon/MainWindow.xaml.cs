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
using System.Xml.Linq;
using tdjWpfClassLibrary;
using tdjWpfClassLibrary.Wagon;

namespace Wagon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CutList OriginCuts;
        public static WagonModelList OriginWagons;
        public static string WagonFilePath = "..//..//..//Files//Wagons.xml";
        private RunTypes RunTypes;
        private static CutList SelectedCutList;
        private XmlDocument xmlDocument;

        public MainWindow()
        {
            InitializeComponent();
            OriginCuts = new CutList();
            OriginCuts.ReadXML("..//..//..//Files//Cuts.xml");
            CutsDataGrid.ItemsSource = OriginCuts;

            //OriginWagons = new WagonModelList();
            OriginWagons = (WagonModelList)XmlHelper.ReadXML(WagonFilePath, typeof(WagonModelList));
            ModelComboBox.ItemsSource = OriginWagons;

            RunTypes = new RunTypes();
            RunTypeComboBox.ItemsSource = RunTypes;

            //SelectedCutList = new CutList();
            //xmlDocument = new XmlDocument();
            //xmlDocument.Load("..//..//..//Files//Project.xml");
            //XElement xmlDesignNode = (XElement)xmlDocument.SelectSingleNode("Profiles/DesignProfile");
            //SelectedCutList.ReadXML(xmlDesignNode);
        }

        private void CutsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WagonDetailsGrid.DataContext = OriginCuts[CutsDataGrid.SelectedIndex];
        }

        private void SelectedCutDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WagonDetailsGrid.DataContext = OriginCuts[CutsDataGrid.SelectedIndex];
        }

        private void DeleteCutButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectFile projectFile = new ProjectFile();
            //projectFile.Version = "0.3";
            //projectFile.Cuts = new CutList();
            Cut c = new Cut();
            c.Model = "C62A";
            projectFile.Cuts.Add(c);
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            string path = "..//..//..//Files//Project.xml";
            XmlHelper.WriteXML(path, projectFile);
            
            ProjectFile p;
            p = (ProjectFile)XmlHelper.ReadXML(path, typeof(ProjectFile));
            MessageBox.Show(p.Version);
        }

        private void NewCutButton_Click(object sender, RoutedEventArgs e)
        {
            string path = "..//..//..//Files//Wagons.xml";
            /*        WagonList wagonlist = new WagonList();
                    tdjWpfClassLibrary.Wagon.Wagon a = new tdjWpfClassLibrary.Wagon.Wagon();
                    a.Model = "C62A";
                    a.Distances.Add(1.75);
                    wagonlist.Add(a);
                    XmlHelper.WriteXML(path, wagonlist);
            */
            WagonModelList wagons;
            wagons = (WagonModelList)XmlHelper.ReadXML(path, typeof(WagonModelList));
            MessageBox.Show(wagons[0].Model);
        }

        private void WagonEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWagon editWagon = new EditWagon();
            editWagon.Show();
        }
    }
}
