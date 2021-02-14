# Axis

数轴。数轴的后台数据实质上是刻度在数轴上的位置。数轴的外观在XAML文件中定义。

后台数据定义数轴数据的起止值、比例。

前台除定义外观外，还要定义RenderTransform，确定显示位置。

## 前台：

前台首先要定义命名空间，例如：

    xmlns:local1="clr-namespace:tdjWpfClassLibrary.Draw;assembly=tdjWpfClassLibrary" 

其次要定义转换器和计算参数，例如：

    <Window.Resources>
        <local1:ConverterPlus x:Key="ValuePlus"/>
        <local1:ConverterSubtract x:Key="ValueSub"/>
        <sys:Double x:Key="TickLength">20</sys:Double>
        <sys:Double x:Key="TickLength5">15</sys:Double>
        <sys:Double x:Key="TickLength10">10</sys:Double>
        <sys:Double x:Key="LabelWidthHalf">50</sys:Double>
        <sys:Double x:Key="LabelHeight">20</sys:Double>
        <sys:Double x:Key="LabelHeightHalf">15</sys:Double>
    </Window.Resources>

刻度数值标签的宽度（LabelWidth）和高度（LabelHeight）可根据数值位数定义，可以定义一个，也可定义多个。

同一个刻度的标签和刻度线应放入同一个Grid中，在后台绑定这个Grid的DataContext即可。标签数据模板和刻度线的模板就可以共用这个DataContext。

- ConverterPlus和ConverterSubtract为数值转换器，在ValueConverter.cs中定义。

- TickLength等为刻度线的长度和标签的宽度和高度等，在定义外观时需要这些参数计算位置。

在每个刻度线和标签中定义一个RenderTransform，以实现与图形同步位置变动。

    <Label.RenderTransform>
        <TranslateTransform Y="{Binding Y, ElementName=ExistPolylineTranslate}"/>
    </Label.RenderTransform>

