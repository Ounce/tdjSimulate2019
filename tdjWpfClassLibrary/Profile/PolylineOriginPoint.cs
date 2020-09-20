using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    public class PolylineOriginPoint
    {
        public double X;
        public double Y;

        public PolylineOriginPoint()
        {
            new PolylineOriginPoint(0, 0);
        }

        public PolylineOriginPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 设置图形左上方原点 Y 坐标。
        /// </summary>
        public void SetY(VerticalAlignment verticalAlignment, double height, double maxAltitude, double minAltitude, double verticalScale)
        {
            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                    Y = PolylinePoint.GetPointY(maxAltitude, verticalScale);
                    break;
                case VerticalAlignment.Center:
                    //（- TopAltitude + MaxAltitude）* Scale.Vertical = (canvasHeight + (MaxAltitude - MinAltitude) * Scale.Vertical) * 0.5
                    //- TopAltitude * Scale.Vertical + MaxAltitude * Scale.Vertical = 0.5 * canvasHeight + 0.5 * (MaxAltitude - MinAltitude) * Scale.Vertical
                    //- OriginPoint.Y = 0.5 * canvasHeight + 0.5 * MaxAltitude * Scale.Verrtical + 0.5 * MinAltitude * Scale.Vertical
                    Y = 0.5 * (PolylinePoint.GetPointY(maxAltitude + minAltitude, verticalScale) - height);
                    break;
                case VerticalAlignment.Bottom:
                    Y = PolylinePoint.GetPointY(minAltitude, verticalScale) - height;
                    break;
            }
        }


        /// <summary>
        /// 设置图形左上方原点 X 坐标。
        /// </summary>
        public void SetX(HorizontalAlignment horizontalAlignment, double width, double length, double horizontalScale)
        {
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    X = 0;
                    break;
                case HorizontalAlignment.Center:
                    X = (width - length * horizontalScale) * 0.5;
                    break;
                case HorizontalAlignment.Right:
                    X = width - length * horizontalScale;
                    break;
            }
        }
    }
}
