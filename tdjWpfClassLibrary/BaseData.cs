using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Retarder;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    public static class BaseData 
    {
        public static string Path = "..//..//..//Files//BaseData.xml";
        private static _baseData Data = (_baseData)XmlHelper.ReadXML(Path, typeof(_baseData));
        
        public static WagonModelList Wagons { get => Data.Wagons; set => Data.Wagons = value; }
        public static RetarderModelList Retarders { get => Data.Retarders; set => Data.Retarders = value; }

        public static void WriteXml()
        {
            XmlHelper.WriteXML(Path, Data);
        }
    }

    [XmlRoot("BaseData")]
    public class _baseData
    {
        public WagonModelList Wagons;
        public RetarderModelList Retarders;
    }
}
