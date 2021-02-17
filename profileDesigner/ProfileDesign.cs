using Accessibility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using tdjWpfClassLibrary;
using tdjWpfClassLibrary.Profile;

namespace ProfileDesigner
{
    /// <summary>
    /// 这个类以Polyline形式显示Profile。图中有设计和既有Profile。
    /// </summary>
    class ProfileDesign :  NotifyPropertyChanged
    {
        public Canvas Canvas
        {
            set
            {
                if (value != _canvas)
                {
                    _canvas = value;
                }
            }
        }
        private Canvas _canvas;

        public double HotizontialScale 
        {  
            set
            {
                if (value != _hScale)
                {
                    _hScale = value;
                    /*
                    DesignPolylineDrawing.HorizontalScale = _hScale;
                    ExistPolylineDrawing.HorizontalScale = _hScale;
                    */
                }
            }
        }
        private double _hScale;

        public double VerticalScale
        {
            set
            {
                if (value != _vScale)
                {
                    _vScale = value;
                    /*
                    DesignPolylineDrawing.VerticalScale = _vScale;
                    ExistPolylineDrawing.VerticalScale = _vScale;
                    */
                }
            }
        }
        private double _vScale;

        public double VerticalHorizontalScale
        {
            get { return _vhScale; }
            set
            {
                if (value != _vhScale)
                {
                    _vhScale = value;
                    OnPropertyChanged("VerticalHorizontalScale");
                }
            }
        }
        private double _vhScale;

        public double MaxAltitude;

        public double MinAltitude;

        public double Length;



        public ProfileDesign()
        {

        }
        /// <summary>
        /// 设置在Canvas全部显示Profile时的比例。
        /// </summary>
        public void SetScale()
        {
            SetMaxMinAltitude();
            SetLength();
            double v = (MaxAltitude - MinAltitude) / _canvas.ActualHeight;
            double h = Length / _canvas.ActualWidth;
            if (h * VerticalHorizontalScale < v)
            {
                HotizontialScale = h;
                VerticalScale = h * VerticalHorizontalScale;
            }
            else
            {
                VerticalScale = v;
                HotizontialScale = v / VerticalHorizontalScale;
            }
        }

        private void SetLength()
        {/*
            if (DesignPolylineDrawing == null)
            {
                if (ExistPolylineDrawing == null)
                    return;
                else
                    Length = ExistPolylineDrawing.Profile.Length;
            }
            else
            {
                if (ExistPolylineDrawing == null)
                    Length = DesignPolylineDrawing.Profile.Length;
                else
                    Length = DesignPolylineDrawing.Profile.Length > ExistPolylineDrawing.Profile.Length ? DesignPolylineDrawing.Profile.Length : ExistPolylineDrawing.Profile.Length;
            }*/
        }

        private void SetMaxMinAltitude()
        {/*
            if (DesignPolylineDrawing == null)
            {
                if (ExistPolylineDrawing == null)
                    return;
                else
                {
                    MaxAltitude = ExistPolylineDrawing.Profile.MaxAltitude;
                    MinAltitude = ExistPolylineDrawing.Profile.MinAltitude;
                }
            }
            else
            {
                if (ExistPolylineDrawing == null)
                {
                    MaxAltitude = DesignPolylineDrawing.Profile.MaxAltitude;
                    MinAltitude = DesignPolylineDrawing.Profile.MinAltitude;
                }
                else
                {
                    MaxAltitude = DesignPolylineDrawing.Profile.MaxAltitude > ExistPolylineDrawing.Profile.MaxAltitude ? DesignPolylineDrawing.Profile.MaxAltitude : ExistPolylineDrawing.Profile.MaxAltitude;
                    MinAltitude = DesignPolylineDrawing.Profile.MinAltitude < ExistPolylineDrawing.Profile.MinAltitude ? DesignPolylineDrawing.Profile.MinAltitude : ExistPolylineDrawing.Profile.MinAltitude;
                }
            }*/
        }
    }
}
