using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tdjWpfClassLibrary;
using tdjWpfClassLibrary.Wagon;

namespace Wagon
{
    /// <summary>
    /// EditWagon.xaml 的交互逻辑
    /// </summary>
    public partial class EditWagon : Window
    {
        public WagonModelList Wagons;
        public EditWagon()
        {
            InitializeComponent();
            string path = "..//..//..//Files//Wagons.xml";
            Wagons = (WagonModelList)XmlHelper.ReadXML(path, typeof(WagonModelList));
            WagonListDataGrid.ItemsSource = Wagons;
            WagonCategoryComboBox.ItemsSource = new WagonCategories();
        }
    }
}
