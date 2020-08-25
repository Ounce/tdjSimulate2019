# Profile

接口ISlope、IScale

## ProfileViewModel

父类：NotifyPropertyChanged, IScale

构造函数为Slopes绑定了数据更新处理函数——SlopesCollectionChanged，在SlopesCollectionChanged处理函数中增加Slope事件时，为Slope进行必要的设置。如：绑定属性变化处理函数，设置比例等。

```c#
    public ProfileViewModel()
    {
		......
        Slopes.CollectionChanged += 
            new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
        ......
    }
```
```c#
    private void SlopesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                Slopes[e.NewStartingIndex].PropertyChanged += SlopePropertyChanged;
                Slopes[e.NewStartingIndex].HorizontalScale = HorizontalScale;
                Slopes[e.NewStartingIndex].VerticalScale = VerticalScale;
                break;
            ......
        }
    }
```
```c#
    private void SlopePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
		......
        switch (e.PropertyName)
        {
            case "EndAltitude":
                break;
        }
    }
```
### 属性

#### ObservableCollection\<SlopeViewModel> Slopes：纵断面数据。基于SlopeViewModel的视图模型。

#### Polyline：根据Slopes数据，与HorizontalScale或VertialScale计算Polyline的各个点的位置，并及时更新。

#### HorizontalScale

#### VerticalScale

### 方法

#### public void SetMaxMinAltitude()



## SlopeViewModel

在此类中计算Polyline各点的坐标。

