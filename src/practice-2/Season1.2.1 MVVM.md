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
- [CallerMemberName] 호출하는 멤버 명으로 자동할당 되도록 하는 애트리뷰트 

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
