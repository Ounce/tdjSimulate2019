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
    /// 车组。车型暂时为同型同重同走行性能，将来区分为不同车型等。
    /// </summary>
    public class Cut : Wagons
    {
        [XmlAttribute("Name")]
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
        /// 车组情况简要说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 初始位置。如果为车组内的车辆其值为-1。
        /// </summary>
        public double InitPosition
        {
            get => _initPosition;
            set
            {
                if (value != _initPosition)
                {
                    _initPosition = value;
                    OnPropertyChanged("InitPosition");
                }
            }
        }
        private double _initPosition;

        /// <summary>
        /// 初始速度。一般为推峰速度。
        /// </summary>
        public double InitSpeed
        {
            get => _initSpeed;
            set
            {
                if (value != _initSpeed)
                {
                    _initSpeed = value;
                    OnPropertyChanged("InitSpeed");
                }
            }
        }
        private double _initSpeed;
        /// <summary>
        /// 位置（前端）。模糊车钩钩舌内侧与车辆端板之间的空间。
        /// </summary>
        [XmlIgnore]
        public double Position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
        private double _position;

        /// <summary>
        ///轴位置
        /// </summary>
        //public List<double> AxisPositions;

        public RunType RunType 
        { 
            get => _runType;
            set
            {
                if (value != _runType)
                {
                    _runType = value;
                    OnPropertyChanged("RunType");
                }
            }
        }
        private RunType _runType;

        public Cut()
        {
            Description = "";
            _initPosition = -1;
        }

        /// <summary>
        /// 深度复制
        /// </summary>
        /// <param name="cut"></param>
        public void Copy(Cut cut)
        {
            Name = cut.Name;
            Description = cut.Description;
            WagonModelID = cut.WagonModelID;
            WagonModel = cut.WagonModel;
            Count = cut.Count;
            RunType = cut.RunType;
            Weight = cut.Weight;
            InitPosition = cut.InitPosition;
            InitSpeed = cut.InitSpeed;
            if (Axises == null)
                Axises = new ObservableCollection<Axis>();
            else
                Axises.Clear();
            foreach (var a in cut.Axises)
            {
                Axis axis = new Axis();
                axis.Distance = a.Distance;
                axis.Position = a.Position;
                Axises.Add(axis);
            }
        }

    }

    [XmlRoot("Cuts")]
    public class CutList : ObservableCollection<Cut>
    {
    }

    public static class CutHelper
    {
        public static string OriginCutsPath = "..//..//..//Files//Cuts.xml";
        public static CutList OriginCuts;
        static CutHelper()
        {
            OriginCuts = (CutList)XmlHelper.ReadXML(OriginCutsPath, typeof(CutList));
            foreach (var w in OriginCuts)
                w.WagonModel = WagonHelper.GetWagonModel(w.WagonModelID);
        }
    }
}
