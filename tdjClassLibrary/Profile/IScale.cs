using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace tdjClassLibrary.Profile
{
    interface IScale
    {
        /// <summary>
        /// 水平比例。
        /// </summary>
        public double HorizontalScale { get; set; }

        /// <summary>
        /// 垂直比例，应根据具体应用自行定义。
        /// </summary>
<<<<<<< HEAD:tdjClassLibrary/Profile/ScaleDrawing.cs
        public double BottomAltitude { get; set; }


=======
        public double VerticalScale { get; set; }
>>>>>>> multiDrawingClass:tdjClassLibrary/Profile/IScale.cs
    }
}
