using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    //[Serializable]
    //[XmlRoot("Project")]
    public class ProjectFile 
    {
        [XmlAttribute("Version")]
        /// <summary>
        /// 文件格式的版本号。
        /// </summary>
        public string Version { get; set; }
        public CutList Cuts { get; set; }

        public static void WriteXML()
        {

        }
    }
}
