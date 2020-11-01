using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace tdjWpfClassLibrary.Profile
{
    class SlopeTable 
    {
        public double Top;
        public double Height;
        public double Mileage;
    }

    public class SlopeLinePoints : NotifyPropertyChanged
    {
        public Line Line;
        public double X1
        {
            get { return Line.X1; }
            set
            {
                if (value != Line.X1)
                {
                    Line.X1 = value;
                    OnPropertyChanged("X1");
                }
            }
        }
        public double Y1
        {
            get { return Line.Y1; }
            set
            {
                if (value != Line.Y1)
                {
                    Line.Y1 = value;
                    OnPropertyChanged("Y1");
                }
            }
        }

        public double X2
        {
            get { return Line.X2; }
            set
            {
                if (value != Line.X2)
                {
                    Line.X2 = value;
                    OnPropertyChanged("X2");
                }
            }
        }

        public double Y2
        {
            get { return Line.Y2; }
            set
            {
                if (value != Line.Y2)
                {
                    Line.Y2 = value;
                    OnPropertyChanged("Y2");
                }
            }
        }
    }

    public class SlopeLines : ObservableCollection<Line>
    {
        // Test
        public SlopeLines()
        {
            Line a = new Line();
            a.X1 = 10;
            a.Y1 = 10;
            a.X2 = 100;
            a.Y2 = 100;
            Add(a);
            Line b = new Line();
            b.X1 = 200;
            b.Y1 = 100;
            b.X2 = 100;
            b.Y2 = 100;
            Add(b);
        }
    }
}
