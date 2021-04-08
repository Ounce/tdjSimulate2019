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
        private static string OriginCutsPath = "..//..//..//Files//Cuts.xml";
        public static WagonModelList OriginWagons;
        public static string WagonFilePath = "..//..//..//Files//Wagons.xml";
        private RunTypes RunTypes;
        private static CutList SelectedCutList;
        private XmlDocument xmlDocument;
        private ProjectFile ProjectFile;
        private Project Project;
        private static string ProjectFilePath = "..//..//..//Files//Project.xml";


        public MainWindow()
        {
            InitializeComponent();
            OriginCuts = (CutList)XmlHelper.ReadXML(OriginCutsPath, typeof(CutList));
            CutsDataGrid.ItemsSource = OriginCuts;

            //OriginWagons = new WagonModelList();
            OriginWagons = (WagonModelList)XmlHelper.ReadXML(WagonFilePath, typeof(WagonModelList));
            ModelComboBox.ItemsSource = OriginWagons;

            RunTypes = new RunTypes();
            RunTypeComboBox.ItemsSource = RunTypes;

            ProjectFile = (ProjectFile)XmlHelper.ReadXML(ProjectFilePath, typeof(ProjectFile));
            Project = (Project)ProjectFile;
            SelectedCutsDataGrid.ItemsSource = Project.Cuts;

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
            WagonDetailsGrid.DataContext = Project.Cuts[SelectedCutsDataGrid.SelectedIndex];
        }

        private void DeleteCutButton_Click(object sender, RoutedEventArgs e)
        {
            if (CutsDataGrid.SelectedItem == null) return;
            OriginCuts.RemoveAt(CutsDataGrid.SelectedIndex);
        }

        private void NewCutButton_Click(object sender, RoutedEventArgs e)
        {
            Cut cut = new Cut();
            OriginCuts.Add(cut);
            CutsDataGrid.SelectedIndex = CutsDataGrid.Items.Count - 1;
        }

        private void WagonEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWagon editWagon = new EditWagon();
            editWagon.Show();
        }

        private void Save()
        {
            XmlHelper.WriteXML(ProjectFilePath, ProjectFile);
            XmlHelper.WriteXML(OriginCutsPath, OriginCuts);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save();
        }
    }
}
