using System;
using System.Collections.Generic;
using System.Text;

namespace tdjClassLibrary.Profile
{
    public interface ISlope
    {
        double Length { get; set; }
        double Grade { get; set; }
    }
}
