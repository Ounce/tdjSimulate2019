using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Layout;
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
        public static CtrRetarderModelList CtrRetarders { get => Data.CtrRetarders; set => Data.CtrRetarders = value; }
        public static ArresterModelList Arresters { get => Data.Arresters; set => Data.Arresters = value; }
        public static SwitchModelList Switches { get => Data.Switches; set => Data.Switches = value; }

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
        public CtrRetarderModelList CtrRetarders;
        public ArresterModelList Arresters;
        public SwitchModelList Switches;
    }
}
