## WPF와 일반 C# 프로젝트 차이점
- project 파일에 UseWPF가 추가됨 (콘솔 프로젝트에서도 UseWPF만 추가하면 사용할 수 있음.)
- WPF는 윈도우에서 만 들어가기 때문에 
```
 <TargetFramework>net7.0-windows</TargetFramework>
 <UseWPF>true</UseWPF>
```
가 포함됨. 
- 클래스 라이브러리 생성시에도 마찬가지로 WPF를 사용하고 싶으면 위 xml 코드를 입력해야함

## StackPanel
- UI내부적으로 사용되는 경우가 많음

## Grid
- Grid에서는 Layer가 중첩되서 컨트롤이 들어가게 된다. 
- opacity : 투명도 
- Alignment 값을 주게되면 Stretch 상태가 변경된다
#### GridDefinition 
- auto : fit으로 동작함
- * : 남는 영역을 다 잡음 
- 한쪽에 span 설정 하게되면 auto 행/열은 자동으로 최소로 잡게 됨.


## DataContext
- 모든 컨트롤에 다 있음
- 컨트롤은 대부분 FrameworkElement를 상속받는데 해당 class에 DataContext를 포함하고있음. 타입은 object 
- 바인딩을 할때 DataContext에 바인딩 대상이 포함되어있어야 바인딩이 가능하다.
- 바인딩시 컨트롤에서 상위노드로 가장 가까운 DataContext를 가져온다

## Bubbling (일반 이벤트) / Tunneling (Preview 이벤트)
- xaml control에 모두 이벤트가 있을때 내부 부터 하나씩 모두 이벤트가 발생된다. 
- 상위컨트롤 이벤에서 하위컨트롤에서 어떤일이 발생됐는지등에 대해서 파악가능하고 활용이가능하다.
- ##### PrieviewEvent에서는 순서가 반대로 일어난다.
- Event E.Handled = true; 그 뒤에부터 나오는 이벤트를 차단 시킴 

## ControlTemplate
### TemplateBinding 
- 상위 노드의 스타일을 가져오도록 한다.


## ContentTemplate
- content에 바인딩하는 템플릿임.
- Template와 같이쓰게 되면 ContentTemplate가 먼저 무시되고 새로 재정의해야함.
- 정의된 ContentTemplate를 사용하려면 Present를 사용하면된다. 



## Template 가 가장 상위에 있음
- ControlTemplate을 사용하면 CotentTemplate가 재정의 되어 나오지 않게됨. 정의된 ContentTemplate을 사용하려면 ContentPresenter를 사용할 것

```
<Window.Resource>
	<Style TargetType={x:Type Button}>
		<Setter Property="ContentTemplate">
			<Setter.Value>
				<DataTempate>
					여기에 정의하고 싶은 Content 바인딩 항목을 정의한다.
					<StackPanel>
						<TextBlock text={Binding Line} />
						<TextBlock text={Binding Proc} />
					</StackPanel>
				</DataTempate>
			</Setter.Value>
		</Setter>
		<Setter Property="Template">
			<Setter.Value>
			//컨트롤 내부를 정의하고 위에 정의된 dataTemplate를 사용하고 싶으면 ContentPresenter 로 내부에 바인딩해야함.
				<ControlTemplate TargetType={x:Type Button}>
					//컨트롤 템플릿 내부구현
					<ContentPresent />
					
				</>
			</Setter.Value>
			
		</Setter>
	</Style>

</Window.Resource>

```



## RelativeSource 

```
<CheckBox Grid.Column="0" IsChecked="{Binding RelativeSource={RelativeSource AncestorType=ToggleButton}, Path=IsChecked}"/>

```
- RelativeSource는 ControlTemplate 내부에서 기존 컨트롤과 관계를 연결한다.
- CheckBox는 내부에 ToggleButton이 있음.
