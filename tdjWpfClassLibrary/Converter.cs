using System;
using System.Collections.Generic;
using System.Text;

using tdjWpfClassLibrary.Draw;

namespace tdjWpfClassLibrary
{
    public static class Converter
    {
        public static AxisDirection DirectionChange(AxisDirection direction)
        {
            switch (direction)
            {
                case AxisDirection.Horizontal:
                    return AxisDirection.Vertical;
                case AxisDirection.Vertical:
                default:
                    return AxisDirection.Horizontal;
            }
        }
    }
}
