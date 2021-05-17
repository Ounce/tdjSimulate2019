using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary.Project
{
    public class Check
    {
        public ObservableCollection<Guid> CutIDs { get; set; }
        public ObservableCollection<Guid> TrackIDs { get; set; }

        [XmlIgnore]
        public CutList Cuts { get; set; }
    }

    public class CheckList : ObservableCollection<Check>
    {

    }
}
