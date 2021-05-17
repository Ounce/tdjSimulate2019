using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Retarder;

namespace tdjWpfClassLibrary.Layout
{
    public class Track : TrackBase
    {
        [XmlIgnore]
        public ProfileViewModel Profile { get; set; }
        [XmlIgnore]
        public CurveList Curves { get; set; }
        [XmlIgnore]
        public SwitchList Switches { get; set; }
        [XmlIgnore]
        public Collection<Retarder.Retarder> Retarders { get; set; }
    }

    public class TrackBase
    {
        public Guid ProfileID { get; set; }
        public ObservableCollection<Guid> CurveIDs { get; set; }
        public ObservableCollection<Guid> SwitchIDs { get; set; }
        public ObservableCollection<Guid> RetarderIDs { get; set; }
    }

    [XmlRoot("Tracks")]
    public class TrackList : Collection<Track>
    {

    }
}
