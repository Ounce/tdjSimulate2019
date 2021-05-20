using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Project;
using tdjWpfClassLibrary.Retarder;

namespace tdjWpfClassLibrary.Layout
{
    public class Track : TrackBase
    {
        public Guid ID { get; set; }    
        public string Name { get; set; }    

        [XmlIgnore]
        public ProfileViewModel Profile { get; set; }
        [XmlIgnore]
        public CurveList Curves { get; set; }
        [XmlIgnore]
        public SwitchList Switches { get; set; }
        [XmlIgnore]
        public Collection<Retarder.Retarder> Retarders { get; set; }

        [XmlIgnore]
        public ObservableCollection<TreeViewNode> Nodes
        {
            get { return GetTreeViewNodes(); }
        }
        
        private ObservableCollection<TreeViewNode> GetTreeViewNodes()
        {
            ObservableCollection<TreeViewNode> nodes = new ObservableCollection<TreeViewNode>()
            {
                new TreeViewNode() {Name = "纵断面", Children = null },
                new TreeViewNode() {Name = "道岔", Children = null}
            };
            return nodes;
        }
    }

    public class TrackBase
    {
        public Guid ProfileID { get; set; }
        public ObservableCollection<Guid> CurveIDs { get; set; }
        public ObservableCollection<Guid> SwitchIDs { get; set; }
        public ObservableCollection<Guid> RetarderIDs { get; set; }
    }

    public class TrackCollection : Collection<Track>
    {

    }
}
