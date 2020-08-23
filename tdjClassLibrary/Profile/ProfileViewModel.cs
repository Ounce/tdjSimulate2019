using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Shapes;



namespace tdjClassLibrary.Profile
{
    /// <summary>
    /// Profile的视图模型类，除Profile的基本参数属性外，还定义了显示在Polyline等控件上需要的参数。
    /// </summary>
    public class ProfileViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<SlopeViewModel> Slopes;

        /// <summary>
        /// 坡度单位。‰或%，‰对应GradeUnit：1000；%对应GradeUnit为100；
        /// </summary>
        public double GradeUnit { get; set; }

        public int Count
        {
            get { return Slopes.Count; }
        }

        /// <summary>
        /// 原始Altitude位置。未设置时此值为-1。
        /// 读取Excel数据时，以第一个非-999高程为FixAltitudePosition。
        /// </summary>
        public int FixAltitudePosition { get; set; }

        /// <summary>
        /// 原始Altitude。如果原始值为BeginAltitude，则此值为true；如果为EndAltitude时为false。
        /// </summary>
        public bool FixBeginOrEndAltitude { get; set; }

        /// <summary>
        /// 最大高程。
        /// </summary>
        public double MaxAltitude { get; set; }

        /// <summary>
        /// 最小高程。
        /// </summary>
        public double MinAltitude { get; set; }

        /// <summary>
        /// 纵断面全长。通过循环计算。
        /// </summary>
        public double Length
        {
            get
            {
                double l = 0;
                foreach (var s in Slopes)
                {
                    l += s.Length;
                }
                return l;
            }
        }

        public ProfileViewModel()
        {
            GradeUnit = 1000;
            Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
        }

        /// <summary>
        /// Slopes改变时，处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlopesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Slopes[e.NewStartingIndex].PropertyChanged += SlopePropertyChanged;

                    break;
            }
        }

        /// <summary>
        /// Slope属性改变处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlopePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 获得在Slopes中的位置。
            int position = -1;
            for (int i = 0; i < Slopes.Count; i++)
            {
                if (Slopes[i].Equals(sender))
                {
                    position = i;
                    break;
                }
            }
            if (position == -1) return;
            switch (e.PropertyName)
            {
                case "EndAltitude":
                    break;
            }
        }

        /// <summary>
        /// 计算最大和最小高程。
        /// </summary>
        /// <returns></returns>
        public void SetMaxMinAltitude()
        {
            double min, max;
            min = max = Slopes[0].BeginAltitude;
            foreach (SlopeViewModel s in Slopes)
            {
                if (s.EndAltitude > max)
                    max = s.EndAltitude;
                if (s.EndAltitude < min)
                    min = s.EndAltitude;
            }
            MaxAltitude = max;
            MinAltitude = min;
            return;
        }
    }


}
