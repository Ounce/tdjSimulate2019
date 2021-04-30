using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Wagon
{
    public class Axle 
    {
        /// <summary>
        /// 轴距，第一轴是距前端的距离。
        /// </summary>
        public double Distance { get; set; }

        public double Position { get; set; }

        public void Copy(Axle axle)
        {
            Distance = axle.Distance;
            Position = axle.Position;
        }
    }
}
