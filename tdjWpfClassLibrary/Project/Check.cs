using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Wagon;
using tdjWpfClassLibrary.Layout;
using System.Windows.Controls;

namespace tdjWpfClassLibrary.Project
{
    public class Check : NotifyPropertyChanged
    {
        public Guid ID { get; set; }
        public string Name 
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    Node.Name = _name;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;

        public ObservableCollection<Guid> CutIDs { get; set; }

        public ObservableCollection<Guid> TrackIDs { get; set; }

        [XmlIgnore]
        public CutList Cuts { get; set; }

        [XmlIgnore]
        public TrackCollection Tracks { get; set; }

        /// <summary>
        /// 绑定Tracks，显示TreeView节点。
        /// </summary>
        [XmlIgnore]
        public TreeViewNode Node { get; set; }

        public Check()
        {
            Node = new TreeViewNode();
            Name = "未命名";
            Node.PageType = PageType.Check;
            Node.DataID = ID;
            TreeViewNode TrackNode = new TreeViewNode();
            TrackNode.Name = "线路";
            TrackNode.PageType = PageType.Tracks;
            Tracks = new TrackCollection();
            Track track = new Track();
            Tracks.Add(track);
            TrackNode.Children = Tracks.Nodes;
            Node.Children.Add(TrackNode);
            TreeViewNode sNode = new TreeViewNode();
            sNode.PageType = PageType.Cut;
            sNode.Name = "车组";
            sNode.Children = null;
            Node.Children.Add(sNode);
            TreeViewNode rNode = new TreeViewNode();
            rNode.PageType = PageType.Resistance;
            rNode.Name = "阻力";
            rNode.Children = null;
            Node.Children.Add(rNode);
        }
    }

}
