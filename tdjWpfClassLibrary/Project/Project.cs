using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Layout;
using tdjWpfClassLibrary.Retarder;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary.Project
{
    public class Project
    {
        public CheckList Checks { get; set; }
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

        public TrackList Tracks
        {
            get => _tracks;
            set => _tracks = value;
        }
        private TrackList _tracks;

        public Collection<Retarder.Retarder> Retarders
        {
            get => _retarders;
            set => _retarders = value;
        }
        private Collection<Retarder.Retarder> _retarders;

        public Project()
        {
            _cuts = new CutList();
        }

        public void UpdateDataByIDs()
        {
            foreach (Track t in Tracks)
            {
                t.Retarders = Retarders.Find(t.RetarderIDs);
            }
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
}
