using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    class ProfileViewModelCollection : NotifyPropertyChanged
    {
        public ObservableCollection<ProfileViewModel> Items;

        public Scale Scale;

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

        public PolylineOriginPoint OriginPoint;

        public double canvasWidth, canvasHeight;

        public HorizontalAlignment HorizontalAlignment
        {
            set
            {
                if (value != _horizontalAlignment)
                {
                    _horizontalAlignment = value;
                    OriginPoint.SetX(_horizontalAlignment, canvasWidth, Length, Scale);
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
                    OriginPoint.SetY(_verticalAlignment, canvasHeight, _maxAltitude, _minAltitude, Scale);
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        public ProfileViewModelCollection()
        {
            Items = new ObservableCollection<ProfileViewModel>();
            Scale = new Scale();
            OriginPoint = new PolylineOriginPoint();
        }

        /// <summary>
        /// 调用Items各项的UpdateAltitude()，同时更新MaxAltitutde和MinAltitude。
        /// </summary>
        public void UpdateMaxMinAltitude()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].UpdateMaxMinAltitude();
                if (i == 0)
                {
                    _maxAltitude = Items[0]._maxAltitude;
                    _minAltitude = Items[0]._minAltitude;
                }
                else
                {
                    if (_maxAltitude < Items[i]._maxAltitude) _maxAltitude = Items[i]._maxAltitude;
                    if (_minAltitude > Items[i]._minAltitude) _minAltitude = Items[i]._minAltitude;
                }
            }
        }

        public void SetPolylineFullSize(double height, double width)
        {
            UpdateMaxMinAltitude();
            Scale.SetScale(height, width, MaxAltitude, MinAltitude, Length);
            foreach (var i in Items)
            {
                i.Scale = Scale;
                i.UpdatePoints();
            }
            OriginPoint.SetX(_horizontalAlignment, canvasWidth, Length, Scale);
            OriginPoint.SetY(_verticalAlignment, canvasHeight, _maxAltitude, _minAltitude, Scale);
        }
    }
}
