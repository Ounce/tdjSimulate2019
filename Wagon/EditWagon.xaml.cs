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
        public EditWagon()
        {
            InitializeComponent();
            WagonListDataGrid.ItemsSource = MainWindow.OriginWagons;
            WagonCategoryComboBox.ItemsSource = new WagonCategories();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XmlHelper.WriteXML(MainWindow.WagonFilePath, MainWindow.OriginWagons);
        }

        private void AddWagonButton_Click(object sender, RoutedEventArgs e)
        {
            WagonModel w = new WagonModel();
            MainWindow.OriginWagons.Add(w);
            WagonListDataGrid.SelectedIndex = WagonListDataGrid.Items.Count - 1;
        }

        private void DeleteWagonButton_Click(object sender, RoutedEventArgs e)
        {
            if (WagonListDataGrid.SelectedItem == null) return;
            MainWindow.OriginWagons.RemoveAt(WagonListDataGrid.SelectedIndex);
        }
    }


}
