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
 
    public class RunTypes : Dictionary<RunType, string>
    {
        public RunTypes()
        {
            Add(RunType.Y, EnumHelper.GetDescription(RunType.Y));
            Add(RunType.Z, EnumHelper.GetDescription(RunType.Z));
            Add(RunType.N, EnumHelper.GetDescription(RunType.N));
            Add(RunType.GY, EnumHelper.GetDescription(RunType.GY));
            Add(RunType.GN, EnumHelper.GetDescription(RunType.GN));
            /*
            foreach (RunType item in Enum.GetValues(typeof(RunType)))
                Add(item, EnumHelper.GetDescription(item));*/
        }
    }
}
