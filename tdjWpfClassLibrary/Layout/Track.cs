using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Project;
using tdjWpfClassLibrary.Equipment;

namespace tdjWpfClassLibrary.Layout
{
    public class Track : TrackBase
    {
        public Guid ID 
        { 
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    _node.TrackID = value;
                }
            }
        }
        private Guid _id;

        public string Name { get; set; }    

        [XmlIgnore]
        public ProfileViewModel Profile { get; set; }
        [XmlIgnore]
        public CurveList Curves { get; set; }
        [XmlIgnore]
        public SwitchList Switches { get; set; }
        [XmlIgnore]
        public Collection<Retarder> Retarders { get; set; }

        [XmlIgnore]
        public TreeViewNode Node
        {
            get => _node;
            set => _node = value;
        }
        private TreeViewNode _node;

        public Track()
        {
            _node = new TreeViewNode();
            _node.PageType = PageType.Track;
            _node.Children = GetTreeViewNodes();
        }
        
        private ObservableCollection<TreeViewNode> GetTreeViewNodes()
        {
            ObservableCollection<TreeViewNode> nodes = new ObservableCollection<TreeViewNode>()
            {
                new TreeViewNode() {Name = "纵断面", PageType = PageType.Profile, Children = null },
                new TreeViewNode() {Name = "道岔", PageType = PageType.Switch, Children = null}
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
