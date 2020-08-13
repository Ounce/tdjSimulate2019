using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace tdjClassLibrary
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        //实现INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        //此方法由每个属性的Set访问者调用。
        //应用于可选propertyName的CallerMemberName属性
        //参数导致调用者的属性名称被替换为参数。
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
