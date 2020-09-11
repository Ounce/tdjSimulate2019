using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace tdjWpfClassLibrary.Profile
{
    /// <summary>
    /// 纵断面的Polyline图，可由此派生出实际使用的类。此类中的与Profile相关的函数都是带Profile参数的。
    /// </summary>
    class ProfilePolylineDrawing
    {
        /// <summary>
        /// 垂直对齐。默认为中心对齐。
        /// </summary>
        public VerticalAlignment VerticalAlignment = VerticalAlignment.Center;

        public double MaxAltitude, MinAltitude;

        /// <summary>
        /// 将profile的MaxAltitude、MinAltitude赋值给MaxAltitude和MinAltitude。
        /// </summary>
        /// <param name="profile"></param>
        public void SetMaxMinAltitude(ProfileViewModel profile)
        {
            MaxAltitude = profile.MaxAltitude;
            MinAltitude = profile.MinAltitude;
        }

        /// <summary>
        /// 将MaxAltitude和MinAltitude与profile的进行比较，后更新。
        /// </summary>
        /// <param name="profile"></param>
        public void UpdateMaxMinAltitude(ProfileViewModel profile)
        {
            if (profile.MaxAltitude > MaxAltitude) MaxAltitude = profile.MaxAltitude;
            if (profile.MinAltitude < MinAltitude) MinAltitude = profile.MinAltitude;
        }

        /// <summary>
        /// 图形顶端对应的Altitude。
        /// </summary>
        public double TopAltitude { get; set; }

        /// <summary>
        /// 纵断面显示比例。
        /// </summary>
        public Scale Scale { get; set; }

        /// <summary>
        /// 原始Profile，派生类可定义其他Profile。
        /// </summary>
        public ProfileViewModel Profile { get; set; }

        public ProfilePolylineDrawing()
        {
            Profile = new ProfileViewModel();
            Scale = new Scale();
        }

        public void SetScale(double height, double width)
        {
            Scale.SetScale(height, width, MaxAltitude, MinAltitude, Profile.Length);
        }

        /// <summary>
        /// 设置图形顶部对应的高程。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="maxAltitude">纵断面的最大高程</param>
        /// <param name="minAltitude">纵断面最小高程</param>
        public void SetTopAltitude(double height, double maxAltitude, double minAltitude)
        {
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    TopAltitude = maxAltitude;
                    break;
                case VerticalAlignment.Center:
                    TopAltitude = maxAltitude + (height / Scale.Vertical - (maxAltitude - minAltitude) ) * 0.5;
                    break;
                case VerticalAlignment.Bottom:
                    TopAltitude = maxAltitude + (height / Scale.Vertical - (maxAltitude - minAltitude));
                    break;
            }
        }

    }
}
