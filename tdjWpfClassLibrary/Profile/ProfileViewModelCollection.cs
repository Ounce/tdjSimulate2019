using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    public class ProfileViewModelCollection : NotifyPropertyChanged, IGraphPosition
    {
        private ObservableCollection<ProfileViewModel> Items;

        public ProfileViewModel this[int index] 
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        public bool Updated
        {
            get { return _updated; }
            set
            {
                _updated = true;
                OnPropertyChanged("Updated");
            }
        }
        private bool _updated = true;

        public int Count
        {
            get { return Items.Count; }
        }

        public void Clear()
        {
            Items.Clear();
        }

        public void Remove(ProfileViewModel item)
        {
            Items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        /// <summary>
        /// 最大高程。
        /// </summary>
        public double MaxAltitude
        {
            get 
            {
                if (Items.Count < 1) return 0;
                _maxAltitude = Items[0].MaxAltitude;
                for (int i = 1; i < Items.Count; i++)
                    if (_maxAltitude < Items[i].MaxAltitude)
                        _maxAltitude = Items[i].MaxAltitude;
                return _maxAltitude; 
            }
        }
        private double _maxAltitude;

        /// <summary>
        /// 最小高程。
        /// </summary>
        public double MinAltitude
        {
            get
            {
                if (Items.Count < 1) return 0;
                _minAltitude = Items[0].MinAltitude;
                for (int i = 1; i < Items.Count; i++)
                    if (_minAltitude > Items[i].MinAltitude)
                        _minAltitude = Items[i].MinAltitude;
                return _minAltitude;
            }
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

        public Point LeftTop { get; set; }

        /// <summary>
        /// 用于计算图型显示位置，图形显示在顶端、中心、底部、左边或右边时，需要此参数在计算偏移量。
        /// </summary>
        public double CanvasActualWidth { get; set; }
        public double CanvasActualHeight { get; set; }

        public HorizontalAlignment HorizontalAlignment
        {
            get => _horizontalAlignment;
            set
            {
                if (value != _horizontalAlignment)
                {
                    _horizontalAlignment = value;
                    OnPropertyChanged("HorizontalAlignment");
                }
            }
        }
        private HorizontalAlignment _horizontalAlignment;

        public VerticalAlignment VerticalAlignment
        {
            get => _verticalAlignment;
            set
            {
                if (value != _verticalAlignment)
                {
                    _verticalAlignment = value;
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        public ProfileViewModelCollection()
        {
            _maxAltitude = StaticClass.Altitude.InitMax;
            _minAltitude = StaticClass.Altitude.InitMin;
            _verticalAlignment = VerticalAlignment.Center;
            Items = new ObservableCollection<ProfileViewModel>();
            Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ItemsCollectionChanged);
            LeftTop = new Point();
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
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    UpdateMaxMinAltitude();
                    break;
            }
        }

        private void ProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Updated":
                    OnPropertyChanged("Updated");
                    break;
            }
        }

        public void Add(ProfileViewModel profile)
        {
            Items.Add(profile);
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
            SetLeftTop();
        }

        public void SetLeftTop()
        {
            IGraphPosition gp = new ProfileViewModel();
            LeftTop = gp.SetLeftTop(HorizontalAlignment, VerticalAlignment, CanvasActualWidth, CanvasActualHeight, 0, Length * Scale.Horizontal, MaxAltitude * Scale.Vertical, MinAltitude * Scale.Vertical);
        }

        public void UpdateScale()
        {
            foreach (var i in Items)
            {
                i.UpdateScale();
            }
        }
    }
}
