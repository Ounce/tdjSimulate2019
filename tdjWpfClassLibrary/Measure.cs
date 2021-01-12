using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary
{
    public class Measure : NotifyPropertyChanged
    {
        public double Length;
        public double Weight;
        public string LengthName;
        public string WeightName;
    }

    public static class MetricMeasure : Measure
    {

    }
}
