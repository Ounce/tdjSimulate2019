# tdjSimulate2019

开发工具：Visual Studio 2019

语言：WPF、C#

## 框架：
解决方案tdjSimulate2019，托管在Github的公开项目。

类在tdjClassLibrary内定义。

各类由NotifyPropertyChanged派生，而此类由接口INotifyPropertyChanged派生。

图形的各项参数，设置在图形类中，并在图形中设置数据类。这类图形一般为纵断面折线图、纵断面高程表等；但是这些图形组合成的具有各个项目特有的图形应该在各项目中定义和实现，这些类在项目的Model文件夹内定义。
