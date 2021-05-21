using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using tdjWpfClassLibrary.Project;

namespace tdjWpfClassLibrary
{

    public class TreeViewNode : NotifyPropertyChanged
    {
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;

        public Guid DataID { get; set; }

        /// <summary>
        /// 对应编辑页面编号。
        /// </summary>
        public PageType PageType { get; set; }
        public string Icon { get; set; }

        public string EditIcon { get; set; }

        public string DisplayName { get; set; }

        public ObservableCollection<TreeViewNode> Children
        {
            get => _children;
            set
            {
                if (value != _children)
                {
                    _children = value;
                    OnPropertyChanged("Children");
                }
            }
        }
        private ObservableCollection<TreeViewNode> _children;

        public TreeViewNode()
        {
            Children = new ObservableCollection<TreeViewNode>();
        }
    }

    public class CheckCollection : Collection<Check>
    {

    }
}
