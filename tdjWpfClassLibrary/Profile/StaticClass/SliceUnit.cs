using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile.StaticClass
{
    public enum Measure { Metric, British }
    class SliceUnit : NotifyPropertyChanged
    {

    }

    public class LengthMeasure
    {
        public Measure Measure { get; set; }
        private double[] metric = { 0.001, 0.01, 0.1, 1, 10, 100, 1000 };
        private double[] british = { };
    }
}
