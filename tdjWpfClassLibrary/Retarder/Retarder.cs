using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace tdjWpfClassLibrary.Retarder
{
    public class RetarderList : ObservableCollection<Retarder>
    {

    }
    public class Retarder : RetarderModel
    {
        public Positions Positions { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
        private int _quantity;

        /// <summary>
        /// 临界速度
        /// </summary>
        public double Speed
        {
            get => _speed;
            set
            {
                if (value != _speed)
                {
                    _speed = value;
                    OnPropertyChanged("Speed");
                }
            }
        }
        private double _speed;

        /// <summary>
        /// 临界速度偏差
        /// </summary>
        public double SpeedDeviation
        {
            get => _speedDeviation;
            set
            {
                if (value != _speedDeviation)
                {
                    _speedDeviation = value;
                    OnPropertyChanged("SpeedDeviation");
                }
            }
        }
        private double _speedDeviation;
    }

    public class RetarderModel : NotifyPropertyChanged
    {
        public Guid ID { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }


    }

    /// <summary>
    /// 普通减速顶列表。
    /// </summary>
    [XmlRoot("Retarders")]
    public class RetarderModelList : ModelCollection<RetarderModel>
    {
        public RetarderModelList() { }
    }
}
