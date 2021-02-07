using System;
using System.Collections.Generic;
using System.Text;

using tdjWpfClassLibrary.Draw;

namespace tdjWpfClassLibrary
{
    public static class Converter
    {
        /// <summary>
        /// 转换 垂直 与 水平。
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
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
