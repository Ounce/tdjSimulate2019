﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    /// <summary>
    /// 模型列表类模板。要求模型类必须有Guid类型的ID属性（或字段）。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Collection<T> : ObservableCollection<T>
    {
        public T Find(Guid id)
        {
            Type type = typeof(T);
            PropertyInfo proInfo = type.GetProperty("ID");
            if (proInfo == null) return default(T);
            foreach (T o in this)
            {
                Guid i = (Guid)proInfo.GetValue(o);
                if (i == id)
                    return o;
            }
            return default(T);
        }

        public Collection<T> Find(ObservableCollection<Guid> ids)
        {
            Type type = typeof(T);
            PropertyInfo proInfo = type.GetProperty("ID");
            if (proInfo == null) return null;
            Collection<T> re = new Collection<T>();
            foreach (T o in this)
            {
                Guid i = (Guid)proInfo.GetValue(o);
                if (ids.Contains(i))
                {
                    re.Add(o);
                }
            }
            return re;
        }
    }


}
