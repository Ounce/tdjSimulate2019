using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Retarder;

namespace tdjWpfClassLibrary.Layout
{
    public class Track : TrackBase
    {
        public ProfileViewModel Profile { get; set; }
        public CurveList Curves { get; set; }
        public SwitchList Switches { get; set; }
        public Collection<Retarder.Retarder> Retarders { get; set; }
    }

    public class TrackBase
    {
        public Guid ProfileID { get; set; }
        public ObservableCollection<Guid> CurveIDs { get; set; }
        public ObservableCollection<Guid> SwitchIDs { get; set; }
        public ObservableCollection<Guid> RetarderIDs { get; set; }
    }

    public class TrackList : Collection<Track>
    {

    }
}
