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
        private static WagonList OriginWagons;
        private RunTypes RunTypes;
        private static CutList SelectedCutList;
        private XmlDocument xmlDocument;

        public MainWindow()
        {
            InitializeComponent();
            OriginCuts = new CutList();
            OriginCuts.ReadXML("..//..//..//Files//Cuts.xml");
            CutsDataGrid.ItemsSource = OriginCuts;

            OriginWagons = new WagonList();
            OriginWagons.ReadXML("..//..//..//Files//Cuts.xml");
            ModelComboBox.ItemsSource = OriginWagons;

            RunTypes = new RunTypes();
            RunTypeComboBox.ItemsSource = RunTypes;

            SelectedCutList = new CutList();
            xmlDocument = new XmlDocument();
            xmlDocument.Load("..//..//..//Files//Project.xml");
            XElement xmlDesignNode = (XElement)xmlDocument.SelectSingleNode("Profiles/DesignProfile");
            SelectedCutList.ReadXML(xmlDesignNode);
        }

        private void CutsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WagonDetailsGrid.DataContext = OriginCuts[CutsDataGrid.SelectedIndex];
        }

        private void SelectedCutDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WagonDetailsGrid.DataContext = OriginCuts[CutsDataGrid.SelectedIndex];
        }
    }
}
