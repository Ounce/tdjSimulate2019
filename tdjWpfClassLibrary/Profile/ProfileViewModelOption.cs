using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public class ProfileViewModelOption : NotifyPropertyChanged
    {
        public double SlopeTableBorderWidth
        {
            get { return _slopeTableBorderWidth; }
            set
            {
                if (value != _slopeTableBorderWidth)
                {
                    _slopeTableBorderWidth = value;
                    OnPropertyChanged("SlopeTableBorderWidth");
                }
            }
        }
        private double _slopeTableBorderWidth = 2;

        public double SlopeTableLineWidth
        {
            get { return _slopeTableLineWidth; }
            set
            {
                if (value != _slopeTableLineWidth)
                {
                    _slopeTableLineWidth = value;
                    OnPropertyChanged("SlopeTableLineWidth");
                }
            }
        }
        private double _slopeTableLineWidth = 1;
        public double SlopeTableFont
        {
            get { return _slopeTableFont; }
            set
            {
                if (value != _slopeTableFont)
                {
                    _slopeTableFont = value;
                    OnPropertyChanged("SlopeTableFont");
                }
            }
        }
        private double _slopeTableFont = 12;

        public double SlopeTableHeight
        {
            get { return _slopeTableHeight; }
            set
            {
                if (value != _slopeTableHeight)
                {
                    _slopeTableHeight = value;
                    OnPropertyChanged("SlopeTableHeight");
                }
            }
        }
        private double _slopeTableHeight = 27;

        public Brush SlopeTableColor 
        {
            get { return _slopeTableColor; }
            set
            {
                if (value != _slopeTableColor)
                {
                    _slopeTableColor = value;
                    OnPropertyChanged("SlopeTableColor");
                }
            }
        }
        private Brush _slopeTableColor;// = Brushes.Red;
    }
}
