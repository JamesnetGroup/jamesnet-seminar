## 파일경로
jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/Local/ViewModels/MainContentViewModel.cs - 뷰모델 
jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/Local/ViewModels/RelayCommand.cs - RelayComand 구현 

jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/Themes/Views/MainContent.xaml 뷰 
jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/UI/Views/MainContent.cs - 뷰 비하인드코드

# MVVM
## 뷰 비하인드 코드에서 DataContext를 통해서 VM이 지정되어 있습니다.
```C#
	public MainContent()
        {
            DataContext = new MainContentViewModel();
        }
```

### INotifyPropertyChanged
- View에서 바인딩중인 프로퍼티가 변경될때 ViewModel 상에 알리고 View를 변경시키려면 ViewModel상에 INotifyPropertyChanged를 상속받고 구현해야함.
- INotifyPropertyChanged에는 event PropertyChangedEventHandler? PropertyChanged; 가 지정되어 있음.
#### 기본적인 OnPropertyChanged
```C#
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string selectedId;
    public string SelectedId
    {
	   get { return selectedId; }
        set
        {
            if (selectedId != value)
            {
                selectedId = value;
                OnPropertyChanged(nameof(SelectedId));
            }
        }
    }
```
- 구현된 OnPropertychanged 이벤트를 바인딩된 프로퍼티가 변경 될때 OnPropertyChanged를 호출해서 View에 알리도록 한다.

#### 개선된 버전 
제네릭과 CallerMemberNameAttribute를 사용하면 조금 더 깔끔하고 보편적인 코드 작성이 가능해집니다.
1. 제네릭을 이용해 필드에 값을 할당하는 부분을 일반화할 수 있습니다.
2. 메소드의 선택 매개변수에 CallerMemberName 특성을 적용하면, 해당 매개변수에 메소드를 호출한 메서드, 속성 혹은 이벤트의 이름이 할당됩니다.

따라서 OnPropertyChanged 메소드에서 property 이름을 넣어주는 부분을 제거하고, 제네릭을 이용해 값을 할당하도록 하는 것을 기본으로 다음과 같이 구현할 수 있습니다.
```C# 
        protected void SetProperty<T>(ref T oldValue,
            T newValue,
            [CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                oldValue = newValue;
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private CompanyModel _currentItem;
        public CompanyModel CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, value);
        }
```
#### CommunityToolkit.Mvvm 패키지를 이용하는 방법
CommunityToolkit.Mvvm에서는 값을 갱신하는 부분과 속성 변경을 알려주는 부분의 자동 구현을 소스생성기 기반의 특성으로 제공합니다. 필드를 선언한 후 ObservableProperty 특성을 적용하면 소스 생성기에서 속성을 자동으로 생성해 줍니다. 이때, 필드의 변수명은 lowerCamelCase여야 하며 \_(언더바) 혹은 m_ 등의 접두사가 허용됩니다.
```csharp
        [ObservableProperty]
        private CompanyModel _currentItem;
	/* 소스 생성기에서 자동으로 생성하는 코드
	public CompanyModel CurrentItem
	{
		get => _currentItem;
		set
		{
			// NotifyPropertyChanging(값 변경 알림)
			// _currentItem 필드에 값 할당
			// OnPropertyChanged(값 변경 알림)
		}
	}
	*/
```

## RelayCommand 
- WPF에서는 xaml에서 ICommand를 상속받은 프로퍼티를 바인딩하는 방식으로 MVVM 패턴의 커맨드패턴을 구현 할 수 있습니다.
- RelayCommand는 ICommand를 상속받아 구현하는 기본적인 방식입니다. 
- RelayCommand의 구현내용은  jamesnet214/wpf/src/practice-2/WpfApp1/WpfControlLibrary1/Local/ViewModels/RelayCommand.cs 에서 확인 하세요.
- 뷰모델파일에서 사용된 코드는 다음과 같습니다. 
```C#
        public ICommand CheckCommand { get; init; }
        public ICommand SelectionCommand1 { get; init; }
        public MainContentViewModel()
        {
            Items = GetItems();
            CurrentItem = Items[1];

            CheckCommand = new RelayCommand<object>(Check);
            SelectionCommand1 = new RelayCommand<object>(Selection);
        }

        private void Selection(object obj)
        {
        }

        private void Check(object obj)
        {
            
        }
```
