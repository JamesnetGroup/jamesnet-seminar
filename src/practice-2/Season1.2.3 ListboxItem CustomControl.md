## 리스트 박스내에 컬렉션의 항목을 정의합니다.
### 파일 경로
-  jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/Themes/Units/CompanyListItem.xaml
-  jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/UI/Units/CompanyListItem.cs


## SharedSizeGroup 
## IsSharedSizeScope
WPF에서 `SharedSizeGroup`는 `Grid`, `WrapPanel`, `StackPanel` 등의 `ItemPanel`에서 사용할 수 있는 속성 중 하나로, 다른 컨트롤과 너비를 공유할 수 있도록 해줍니다. `Grid.IsSharedSizeScope`는 `Grid`에서 `SharedSizeGroup`을 사용할 때, `Grid` 내에서 너비를 공유할 열의 범위를 지정하는데 사용됩니다.

`SharedSizeGroup`과 `Grid.IsSharedSizeScope`를 함께 사용하면, 동일한 그룹 이름으로 지정된 `SharedSizeGroup`를 가진 컨트롤들 간에 동일한 너비를 가지도록 할 수 있습니다. `Grid.IsSharedSizeScope`를 True로 설정한 `Grid`는 해당 `Grid` 내에서 `SharedSizeGroup`을 사용하는 컨트롤들 간에 동일한 너비를 공유합니다. 이를 통해, `Grid` 내에서 동일한 너비를 가지는 컨트롤들을 배치할 때, 균일하게 배치되도록 할 수 있습니다.

- 1강에 나온 ContentTemplate 내용과 유사하지만 여기서 정의되는 내용은 ListBox 내부에 배치됩니다. 

- #### 아래 내용은 데이터 템플릿을 만드는 내용입니다.
```
    <DataTemplate x:Key="CompanyItemTemplate">
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" SharedSizeGroup="COMP_ID"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <TextBlock Grid.Column="0" Margin="4" Text="{Binding Id}"/>
            <TextBlock Grid.Column="1" Margin="4" Text="{Binding Name}"/>
        </Grid>
    </DataTemplate>
```

- 템플릿 스타일 재정의에서 아래와 같이 위에 정의된 DataTempalate을 사용하는 것을 확인할 수 있습니다.
```
	<Style TargetType="{x:Type units:CompanyListItem}">
-- 생략
        <Setter Property="ContentTemplate" Value="{StaticResource CompanyItemTemplate}"/>
-- 생략 
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type units:CompanyListItem}">
-- 생략 
                        <Grid>
                            <Grid.RowDefinitions>
                                <RowDefinition Height="Auto"/>
                                <RowDefinition Height="Auto"/>
                            </Grid.RowDefinitions>
                            <TextBlock Text="Content" Padding="4"/>
                            <ContentPresenter x:Name="content" Grid.Row="1" Visibility="Collapsed"/>
                        </Grid>
-- 생략
```

