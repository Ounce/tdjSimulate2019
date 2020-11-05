using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    class ProfileViewModelCollection : NotifyPropertyChanged
    {
        public ObservableCollection<ProfileViewModel> Items;

        /// <summary>
        /// 最大高程。
        /// </summary>
        public double MaxAltitude
        {
            get { return _maxAltitude; }
        }
        public double _maxAltitude;

        /// <summary>
        /// 最小高程。
        /// </summary>
        public double MinAltitude
        {
            get { return _minAltitude; }
        }
        public double _minAltitude;

        /// <summary>
        /// 最长的纵断面全长。
        /// </summary>
        public double Length
        {
            get
            {
                double l = 0;
                foreach (var s in Items)
                {
                    if (l < s.Length) l = s.Length;
                }
                return l;
            }
        }

        public PolylineOriginPoint PolylineOriginPoint;

        public double canvasWidth, canvasHeight;

        public HorizontalAlignment HorizontalAlignment
        {
            set
            {
                if (value != _horizontalAlignment)
                {
                    _horizontalAlignment = value;
                    PolylineOriginPoint.SetX(_horizontalAlignment, canvasWidth, Length);
                    OnPropertyChanged("HorizontalAlignment");
                }
            }

        }
        private HorizontalAlignment _horizontalAlignment;

        public VerticalAlignment PolylineVerticalAlignment
        {
            set
            {
                if (value != _verticalAlignment)
                {
                    _verticalAlignment = value;
                    PolylineOriginPoint.SetY(_verticalAlignment, canvasHeight, _maxAltitude, _minAltitude);
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        public ProfileViewModelCollection()
        {
            _maxAltitude = StaticClass.Altitude.InitMax;
            _minAltitude = StaticClass.Altitude.InitMin;
            Items = new ObservableCollection<ProfileViewModel>();
            Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ItemsCollectionChanged);
            PolylineOriginPoint = new PolylineOriginPoint();
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Items[e.NewStartingIndex].PropertyChanged += ProfilePropertyChanged;
                    if (Items[e.NewStartingIndex].MaxAltitude > MaxAltitude)
                        SetMaxAltitude(Items[e.NewStartingIndex].MaxAltitude);
                    if (Items[e.NewStartingIndex].MinAltitude < MinAltitude)
                        SetMinAltitude(Items[e.NewStartingIndex].MinAltitude);
                    break;
            }
        }

        private void ProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            int p;
            switch (e.PropertyName)
            {
                case "MaxAltitude":
                    if (((ProfileViewModel)sender).MaxAltitude > _maxAltitude)
                        SetMaxAltitude(((ProfileViewModel)sender).MaxAltitude);
                    else
                        UpdateMaxMinAltitude();
                    break;
                case "MinAltitude":
                    if (((ProfileViewModel)sender).MinAltitude > _minAltitude)
                        SetMinAltitude(((ProfileViewModel)sender).MinAltitude);
                    else
                        UpdateMaxMinAltitude();
                    break;
             }
        }

        /// <summary>
        /// 调用Items各项的UpdateAltitude()，同时更新MaxAltitutde和MinAltitude。
        /// </summary>
        public void UpdateMaxMinAltitude()
        {
            double max, min;
            if (Items.Count > 0)
            {
                max = Items[0].MaxAltitude;
                min = Items[0].MinAltitude;
                for (int i = 0; i < Items.Count; i++)
                {
                    //Items[i].UpdateMaxMinAltitude();
                    if (max < Items[i]._maxAltitude)
                        max = Items[i]._maxAltitude;
                    if (min > Items[i]._minAltitude)
                        min = Items[i]._minAltitude;
                }
                if (_maxAltitude != max)
                    SetMaxAltitude(max);
                if (_minAltitude != min)
                    SetMinAltitude(min);
            }
        }

        private void SetMaxAltitude(double value)
        {
            _maxAltitude = value;
            OnPropertyChanged("MaxAltitude");
        }

        private void SetMinAltitude(double value)
        {
            _minAltitude = value;
            OnPropertyChanged("MinAltitude");
        }

        /// <summary>
        /// 按照height和width，将纵断面全尺寸显示。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public void SetPolylineFullSize(double height, double width)
        {
            UpdateMaxMinAltitude();
            Scale.SetScale(height, width, MaxAltitude, MinAltitude, Length);
            SetPolylineOriginPoint(height, width);
        }

        public void SetPolylineOriginPoint(double height, double width)
        {
            PolylineOriginPoint.SetX(_horizontalAlignment, width, Length);
            PolylineOriginPoint.SetY(_verticalAlignment, height, _maxAltitude, _minAltitude);
        }
    }
}
