using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace tdjWpfClassLibrary.Wagon
{
    /// <summary>
    /// 同型号的一组车辆。包括载重也相同。
    /// </summary>
    public class Wagons : NotifyPropertyChanged
    {
        public Guid WagonModelID
        {
            get => _wagonModelID;
            set
            {
                if (value != _wagonModelID)
                {
                    _wagonModelID = value;
                    OnPropertyChanged("WagonModelID");
                    WagonModel = WagonHelper.GetWagonModel(value);
                    WagonModel.PropertyChanged += WagonModelPropertyChanged;
                }
            }
        }
        private Guid _wagonModelID;

        [XmlIgnore]
        public WagonModel WagonModel 
        { 
            get => _wagonModel;
            set
            {
                if (value != _wagonModel)
                {
                    _wagonModel = value;
                    UpdateAxises();
                    OnPropertyChanged("WagonModel");
                    OnPropertyChanged("Model");
                }
            }
        }
        private WagonModel _wagonModel;

        [XmlIgnore]
        public string Model
        {
            get => WagonModel.Model;
        }

        [XmlIgnore]
        public WagonCategory Category
        {
            get => WagonModel.Category;
        }

        public int Count
        {
            get => _count;
            set
            {
                if (value != _count)
                {
                    _count = value;
                    UpdateAxises();
                    OnPropertyChanged("Count");
                }
            }
        }
        private int _count;

        /// <summary>
        /// 单辆车总重。
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
        /// 轴距，第一轴是距前端的距离。
        /// </summary>
        public ObservableCollection<Axle> Axles { get; set; }

        public Wagons()
        {
            WagonModel = new WagonModel();
            Axles = new ObservableCollection<Axle>();
            WagonModel.PropertyChanged += WagonModelPropertyChanged;
        }

        /// <summary>
        /// 监视WagonModel属性变化。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WagonModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Model":
                    OnPropertyChanged("Model");
                    break;
                case "Length":
                    OnPropertyChanged("Length");
                    break;
            }
        }

        private void UpdateAxises()
        {
            if (Count == 0) return;
            double p = 0;
            double np = 0;
            Axles.Clear();
            for (int i = 0; i < _count; i++) 
            { 
                for (int j = 0; j < WagonModel.Axles.Count; j++)
                {
                    Axle na = new Axle();
                    if (j == 0)
                    {
                        na.Position = p + WagonModel.Axles[0].Distance;
                    }
                    else
                    {
                        na.Position = np + WagonModel.Axles[j].Distance;
                    }
                    Axles.Add(na);
                    np = na.Position;
                }
                p += WagonModel.Length;
            }
            p = 0;
            foreach (var a in Axles)
            {
                a.Distance = a.Position - p;
                p = a.Position;
            }
        }
    }

    public class Wagon : WagonModel
    {
    }

    public class WagonModel : NotifyPropertyChanged
    {
        [XmlAttribute("ID")]
        public Guid ID { get; set; }

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
        /// 轴距，第一轴是距前端的距离。
        /// </summary>
        public ObservableCollection<Axle> Axles { get; set; }

        public WagonModel()
        {
            ID = Guid.NewGuid();
            Category = WagonCategory.C;
            Axles = new ObservableCollection<Axle>();
        }

        /// <summary>
        /// 复制WagonModel类中的各个属性。用于改变 型号 时修改相关参数。
        /// </summary>
        /// <param name="wagonModel"></param>
        public void Copy(WagonModel wagonModel)
        {
            Model = wagonModel.Model;
            Category = wagonModel.Category;
            Length = wagonModel.Length;
            if (Axles == null)
                Axles = new ObservableCollection<Axle>();
            else
                Axles.Clear();
            foreach (var a in wagonModel.Axles)
            {
                Axle axis = new Axle();
                axis.Distance = a.Distance;
                axis.Position = a.Position;
                Axles.Add(axis);
            }
        }
    }

    /// <summary>
    /// 各种车型的列表。
    /// </summary>
    [XmlRoot("Wagons")]
    public class WagonModelList : Collection<WagonModel>
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
        /*
        public WagonModel Find(Guid id)
        {
            foreach (var w in this)
            {
                if (w.ID == id)
                    return w;
            }
            return null;
        }
        */
    }

    public static class WagonHelper
    {
        public static string WagonFilePath = "..//..//..//Files//Wagons.xml";
        public static WagonModelList WagonModelList = (WagonModelList)XmlHelper.ReadXML(WagonFilePath, typeof(WagonModelList));
        public static WagonModel GetWagonModel(string model)
        {
            //if (WagonModelList == null) return null;
            return WagonModelList.FindByModel(model);
        }

        public static WagonModel GetWagonModel(Guid id)
        {
            //if (WagonModelList == null) return null;
            return WagonModelList.Find(id);
        }

        public static void WriteWagonModelList()
        {
            XmlHelper.WriteXML(WagonFilePath, WagonModelList);
        }

        /// <summary>
        /// 为WagonModelList中ID为空的Model设置一个有效的ID。
        /// </summary>
        public static void SetWagonModelGuid()
        {
            foreach (var i in WagonModelList)
            {
                if (i.ID.Equals(Guid.Empty))
                {
                    i.ID = Guid.NewGuid();
                }
            }
        }

        /// <summary>
        /// 型号 是否已经存在。不考虑exceptModel。
        /// </summary>
        /// <param name="model">查找型号</param>
        /// <param name="exceptModel">排除型号</param>
        /// <returns></returns>
        public static bool IsExist(string model, string exceptModel)
        {
            if (WagonModelList == null || exceptModel == null) return false;
            if (model == exceptModel) return false;
            foreach (var i in WagonModelList)
            {
                if (i.Model != exceptModel && i.Model == model)
                    return true;
            }
            return false;
        }


    }
}
