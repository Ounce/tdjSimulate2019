using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace tdjClassLibrary.Profile
{
    /// <summary>
    /// 纵断面比例图。高程比例与里程比例不同。
    /// </summary>
    public class ScaleDrawing : BaseDrawing
    {
        /// <summary>
        /// 图形顶端对应的高程。
        /// </summary>
        public double TopAltitude { get; set; }

        /// <summary>
        /// 图形底部对应的高程。
        /// </summary>
        public double BottomAltitude { get; set; }


    }
}
