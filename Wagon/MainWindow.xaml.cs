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
        private RunTypes RunTypes;
        /*
        private static CutList SelectedCutList;
        private XmlDocument xmlDocument;
        */
        private ProjectFile ProjectFile;
        private Project Project;
        private static string ProjectFilePath = "..//..//..//Files//Project.xml";


        public MainWindow()
        {
            InitializeComponent();
            
            CutsDataGrid.ItemsSource = CutHelper.OriginCuts;

            ModelComboBox.ItemsSource = BaseData.Wagons; //WagonHelper.WagonModelList;

            RunTypes = new RunTypes();
            RunTypeComboBox.ItemsSource = RunTypes;

            ProjectFile = (ProjectFile)XmlHelper.ReadXML(ProjectFilePath, typeof(ProjectFile));
            foreach (var w in ProjectFile.Cuts)
                w.WagonModel = WagonHelper.GetWagonModel(w.WagonModelID);
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
            if (CutsDataGrid.SelectedItem == null) return;
            WagonDetailsGrid.DataContext = CutHelper.OriginCuts[CutsDataGrid.SelectedIndex];
        }

        private void CutsDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CutsDataGrid.SelectedItem == null) return;
            WagonDetailsGrid.DataContext = CutHelper.OriginCuts[CutsDataGrid.SelectedIndex];
        }

        private void SelectedCutDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedCutsDataGrid.SelectedItem == null) return;
            WagonDetailsGrid.DataContext = Project.Cuts[SelectedCutsDataGrid.SelectedIndex];
        }

        private void SelectedCutsDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SelectedCutsDataGrid.SelectedItem == null) return;
            WagonDetailsGrid.DataContext = Project.Cuts[SelectedCutsDataGrid.SelectedIndex];
        }

        private void DeleteCutButton_Click(object sender, RoutedEventArgs e)
        {
            if (CutsDataGrid.SelectedItem == null) return;
            CutHelper.OriginCuts.RemoveAt(CutsDataGrid.SelectedIndex);
        }

        private void NewCutButton_Click(object sender, RoutedEventArgs e)
        {
            Cut cut = new Cut();
            CutHelper.OriginCuts.Add(cut);
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
            XmlHelper.WriteXML(CutHelper.OriginCutsPath, CutHelper.OriginCuts);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (CutsDataGrid.SelectedItem == null) return;
            Cut cut = new Cut();
            cut.Copy(CutHelper.OriginCuts[CutsDataGrid.SelectedIndex]);
            Project.Cuts.Add(cut);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCutsDataGrid.SelectedItem == null) return;
            Project.Cuts.RemoveAt(SelectedCutsDataGrid.SelectedIndex);
        }

        private void MoveUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCutsDataGrid.SelectedItem == null || SelectedCutsDataGrid.SelectedIndex == 0) return;
            Project.Cuts.Move(SelectedCutsDataGrid.SelectedIndex, SelectedCutsDataGrid.SelectedIndex - 1);
        }

        private void MoveDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCutsDataGrid.SelectedItem == null || SelectedCutsDataGrid.SelectedIndex == SelectedCutsDataGrid.Items.Count - 1) return;
            Project.Cuts.Move(SelectedCutsDataGrid.SelectedIndex, SelectedCutsDataGrid.SelectedIndex + 1);
        }
    }
}
