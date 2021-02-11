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

using tdjWpfClassLibrary.Draw;

namespace axis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NumberAxis axis = new NumberAxis("高程（米）");
            axis.AddTickMarks(1);   // 此函数 需在SetValue之前使用，它没有计算刻度线的位置。
            axis.SetValue(0, 10, 100);
            grid.DataContext = axis.MultiTicks[0];
         //   ExistPolylineTranslate.Y = 100;
         //   ExistPolylineTranslate.X = 1000;
        }
    }
}
