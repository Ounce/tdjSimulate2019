using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

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

    public class Wagon : NotifyPropertyChanged
    {
        [XmlAttribute("Category")]
        public WagonCategory Category
        {
            get => _category;
            set
            {
                if (value != _category)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        private WagonCategory _category;

        /// <summary>
        /// 车辆全长。
        /// </summary>
        [XmlAttribute("Length")]
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
        [XmlAttribute("Weight")]
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
        [XmlAttribute("Model")]
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

        /// <summary>
        /// 轴距，第一轴是距前端的距离。
        /// </summary>
        public List<double> AxisDistances { get; set; }

        public Wagon()
        {
            AxisDistances = new List<double>();
        }
    }

    /// <summary>
    /// 各种车型的列表。
    /// </summary>
    [XmlRoot("Wagons")]
    public class WagonList : ObservableCollection<Wagon>
    {
        public WagonList() { }
    }
}
