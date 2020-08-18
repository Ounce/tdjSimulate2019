# tdjSimulate2019

开发工具：Visual Studio 2019

语言：WPF、C#

## 框架：

解决方案tdjSimulate2019，托管在Github的公开项目。

类在tdjClassLibrary内定义。

各类由NotifyPropertyChanged派生，而此类由接口INotifyPropertyChanged派生。

图形的各项参数，设置在图形类中，并在图形中设置数据类。这类图形一般为纵断面折线图、纵断面高程表等；但是这些图形组合成的具有各个项目特有的图形应该在各项目中定义和实现，这些类在项目的Model文件夹内定义。

## tdjClassLibrary的命名空间

tdjClassLibrary是库项目，PresentationFramework.dll是手动添加的，这个dll包含了System.Windows.Shapes命名空间，Polyline在这个命名空间里。

Point类使用了System.Drawing中的类。但这个类中的X、Y，为int，PointF的为float。

## 视图类ViewModel的结构（第一种）

视图类定义具体数据，以及绘图显示等功能的视图控件及其参数。

### ProfileViewModel的结构

- Slopes
- AltitudeTable：高程表
- 比例图（Ployline）
- MileageTable：里程表

## 视图类ViewModel的结构（第二种）

视图类分成数据和控件两类。

### ProfileViewModel的结构

- Slopes
- Length

### AltitudeTable

- Top
- Left
- Slopes引用
- AltitudeLabes高程数据列表
- HorizontalScale

### MileageTable

- Top
- Left
- Slopes引用

### ScaleDrawing比例图

- Top
- Left
- Slopes引用

  