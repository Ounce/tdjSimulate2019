using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace tdjClassLibrary.Profile
{
    /// <summary>
    /// 纵断面比例图。高程比例与里程比例不同。此图形是将Profile绘制在Canvas上。
    /// </summary>
    public class ProfileScaleDrawing : BaseProfileDrawing
    {
        /// <summary>
        /// 图形顶端对应的高程。
        /// </summary>
        public double TopAltitude { get; set; }

        /// <summary>
        /// 图形底部对应的高程。
        /// </summary>
        public double BottomAltitude { get; set; }

        public ProfileScaleDrawing(Canvas canvas)
        {
            Canvas = canvas;
            Profile.Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
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
                    Profile.Slopes[e.NewStartingIndex].PropertyChanged += SlopePropertyChanged;
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
            switch (e.PropertyName)
            {
                case "EndAltitude":
                    break;
            }
        }
    }
}
