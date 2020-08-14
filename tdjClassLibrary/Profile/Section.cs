using System;
using System.Collections.Generic;
using System.Text;

namespace tdjClassLibrary.Profile
{
    public interface ISection
    {
        double Length { get; set; }
        double Grade { get; set; }
        double Radii { get; set; }
        double? BeginAltitude { get; set; }
        double? EndAltitude { get; set; }
        double? BeginMileage { get; set; }
        double? EndMileage { get; set; }
        void CountBeginAltitude()
        {
            if (EndAltitude == null)
                BeginAltitude = null;
            else
                BeginAltitude = Grade * Length + EndAltitude;
        }
        void CountEndAltitude()
        {
            if (BeginAltitude == null)
                EndAltitude = null;
            else
                EndAltitude = BeginAltitude - Grade * Length;
        }
    }

    public class Section : ISection
    {
        public double Length { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Grade { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Radii { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double? BeginAltitude { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double? EndAltitude { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double? BeginMileage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double? EndMileage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
