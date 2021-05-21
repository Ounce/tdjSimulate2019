using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Layout;
using tdjWpfClassLibrary.Profile;
using tdjWpfClassLibrary.Retarder;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary.Project
{
    public class Project : NotifyPropertyChanged
    {
        [XmlAttribute("Name")]
        public string Name 
        { 
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    _node.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlElement("Checks")]
        public CheckCollection Checks 
        { 
            get => _checks;
            set
            {
                if (value != _checks)
                {
                    _checks = value;
                    OnPropertyChanged("Nodes");
                    OnPropertyChanged("Checks");
                }
            }
        }
        private CheckCollection _checks;

        [XmlAttribute("Cuts")]
        public CutList Cuts
        {
            get { return _cuts; }
            set
            {
                if (value != _cuts)
                {
                    _cuts = value;
                }
            }
        }
        private CutList _cuts;

        public Collection<Track> Tracks
        {
            get => _tracks;
            set => _tracks = value;
        }
        private Collection<Track> _tracks;

        public Collection<Retarder.Retarder> Retarders
        {
            get => _retarders;
            set => _retarders = value;
        }
        private Collection<Retarder.Retarder> _retarders;

        public Collection<ProfileViewModel> Profiles { get; set; }

        public TreeViewNode Node 
        {
            get => _node;
        }
        private TreeViewNode _node;

        public Project()
        {
            _node = new TreeViewNode();
            _name = "未命名";
            _node.Name = _name;
            _node.PageType = PageType.Project;
            _checks = new CheckCollection();
            Check check = new Check();
            _checks.Add(check);
            _node.Children = Checks.Nodes;
            _cuts = new CutList();
            Checks.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ChecksCollectionChanged);
        }

        private void ChecksCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _node.Children = Checks.Nodes;
        }

        private TreeViewNode GetTreeViewNode()
        {
            TreeViewNode tv = new TreeViewNode();
            tv.Name = Name;
            tv.PageType = PageType.Project;
            tv.Children = Checks.Nodes;
            return tv;
        }
    }

    //[Serializable]
    [XmlRoot("Project")]
    public class ProjectFile : Project
    {
        /// <summary>
        /// 文件格式的版本号。
        /// </summary>
        [XmlAttribute("Version")]
        public string Version { get; }

        [XmlIgnore]
        public string Path;

        public ProjectFile()
        {
            Version = "0.1";
        }

        /*
        public Project ReadXML()
        {
            Project = (Project)XmlHelper.ReadXML(Path, typeof(ProjectFile));
            return Project;
        }
        */
    }

    public class ProjectCollection : Collection<Project>
    {

    }
}
