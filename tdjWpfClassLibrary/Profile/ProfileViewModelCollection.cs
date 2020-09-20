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

        public Point OriginPoint = new Point(0, 0);

        public HorizontalAlignment HorizontalAlignment
        {
            set
            {
                if (value != _horizontalAlignment)
                {
                    _horizontalAlignment = value;
                    SetOriginPointX();
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
                    SetOriginPointY();
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        public ProfileViewModelCollection()
        {
            Items = new ObservableCollection<ProfileViewModel>();
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
            SetOriginPointX(width);
            SetOriginPointY(height);
        }

        /// <summary>
        /// 设置图形左上方原点 X 坐标。
        /// </summary>
        private void SetOriginPointX(double width)
        {
            switch (_horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    OriginPoint.X = 0;
                    break;
                case HorizontalAlignment.Center:
                    OriginPoint.X = (width - Length * Scale.Horizontal) * 0.5;
                    break;
                case HorizontalAlignment.Right:
                    OriginPoint.X = width - Length * Scale.Horizontal;
                    break;
            }
        }

        /// <summary>
        /// 设置图形左上方原点 Y 坐标。
        /// </summary>
        private void SetOriginPointY(double height)
        {
            switch (_verticalAlignment)
            {
                case VerticalAlignment.Top:
                    OriginPoint.Y = Items[0].GetPointY(MaxAltitude);
                    break;
                case VerticalAlignment.Center:
                    //（- TopAltitude + MaxAltitude）* Scale.Vertical = (canvasHeight + (MaxAltitude - MinAltitude) * Scale.Vertical) * 0.5
                    //- TopAltitude * Scale.Vertical + MaxAltitude * Scale.Vertical = 0.5 * canvasHeight + 0.5 * (MaxAltitude - MinAltitude) * Scale.Vertical
                    //- OriginPoint.Y = 0.5 * canvasHeight + 0.5 * MaxAltitude * Scale.Verrtical + 0.5 * MinAltitude * Scale.Vertical
                    OriginPoint.Y = 0.5 * (GetPointY(MaxAltitude) + GetPointY(MinAltitude) - height);
                    break;
                case VerticalAlignment.Bottom:
                    OriginPoint.Y = GetPointY(MinAltitude) - height;
                    break;
            }
        }
    }
}
