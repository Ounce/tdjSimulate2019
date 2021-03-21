using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjWpfClassLibrary.Wagon
{
    /// <summary>
    /// 同型号的一组车辆。包括载重也相同。
    /// </summary>
    public class Wagons : Wagon
    {
        public int Count
        {
            get => _count;
            set
            {
                if (value != _count)
                {
                    _count = value;
                    OnPropertyChanged("Count");
                }
            }
        }
        private int _count;
    }

    public class Wagon : WagonModel
    {
        /// <summary>
        /// 车辆全长。
        /// </summary>
        public double Length
        {
            get => _length;
            set
            {
                if (value != _length)
                {
                    _length = value;
                    OnPropertyChanged("Length");
                }
            }
        }
        private double _length;

        /// <summary>
        /// 车辆总重。
        /// </summary>
        public double Weight
        {
            get => _weight;
            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        private double _weight;

        /// <summary>
        /// 型号。
        /// </summary>
        public string Model
        {
            get => _model;
            set
            {
                if (value != _model)
                {
                    _model = value;
                    OnPropertyChanged("Model");
                }
            }
        }
        private string _model;

        /// <summary>
        ///轴位置
        /// </summary>
        public List<double> AxisPositions;
    }

    public enum WagonType { C, P, N };

    public class WagonModel : NotifyPropertyChanged
    {
        public WagonType Type 
        { 
            get => _type; 
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        private WagonType _type;

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;

        /// <summary>
        /// 轴距，第一轴是距前端的距离。
        /// </summary>
        public List<double> AxisDistances;
    }

    public static class WagonTypes 
    {
        public static List<WagonModel> Items = new List<WagonModel>()
        {
            new WagonModel() { Type = WagonType.C, Name = "敞车"},
            new WagonModel() { Type = WagonType.P, Name = "棚车"},
            new WagonModel() { Type = WagonType.N, Name = "平车"}
        };
    };

    public class WagonList : ObservableCollection<Wagon>
    {
        public WagonList()
        {
            ReadJSON();
        }

        private void ReadJSON()
        {

        }
    }
}
