using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjClassLibrary.Profile
{
    class ProfileViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<Slope> Slopes;
    }
}
