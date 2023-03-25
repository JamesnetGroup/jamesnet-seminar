## 파일 경로
##### jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/UI/Units/CompanyList.cs
##### jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/Themes/Units/CompanyList.xaml

## ListBox 기본 컨트롤 
- ##### ItemSource : IEnumerable 속성이므로 IEnumerable을 상속받는 컬렉션 프로퍼티로 바인딩 가능합니다. 
- 주요 속성 
```xaml
<ListBox 
            ItemsSource="{Binding Accounts}"
            DisplayMemberPath="Name"
            SelectedValuePath="Id" 
            SelectedValue="{Binding SelectedId}" 
            SelectedItem="{Binding SelectedAccount}" />
```
- ##### DisplayMemberPath : 보여지는 항목
- ##### SelectedValuePath : ListBox Item 선택시 Value로 지정되는 SelectedItem 인스턴스의 프로퍼티 명칭
- ##### SelectedValue : SelectedValuePath에서 지정된 항목을 바인딩할 ViewModel의 프로퍼티
- ##### SelectedItem : 선택된 항목을 바인딩할 viewModel의 프로퍼티



### ListBox Template
- Grid.IsSharedSizeScoped : 그리드안에 내용물간의 크기를 같도록 지정함.
- ScrollerViewer : 스크롤을 사용하고싶으면 써아하는 컨트롤

- #### ItemsPresenter : 1강에서 ContentControl을 대신해서 ItemList가 들어갑니다.
- ##### 강의 소스상에서는 ListBox와 ListBoxItem 코드가 분리가 되어 ItemsPresenter로 전달하지만 한소스에 포함되는 구조가 된다면 다음과 같습니다.
```xaml
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
	<Style TargetType="{x:Type units:CompanyList}">
		<Setter Property="ItemTemplate" Value="{StaticResource CompanyItemTemplate}" />
		... 기타 스타일 생략 
	</Style>
```


- #### 마지막 커밋의 xaml 코드상에서는 <ItemsPresenter/> 로 지정되어 뷰코드만 보고는 어떤 템플릿과 연결되어 있는지 알수없지만 해당 뷰의 cs파일로 가면 다음과 같이 지정되어있는 것을 확인 가능합니다. 
```C#
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CompanyListItem();
        }
```

- {Binding RelativeSource={RelativeSource AncetorType=ListBox},Path=Items.Count} :  xaml 구조상 가까이 있는 상위 노드중 지정하는 타입의 DataContext중 path에 해당하는 것을 가져옵니다. 
- ItemsPanel : 각 타입별로 ListBox의 보여지는 방식을 다르게 지정합니다. 
	- ItemsPresenter에 표시해주는 패널에 대해서 재정의
	- StackPanel - 기본
	- WrapPanel - 자동으로 Wrap을 처리해줌
	- UniformGrid - 행 또는열을 고정하여 ListBox를 구성

## CompanyList.cs 설명 
##### public static readonly DependencyProperty SelectionCommandProperty = DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(CompanyList));
- Parameter 해당 속성의 이름, 속성 값의 형식, 소유자 타입       
- DependencyProperty : 해당 컨트롤을 사용할때 xaml에서 지정할수있는 컨트롤을 지정합니다.  다음과 같이 사용합니다. 
```
<units:CompanyList ItemsSource="{Binding Items}"
                                               SelectionMode="Extended"
                                               SelectionCommand="{Binding SelectionCommand1}"
                                               SelectedItem="{Binding CurrentItem}"
                                               SelectedValuePath="Id"/>
```

```C#
        public CompanyList()
        {
            PreviewMouseLeftButtonDown += CompanyList_MouseLeftButtonDown;
        }

        private void CompanyList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement fe && fe.DataContext != null)
            {
                SelectionCommand.Execute(fe.DataContext);
            }
        }
```
- 위와 같이 SelectionCommand를 별도로 구현한 이유는 MultiSelected 상태에서 다시 선택하는 아이템에 대해서 체크할때 사용하기 위해서 입니다. 
