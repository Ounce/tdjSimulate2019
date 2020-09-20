using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }
}
