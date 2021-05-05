using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace tdjWpfClassLibrary.Retarder
{
    public class CtrRetarder : CtrRetarderModel
    {
        public Guid ModelID { get; set; }

        public Position Position { get; set; }

        public void Copy(CtrRetarderModel model)
        {
            ModelID = model.ID;

        }
    }

    public class CtrRetarderModel : RetarderModel
    {
        public bool CtrState { get; set; }
    }


    /// <summary>
    /// 可控顶列表。
    /// </summary>
    [XmlRoot("CtrRetarders")]
    public class CtrRetarderModelList : Collection<CtrRetarderModel>
    {
        public CtrRetarderModelList() { }
    }
}
