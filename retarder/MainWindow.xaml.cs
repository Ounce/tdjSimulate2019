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
using tdjWpfClassLibrary;

namespace retarder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Project Project { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Project = (Project)XmlHelper.ReadXML("..//..//..//Files//Project.xml", typeof(Project));
            if (Project != null)
            {
                retarderDataGrid.ItemsSource = Project.Retarders;
            }
        }
    }
}
