using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Project;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    /// <summary>
    /// 模型列表类模板。要求模型类必须有Guid类型的ID属性（或字段）。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Collection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// 用于TreeView控件的显示。
        /// </summary>
        public ObservableCollection<TreeViewNode> Nodes { get { return GetTreeViewNodes(); } }

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

        private ObservableCollection<TreeViewNode> GetTreeViewNodes()
        {
            if (this.Count < 1) return null;
            Type type = typeof(T);
            PropertyInfo proInfo = type.GetProperty("Node");
            if (proInfo == null) return null;
            ObservableCollection<TreeViewNode> nodes = new ObservableCollection<TreeViewNode>();
            foreach (T o in this)
            {
                TreeViewNode node = (TreeViewNode)proInfo.GetValue(o);
                nodes.Add(node);
            }
            return nodes;
        }
    }


}
