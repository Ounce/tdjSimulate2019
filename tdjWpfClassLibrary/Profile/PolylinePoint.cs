using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    public static class PolylinePoint
    {
        public static Point GetPoint(double mileage, double altitude, Scale scale)
        {
            return new Point(mileage * scale.Horizontal, GetPointY(altitude, scale.Vertical));
        }

        public static double GetPointY(double altitude, double verticalScale)
        {
            return - altitude * verticalScale;
        }
    }
}
