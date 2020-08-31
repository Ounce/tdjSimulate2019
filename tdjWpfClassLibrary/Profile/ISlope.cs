using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public interface ISlope
    {
        double Length { get; set; }
        double Grade { get; set; }
    }
}
