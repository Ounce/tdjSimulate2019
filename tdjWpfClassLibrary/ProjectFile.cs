using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    //[Serializable]
    [XmlRoot("Project")]
    public class ProjectFile 
    {
        /// <summary>
        /// 文件格式的版本号。
        /// </summary>
        [XmlAttribute("Version")]
        public string Version { get; set; }

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

        public ProjectFile()
        {
            Version = "0.1";
            _cuts = new CutList();
        }
    }
}
