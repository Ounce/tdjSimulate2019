using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace tdjWpfClassLibrary.Project
{
    public enum PageType 
    {
        [Description("项目")]
        Project,
        [Description("工况")]
        Check,
        [Description("线路")]
        Tracks,
        [Description("股道")]
        Track,
        [Description("纵断面")]
        Profile,
        [Description("道岔")]
        Switch,
        [Description("曲线")]
        Curve,
        [Description("警冲标")]
        Warning,
        [Description("停车点")]
        StopPoint,
        [Description("减速顶")]
        Retarder,
        [Description("可控顶")]
        CtrRetarder,
        [Description("停车顶")]
        Arrester,
        [Description("车组")]
        Cut,
        [Description("阻力")]
        Resistance

    }
}
