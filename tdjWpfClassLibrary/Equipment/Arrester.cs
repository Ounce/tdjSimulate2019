using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Equipment
{
    public class Arrester : ArresterModel
    {
    }

    public class ArresterModel : RetarderModel
    {

    }

    public class ArresterModelList : Collection<ArresterModel>
    {
        public ArresterModelList() { }
    }
}
