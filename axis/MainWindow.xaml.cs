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
            NumberAxis axis = new NumberAxis();
            axis.AddTickMarks(1);   // 此函数 需在SetValue之前使用，它没有计算刻度线的位置。
            axis.AddTickMarks(0.5);
            axis.AddTickMarks(0.1);
            axis.AddTickMarks(1);  
            axis.AddTickMarks(0.5);
            axis.AddTickMarks(0.1);
            axis.SetValue(0, 10, 100);
            grid.DataContext = axis.MultiTicks[0];
            Ticks5.ItemsSource = axis.MultiTicks[1];
            Ticks10.ItemsSource = axis.MultiTicks[2];
            TicksV1.ItemsSource = axis.MultiTicks[3];
            TicksV1L.ItemsSource = axis.MultiTicks[3];
            //   ExistPolylineTranslate.Y = 100;
            //   ExistPolylineTranslate.X = 1000;
        }
    }
}
