using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace tdjClassLibrary.Profile
{
    /// <summary>
    /// 纵断面比例图。高程比例与里程比例不同。此图形是将Profile绘制在Canvas上。
    /// </summary>
    public class ProfilePolylineDrawing : BaseProfileDrawing
    {
        /// <summary>
        /// 图形顶端对应的高程。
        /// </summary>
        public double TopAltitude { get; set; }

        /// <summary>
        /// 图形底部对应的高程。
        /// </summary>
        public double BottomAltitude { get; set; }

        /// <summary>
        /// 这个折线按照水平和垂直比例计算纵断面在Canvas中的位置。并将其添加到Canvas中。
        /// </summary>
        private Polyline Polyline { get; set; }

        /// <summary>
        /// 构造函数进行一部分初始化工作，将Polyline添加到Canvas中。并将Profile处理函数进行绑定。
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="profile"></param>
        public ProfilePolylineDrawing(ref Canvas canvas, ref ProfileViewModel profile)
        {
            Canvas = canvas;
            Profile = profile;
            Polyline = new Polyline();
            Canvas.Children.Add(Polyline);
            if (Profile.Count > 0)
            Polyline.Points.Add(Profile.Slopes[0].BeginPolylinePoint);
            foreach (var s in Profile.Slopes)
            {
                Polyline.Points.Add(s.EndPolylinePoint);
            }
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
                    if (e.NewStartingIndex == 0)
                        Polyline.Points.Add(Profile.Slopes[0].BeginPolylinePoint);
                    Polyline.Points.Insert(e.NewStartingIndex + 1, Profile.Slopes[e.NewStartingIndex].EndPolylinePoint);
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
            for (int i = 0; i < Profile.Slopes.Count; i++)
            {
                if (Profile.Slopes[i].Equals(sender))
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

        private void ProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HorizontalScale":

                case "VerticalScale":

                    break;
            }
        }


        }

    }
}
