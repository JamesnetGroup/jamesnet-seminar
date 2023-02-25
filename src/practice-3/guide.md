# 내용

1. Forms 프로젝트
2. Application 프로젝트
3. Core 프로젝트
4. DirectModules
5. PrismRegion 
6. ILoadable 
7. IEventAggregator
8. PubSubEvent<T>
9. CommunityToolkit.Mvvm

## 1. Forms 프로젝트

- WPF 사용이 가능한 Class Library 생성합니다.

#### CustomControl 생성 (UI/Views)
- MainWindow : Window
- MainContent : ContentControl

#### ViewModel 생성 (Local/ViewModels)
- MainContentViewModel

## 2. Application 프로젝트

- WPF 애플리케이션 프로젝트를 생성합니다.
- 누겟 Prism.Unity을 설치합니다.

#### cs 파일생성 (루트)
- App.cs : PrismApplication
- Starter.cs (STAThread 진입점)

#### Override 구현
- CreateShell

```csharp
protected override Window CreateShell()
{
    return new MainWindow();
}
```

- RegisterTypes

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // containerRegistry.RegisterSingleton : 인스턴스 선언 (필요한 시점에 생성됨)
    // containerRegistry.RegisterInstance : 인스턴스 생성
}
```

- ConfigureModuleCatalog

```csharp
protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
{
    base.ConfigureModuleCatalog(moduleCatalog);
    
    // 추후작업
    foreach (IModule module in _modules)
    {
        moduleCatalog.AddModule(module.GetType());
    }
}
```

#### ViewModel 연결

- WireViewModel 체인 메서드 구현

```csharp
internal App WireViewModel()
{
    ViewModelLocationProvider.Register<MainContent, MainContentViewModel>();
    return this;
}
```

#### 애플리케이션 시작
- WireViewModel 체인메서드 호출
```csharp
[STAThread]
static void Main(string[] args)
{
    _ = new App()
        .WireViewModel()
        .Run();
}
```

## 3. Core 프로젝트

- WPF 사용 가능한 클래스라이브러리 생성

#### PrismContent 생성
- ViewModelLocationProvider를 통한 VM 생성 및 시점 구현


```csharp

public PrismContent()
{
    ViewModelLocationProvider.AutoWireViewModelChanged(this, WireViewModelChanged);
}

private void WireViewModelChanged(object arg1, object arg2)
{
    if (arg1 is FrameworkElement fe && arg2 is INotifyPropertyChanged vm)
    {
        fe.DataContext = vm;
    }
}
```

## 4. DirectModules 구현

- IModule 구현

> 왜 필요한가?

> 시점 차이는?

```csharp
internal class DirectModules : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("MainRegion", typeof(LoginContent));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
            
    }
}
```

- AddModule 구현

> ConfigureModuleCatalog에서 _modules 등록하도록 추가

```csharp

internal App AddModule<T>() where T : IModule, new()
{
    IModule module = new T();
    _modules.Add(module);

    return this;
}
```

## 5. PrismRegion 구현

> Region을 꼭 써야 할까?
 
```csharp
public class PrismRegion : ContentControl
{
    public static readonly DependencyProperty ContentNameProperty = 
        DependencyProperty.Register("RegionName", typeof(string), typeof(PrismRegion), 
            new PropertyMetadata(ContentNamePropertyChanged));

    public string RegionName
    {
        get => (string)GetValue(ContentNameProperty);
        set => SetValue(ContentNameProperty, value);
    }

    private static void ContentNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is string str
            && string.IsNullOrEmpty(str) == false
            && Application.Current?.CheckAccess() == true)
        {
            IRegionManager rm = RegionManager.GetRegionManager(Application.Current.MainWindow);
            RegionManager.SetRegionName((PrismRegion)d, str);
            RegionManager.SetRegionManager(d, rm);
        }
    }

    public PrismRegion()
    {
    }
}
```

## 6. ILoadable 구현

```csharp
private void PrismContent_Loaded(object sender, RoutedEventArgs e)
{
    if (DataContext is IViewLoadable loadableView)
    { 
        loadableView.OnLoaded(this);
    }
}
```

## 7. EventAggregator

- Publish
- Subscribe
    
## 8. PubSubEvent<T>

- PubSubEvent`<T>`
- CustomEvent
    
## 9. CommunityToolkit.Mvvm
- ObservableObject
- partial class
- [ObservableProperty]
- partial Changed
- [RelayCommand + CanExecute](https://forum.dotnetdev.kr/t/relaycommand-canexecute/5215/7?u=jamesnet)
    
```csharp
sing CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace JamesCommand
{
    public partial class MyViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _keywords;

        private bool CanSave(object o)
        {
            return !string.IsNullOrWhiteSpace(Keywords) && Keywords.Length > 0;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private void Save(object o)
        {
            // Save.
        }
    }
} 
```
