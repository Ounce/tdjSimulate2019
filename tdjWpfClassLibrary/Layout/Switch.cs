using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjWpfClassLibrary.Layout
{
    public class Switch : SwitchModel
    {
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;

        public double Position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
        private double _position;
    }

    public class SwitchList : ObservableCollection<Switch> { }

    public class SwitchModel : NotifyPropertyChanged
    {
        public Guid ID { get; set; }
        public string Model { get; set; }
    }

    public class SwitchModelList : Collection<SwitchModel> { }
}
