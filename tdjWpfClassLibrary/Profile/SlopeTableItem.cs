using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public class SlopeTableItem : NotifyPropertyChanged
    {
        public double X1
        {
            get { return _x1; }
            set
            {
                if (value != _x1)
                {
                    _x1 = value;
                    OnPropertyChanged("X1");
                }
            }
        }
        private double _x1;

        public double X2
        {
            get { return _x2; }
            set
            {
                if (value != _x2)
                {
                    _x2 = value;
                    OnPropertyChanged("X2");
                }
            }
        }
        private double _x2;
        
        public double GradeLineY1 { get; set; }
        public double GradeLineY2 { get; set; }
    }
}
