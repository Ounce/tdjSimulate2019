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
        public string Name { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlElement("Checks")]
        public CheckCollection Checks { get; set; }

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

        public TreeViewNode Node { get { return GetTreeViewNode(); } }

        public Project()
        {
            Checks = new CheckCollection();
            Check check = new Check();
            check.Name = "ceshi";
            Checks.Add(check);
            _cuts = new CutList();
        }

        private TreeViewNode GetTreeViewNode()
        {
            TreeViewNode tv = new TreeViewNode();
            tv.Name = Name;
            tv.PageType = 1;
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
