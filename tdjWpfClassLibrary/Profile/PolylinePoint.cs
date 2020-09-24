﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    public static class PolylinePoint
    { 
        public static Point GetPoint(double mileage, double altitude)
        {
            return new Point(mileage * Scale.Horizontal, GetPointY(altitude, Scale.Vertical));
        }

        public static double GetPointY(double altitude, double verticalScale)
        {
            return - altitude * verticalScale;
        }
    }
}