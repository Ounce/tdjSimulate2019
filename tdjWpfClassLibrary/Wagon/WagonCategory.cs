using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace tdjWpfClassLibrary.Wagon
{
    /// <summary>
    /// 车辆分类。
    /// </summary>
    public enum WagonCategory 
    {
        /// <summary>
        /// 敞车
        /// </summary>
        [Description("敞车")]
        C = 1,
        /// <summary>
        /// 棚车
        /// </summary>
        [Description("棚车")]
        P = 2,
        /// <summary>
        /// 平车
        /// </summary>
        [Description("平车")]
        N = 3,
        /// <summary>
        /// 罐车
        /// </summary>
        [Description("罐车")]
        G = 4,
        /// <summary>
        /// 家畜车
        /// </summary>
        [Description("家畜车")]
        J = 5
    }

    /// <summary>
    /// 车辆分类目录
    /// </summary>
    public class WagonCategories : EnumDictionary<WagonCategory> { }
}
