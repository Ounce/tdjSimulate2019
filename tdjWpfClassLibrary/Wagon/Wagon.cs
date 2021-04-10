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

        public Wagons()
        {
        }
    }

    public class Wagon : WagonModel
    {
    }

    public class WagonModel : NotifyPropertyChanged
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
                    WagonModel w = WagonHelper.GetWagonModel(value);
                    if (w == null) return;
                    Copy(w);
                }
            }
        }
        private string _model;

        /// <summary>
        /// 轴距，第一轴是距前端的距离。
        /// </summary>
        public ObservableCollection<Axis> Axises { get; set; }

        public WagonModel()
        {
            Category = WagonCategory.C;
            Axises = new ObservableCollection<Axis>();
        }


        /// <summary>
        /// 复制WagonModel类中的各个属性。用于改变 型号 时修改相关参数。
        /// </summary>
        /// <param name="wagonModel"></param>
        public void Copy(WagonModel wagonModel)
        {
            Category = wagonModel.Category;
            Length = wagonModel.Length;
            if (Axises == null)
                Axises = new ObservableCollection<Axis>();
            else
                Axises.Clear();
            foreach (var a in wagonModel.Axises)
            {
                Axis axis = new Axis();
                axis.Distance = a.Distance;
                axis.Position = a.Position;
                Axises.Add(axis);
            }
        }
    }

    /// <summary>
    /// 各种车型的列表。
    /// </summary>
    [XmlRoot("Wagons")]
    public class WagonModelList : ObservableCollection<WagonModel>
    {
        public WagonModelList() { }
        public WagonModel FindByModel(string model)
        {
            foreach (var w in this)
            {
                if (w.Model == model)
                    return w;
            }
            return null;
        }
    }

    public static class WagonHelper
    {
        public static string WagonFilePath = "..//..//..//Files//Wagons.xml";
        private static WagonModelList WagonModelList = (WagonModelList)XmlHelper.ReadXML(WagonFilePath, typeof(WagonModelList));
        public static WagonModel GetWagonModel(string model)
        {
            if (WagonModelList == null) return null;
            return WagonModelList.FindByModel(model);
        }
    }
}
