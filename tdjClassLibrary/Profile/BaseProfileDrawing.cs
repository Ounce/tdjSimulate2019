using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjClassLibrary.Profile
{
    /// <summary>
    /// 基于ProfileViewModel的图形类；
    /// </summary>
    public class BaseProfileDrawing : BaseDrawing
    {
        public ProfileViewModel Profile { get; set; }
    }
}
