using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace tdjWpfClassLibrary
{
    public class CanvasLabel : NotifyPropertyChanged
    {
        private string _text;
        /// <summary>
        /// Content of Label.
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        private VerticalAlignment _verticalBaseAlignment = VerticalAlignment.Bottom;
        private HorizontalAlignment _horizontalBaseAlignment = HorizontalAlignment.Left;
        private double _left, _right, _top, _bottom;
        public double Left
        {
            get { return _left; }
            set
            {
                if (value != _left)
                {
                    _left = value;
                    OnPropertyChanged("Left");
                    _horizontalBaseAlignment = HorizontalAlignment.Left;
                }
            }
        }
        public double CenterHorizontal
        {
            set
            {
                if (value != _left + 0.5 * _width)
                {
                    Left = value - 0.5 * _width;
                    _horizontalBaseAlignment = HorizontalAlignment.Center;
                }
            }
        }
        public double Right
        {
            set
            {
                if (value != _left + _width)
                {
                    Left = value - _width;
                    _horizontalBaseAlignment = HorizontalAlignment.Right;
                }
            }
        }
        public double Top
        {
            get { return _top; }
            set
            {
                if (value != _top)
                {
                    _top = value;
                    OnPropertyChanged("Top");
                    _verticalBaseAlignment = VerticalAlignment.Top;
                }
            }
        }
        public double CenterVertical
        {
            set
            {
                if (value != _top + 0.5 * _height)
                {
                    Top = value - 0.5 * _height;
                    _verticalBaseAlignment = VerticalAlignment.Center;
                }
            }
        }
        public double Bottom
        {
            set
            {
                if (value != _top + _height)
                {
                    Top = value - _height;
                    _verticalBaseAlignment = VerticalAlignment.Bottom;
                }
            }
        }

        private double _height, _width;
        public double Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    switch (_verticalBaseAlignment)
                    {
                        case VerticalAlignment.Top:
                            break;
                        case VerticalAlignment.Bottom:
                            Top = _top - (value - _height);
                            break;
                        case VerticalAlignment.Center:
                            Top = _top - 0.5 * (value - _height);
                            break;
                    }
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }
        public double Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    switch (_horizontalBaseAlignment)
                    {
                        case HorizontalAlignment.Left:
                            break;
                        case HorizontalAlignment.Center:
                            Left = _left - 0.5 * (value - _width);
                            break;
                        case HorizontalAlignment.Right:
                            Left = _left - (value - _width);
                            break;
                    }
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        private HorizontalAlignment _horizontalContentAlignment;
        private VerticalAlignment _verticalContentAlignment;
        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return _horizontalContentAlignment; }
            set
            {
                if (value != _horizontalContentAlignment)
                {
                    _horizontalContentAlignment = value;
                    OnPropertyChanged("HorizontalContentAlignment");
                }
            }
        }
        public VerticalAlignment VerticalContentAlignment
        {
            get { return _verticalContentAlignment; }
            set
            {
                if (value != _verticalContentAlignment)
                {
                    _verticalContentAlignment = value;
                    OnPropertyChanged("VerticalContentAlignment");
                }
            }
        }
    }
}
