using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using tdjWpfClassLibrary.Draw;

namespace tdjWpfClassLibrary.Profile
{
    interface IGraphPosition
    {
        HorizontalAlignment HorizontalAlignment { get; set; } 
        VerticalAlignment VerticalAlignment { get; set; }

        double CanvasActualWidth { get; set; }
        double CanvasActualHeight { get; set; }

        /// <summary>
        /// 图形在画布显示范围内的左上角坐标。
        /// </summary>
        Point LeftTop { get; set; }

        /// <summary>
        /// 设置图形在画布显示范围内的左上角坐标。参数坐标为实际值 * 比例，LeftTop为屏幕坐标。需要设置位置偏移量将图形移到显示区。
        /// </summary>
        /// <param name="left">图形水平方向的起点坐标。（起点参数 * 水平比例）</param>
        /// <param name="right">图形水平方向的终点坐标。（终点参数 * 水平比例）</param>
        /// <param name="top">图形顶端坐标。（顶端 * 垂直比例）</param>
        /// <param name="bottom">图形底端坐标。（底端 * 垂直比例）</param>
        Point SetLeftTop(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, double canvasActualWidth, double canvasActualHeight, double left, double right, double top, double bottom)
        {
            double l, t;
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                default:
                    l = 0;
                    break;
                case HorizontalAlignment.Center:
                    l = (CanvasActualWidth - (right - left)) * 0.5;
                    break;
                case HorizontalAlignment.Right:
                    l = (CanvasActualWidth - (right - left));
                    break;
            }
            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:
                default:
                    t = ValueConverter.VerticalValue(top);
                    break;
                case VerticalAlignment.Center:
                    t = ValueConverter.VerticalValue(top + (canvasActualHeight - (top - bottom)) * 0.5);
                    break;
                case VerticalAlignment.Bottom:
                    t = ValueConverter.VerticalValue(canvasActualHeight + bottom);
                    break;
            }
            return new Point(l, t);
        }
    }
}
