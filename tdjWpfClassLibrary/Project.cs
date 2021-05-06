﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Retarder;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    public class Project
    {

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

        public RetarderList Retarders
        {
            get => _retarders;
            set => _retarders = value;
        }
        private RetarderList _retarders;

        public Project()
        {
            _cuts = new CutList();
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


    }
}
