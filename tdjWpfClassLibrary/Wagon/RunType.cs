using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace tdjWpfClassLibrary.Wagon
{
    /// <summary>
    /// 走行性能分类。
    /// </summary>
    public enum RunType 
    { 
        [Description("易")]
        Y = -1,
        [Description("中")]
        Z = 0,
        [Description("难")]
        N = 1,
        [Description("极易")]
        GY = -2,
        [Description("极难")]
        GN = 2
    }

    /// <summary>
    /// 得到枚举的DescriptionAttribute值。此类未测试。
    /// </summary>
    public static class EnumHelper
    {
        static public string GetDescription(Enum en)
        {
            Type enumType = en.GetType();
            var name = Enum.GetName(enumType, en);
            if (name == null)
                return string.Empty;
            object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
                return string.Empty;
            DescriptionAttribute attr = objs[0] as DescriptionAttribute;
            return attr.Description;
        }
    }
}
