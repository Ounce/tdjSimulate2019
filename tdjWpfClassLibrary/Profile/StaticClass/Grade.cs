using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile.StaticClass
{
    /// <summary>
    /// 与坡度有关的静态类。
    /// </summary>
    public static class Grade
    {
        /// <summary>
        /// 坡度单位，‰为1000；%为100；国内和欧洲为1000，美国为100。
        /// </summary>
        public static int Unit = 1000;

        /// <summary>
        /// 判断坡度的方向，返回值为-1，反坡；0，平坡；1，顺坡。
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static int Direction(double grade)
        {
            if (grade < -0.00001) return -1;
            else if (grade > 0.00001) return 1;
            else return 0;
        }
    }
}
