# 내용

1. Forms 프로젝트
2. Application 프로젝트
3. DirectModules 구

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

## 3. DirectModules 구현

- IModule 구현

> 왜 필요한가?

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


