using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public class ProfileViewModelOption
    {
        public double SlopeTableBorderWidth { get; set; } = 2;
        public double SlopeTableLineWidth { get; set; } = 1;
        public double SlopeTableFont { get; set; } = 12;
        public double SlopeTableHeight { get; set; } = 27;
        public Brush SlopeTableColor { get; set; } = Brushes.Red;
    }
}
