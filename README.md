# WPF_Calendar
일정, 루틴 데이터를 입력하면 달력 UI를 통해 매일, 매주, 매달 해야 할 일을 간단하게 확인하고 완료 여부를 관리할 수 있는 프로그램

# 개발 기간
- 2025.12.01 ~ 26.03.07 (ver 1.0 - 기본 기능 구현 완료)

# 사용 기술
C#, WPF, MVVM 패턴, JSON Serialization

# 신경쓰며 만든 것
 1. MVVM 패턴 구현 (Model과 View 사이의 상호 의존성을 최소화, ViewModel을 통해 Data Binding, Command를 사용하여 효율적으로 데이터를 처리)
  - ViewModel에서 Window(View)를 생성하는건 MVVM에 위배되므로 Messenger를 통해 WindowService의 기능을 호출
 2. 사용자 UI, 편의성
  - 프로그램 전체화면으로 변경할때 작업 표시줄은 나타나게 창 크기 조절
  - 최소화시 SystemTray로 이동시키는 기능 구현
  - OS 부팅시 프로그램 자동 실행 할 수 있게하는 기능 구현
 3. 달력, 일정
  - 임시 공휴일이 제대로 적용되게 구현
  - 일정을 생성할때 주기를 정하여 규칙이 자동으로 생성되게 구현
 4. 데이터 저장
  - UI 스레드가 멈추지 않도록 데이터 저장은 async/await을 사용하여 비동기로 처리
  - CancellationTokenSource를 사용하여 3초동안 입력이 없으면 데이터 저장 실행 (매번 데이터를 처리하지않고 입력이 없을때 한번에 처리)
  - 데이터 저장시 .tmp로 먼저 저장하고 기존 파일과 교체 (데이터 저장 중 프로그램 종료시 데이터 깨지지 않도록)
 5. 데이터 수정 ★
  - 데이터를 수정할때 여러가지 조건을 따져서 분기를 정해 해당 분기에 맞는 로직을 실행하게 구성했음
  - 지금 적용이 안되고있으니 수정한 후 ReadMe도 다시 수정해야함
 6. Interface 사용 (익숙하진 않아서 사용할수있는 부분에 적용하며 만듦)
  - ITodoRepository, ISettingRepository 인터페이스를 구현해 저장 방식을 교체해도 다른 로직에 영향을 주지 않도록 구현
  - IRoutineViewModel이라는 공통 인터페이스를 구현해서 ViewModel이 달라도 공통 메서드는 호출할 수 있게끔 다형성 구현

## 주요 기능
### 달력

https://github.com/user-attachments/assets/8138e091-2bad-4755-ad14-eb0816d3c83b

 - CalendarDayModel이라는 DataClass를 만들어서 날짜마다 정보(날짜, 요일, 휴일 여부, 선택 상태, 해당 날짜의 일정과 규칙 등)를 저장 (매달 최소 28개의 CalendarDayModel이 존재) 
 - 달력의 달이 바뀌면 CalendarDayModel들을 전부 교체하고 데이터 저장소에 접근해서 표시해야할 Schedule과 Routine이 있는지 확인하여 적용함
 - 달력의 년도가 바뀌면 HolidayProvider에서 해당 년도의 공휴일과 임시 공휴일을 계산하여 가지고있고, CalendarDayModel이 생성될때마다 HolidayProvider에게 오늘이 휴일인지 체크하여 휴일을 표시함
 - 달력의 날짜를 클릭하면 해당 날짜의 일정이 왼쪽 패널에 표시되고 체크박스로 완료 여부를 변경할 수 있음

#### 일정, 규칙 등록

https://github.com/user-attachments/assets/aa66ac2e-1992-4e67-8e5b-05bcceea4bd7

 - 일정이나 규칙을 추가할땐 MainViewModel에서 TodoWindow를 그려달라고 ViewModel을 만들어서 Messenger에 메세지를 보내고 app.xaml.cs에서 해당 메세지를 확인하고 ViewModel에 맞는 Window를 WindowService에 요청해서 화면에 띄워줌
 - Window에서 데이터를 입력하고 등록 버튼을 누르면 TodoStorage에 데이터를 추가하고 DataManager에 데이터 저장 요청, Messenger에 UI 업데이트 메세지를 보냄 -> 이를 CalendarViewModel이 확인하여 UI를 갱신
 - DataManager는 데이터 저장 요청이 들어오면 TodoStorage의 값들을 통째로 Json으로 변환하여 경로에 저장함

#### 일정, 규칙 수정

https://github.com/user-attachments/assets/7aff627a-3c94-46ff-9863-9ff90022f27f

 - 일정이나 규칙을 수정할땐 날짜를 선택하고 왼쪽 패널의 목록에서 텍스트를 더블 클릭하여 수정 창을 열 수 있음 (Window를 여는건 Messenger에게 Window를 그려달라고 메세지를 보내는 방식)
 - Window에서 데이터를 수정하고 수정 버튼을 누르면 TodoStorage에서 해당 데이터를 수정하고 DataManager에 데이터 저장 요청, Messenger에 UI 업데이트 메세지를 보냄 -> 이를 CalendarViewModel이 확인하여 UI를 갱신
 - DataManager는 데이터 저장 요청이 들어오면 TodoStorage의 값들을 통째로 Json으로 변환하여 FileHelper에 입력된 경로에 저장함
 
#### 일정, 규칙 삭제

https://github.com/user-attachments/assets/9d19567c-c5c7-4b96-95ba-42435b45f296

 - 일정이나 규칙을 제거할땐 수정 창을 열어서 삭제 버튼을 눌러 삭제가 가능함
 - Window에서 삭제 버튼을 누르면 TodoStorage에서 해당 데이터를 삭제하고 DataManager에 데이터 저장 요청, Messenger에 UI 업데이트 메세지를 보냄 -> 이를 CalendarViewModel이 확인하여 UI를 갱신
 - DataManager는 데이터 저장 요청이 들어오면 TodoStorage의 값들을 통째로 Json으로 변환하여 FileHelper에 입력된 경로에 저장함

#### 일정, 규칙 목록

https://github.com/user-attachments/assets/77210e47-88be-403d-bb5a-697503a149d4

 - 목록 버튼을 눌러서 등록한 모든 일정과 규칙을 확인하고 삭제, 수정 할 수 있음
 - 체크박스를 선택해 삭제할 일정을 여러개 선택할 수 있고 체크박스 외의 흰 바탕 부분을 클릭하면 일정이 파란색으로 선택되는데 수정 버튼을 누르면 파란색으로 선택된 데이터를 수정할 수 있음 (더블 클릭으로도 수정 가능)
 - 상단 탭으로 일정, 규칙, 과거 규칙 세가지 형태의 데이터에 접근이 가능

### 설정

https://github.com/user-attachments/assets/b89b817f-09d5-47d6-ac11-f1df0a4c0d08

 - 설정 버튼을 눌러서 프로그램 설정을 변경할 수 있음 (부팅시 프로그램 자동 실행, 최소화시 시스템 트레이로 이동)
 - 프로그램 자동 실행을 키면 AutoStartService에서 Registry에 접근해 현재 사용자의 권한으로 자동 실행을 등록함
 - 최소화시 시스템 트레이로 이동을 키면 프로그램 전역에서 Messenger를 통해 WindowService의 Minimize를 호출할 경우 MainWindow를 작업 표시줄에서 안보이게 변경하고 Alt + Tab을 눌러도 보이지 않게 Hide()로 상태를 변경함

### 만들며 어려웠던 점과 어떻게 해결했는지
 1. /View/Calendar/CalendarView에서 각 날짜마다 해야할 일을 나열할때 n번째 데이터는 조건에따라 텍스트를 변경하거나 Visible을 관리해야해서 ItemsControl의 AlternationCount를 이용해 데이터마다 번호를 부여한 뒤
    MultiDataTrigger에서 데이터마다 할당받은 AlternationIndex에 접근해 자신이 몇번째 데이터인지 확인하여 조건에따라 처리하려했는데 모든 데이터의 AlternationIndex가 0으로 인식되는 오류가 발생했었다.
    
    찾아보니 WPF의 ItemsControl은
    ① 만들어야하는 Items의 갯수만큼 ContentPresenter라는 틀을 생성하고 내부에 필요한 Elements(UI 요소)들을 생성하여 쌓아둔 뒤
    ② 각 Elements들에 설정된 Style, Trigger 등을 적용한 후에
    ③ ContentPresenter별로 내부 Elements들의 위치 배열, 부모 자식 관계 연결, ContentPresenter들의 순서 배치 등의 단계를 거친다.
    여기서 3번의 단계에서 각 데이터마다 AlternationIndex를 할당받는데 MultiDataTrigger는 2번 단계에서 AlternationIndex 값을 받아와 처리하려했기때문에 모든 데이터의 AlternationIndex가 0으로 인식되고있었다.
    
    MultiDataTrigger는 자신과 같은 라인의 자식 요소들의 속성 변화는 잘 감지하는데 부모 요소인 ContentPresenter의 속성(AlternationIndex)을 주시하게하면 첫 로딩시점에만 검사하고 이후엔 값의 변화를 놓칠수있다고하기에
    MultiDataTrigger에서 자신과 같은 ContentPresenter의 자식 요소인 Grid의 Tag를 검사하게 변경하고 Grid.Tag는 Binding으로 AlternationIndex의 값과 연결해 AlternationIndex의 값이 변경되면 Tag값도 즉시 변경되도록 만들었다.
    이렇게하면 AlternationIndex가 부여돼 Grid.Tag가 업데이트되고 MultiDataTrigger에서 Grid.Tag의 변화를 감지하여 3번의 단계에서도 정상적으로 MultiDataTrigger가 동작할 수 있도록 만들어서 해결했다.
    
 2. /Common/Util/FileHelper에서 데이터를 Json 형식으로 저장하려했는데, 기존 Json 데이터만 남아있고 변경한 데이터는 저장되지않고 사라지는 문제가 발생했었다.

    프로그램이 실행될 때 LoadJson으로 Json 데이터를 읽어왔는데 이때 데이터를 읽어온 후 파일을 닫지 않아서 계속 Json 파일이 실행중인 상태였고
    데이터를 저장하기위해 Json 파일에 접근하는데 이미 사용중인 파일엔 정삭적으로 쓰기 작업이 불가능해서 데이터 저장에 실패하는 상황이였다.

    using을 이용해 Json 데이터를 읽어온 후 확실하게 파일을 종료하여 데이터를 저장할때 파일이 사용중이지 않게 변경하여 해결했다.
    
 3. /Common/Service/WindowService에서 메신저를 통해 Minimize가 호출됐을때 "예외 발생: 'System.InvalidOperationException'(PresentationCore.dll)'" 라고 뜨면서 프로그램이 멈추는 오류가 발생했었다.

    찾아보니 UI 스레드가 아닌 다른 스레드에서 UI 요소를 수정하려할때 발생한다고하는데 보통 Dispatcher를 사용해 UI 스레드가 한가할때 작업 시키기위해 대기시키는 방법으로 해결한다고해서 따라해봤는데 계속 동일한 오류가 반복됐다.
    근데 아무리 생각해봐도 사용자가 버튼을 클릭 -> UI 이벤트 발생 -> 메인 스레드에서 Command 실행 -> Command 내부에서 Messenger를 통해 WindowService.Minimize 호출 이라는 단계를 거쳐 명령이 실행되는데 다른 스레드에서 UI를 수정하는건 아닌거같았다.
    다른 코드들도 확인하고있자니 나는 타이틀바를 커스텀으로 만들어서 사용하고있었고 /View/MainTitleBarView에서 /Common/Util/WindowBehavior를 사용해 커스텀 타이틀바의 윈도우 창 이동을 구현하고 있었다.
    WindowService의 Minimze는 커스텀 타이틀 바 내부의 최소화 버튼을 누르면 동작하는 방식이였다.
    최소화 버튼을 누를때 WindowBehavior의 DragMove가 잠깐 동작되고 이때 WindowService의 Minimize 요청까지 동시에 이루어져 하나의 동작이 이루어질때 다른 동작을 요청해서 충돌이 발생하는거였다.

    해결하기위해 WindowBehaivor에서 Mouse의 상태가 Pressed 상태일때만 DragMove가 동작하게 방어 코드를 작성하고, 또 다른 경우에 대비해 WindowService에 lock과 bool 변수를 추가해 한번에 하나의 동작만 가능하도록 방어 코드를 구현해 해결했다.
    
 4. 프로그램을 정상적으로 종료해도 메모리를 50 ~ 60MB씩 점유하고 정상적으로 종료되지 않는 문제가 발생했었다.
    
    프로그램이 종료되면 app.xaml.cs의 OnExit() 메서드가 호출되는데 여기서 프로그램 종료 직전에 최종으로 데이터를 저장하는 메서드를 호출하게 설계했다.
    그런데 그 메서드에서 비동기 데이터 저장 메서드 2개를 .GetAwaiter().GetResult()를 붙여서 호출하게 만들었고 이러면 UI 스레드에서 비동기 메서드가 종료될때까지 대기 상태에 돌입하게된다.
    비동기 메서드들은 자신의 일을 처리하고 시작 지점으로 돌아와서 완료 신호를 쏴야하는데 GetResult로 인해 UI 스레드 자체가 대기상태가 돼서 돌아갈수 없기때문에 데드락이 발생했었다.

    이를 해결하기위해 Task.Run을 사용해서 다른 스레드에게 비동기 작업 자체를 위임시켰다.
    (해당 스레드 내부에서 완료 신호까지 UI 스레드에게 전달해주기때문에 GetResult는 완료 신호를 받고 다음 코드를 진행할수있게된다.)

# 만들며 아쉬웠던 점
 화면 기획서는 잘 작성했지만 데이터 처리, 인터페이스, 객체간의 상속 등 세부적인 프로그램 동작 기획서를 작성하지 않고 개발을 시작했는데
 규칙(Routine) 추가 및 수정 기능을 구현할때 일간, 주간, 월간, 연간으로 나뉘는 복잡한 조건부 로직과 과거, 현재, 미래 어느 시점의 데이터를 수정하는지에 따라 바뀌는 수정 대상 등
 데이터 처리 구조를 설계하는 과정에서 코드 수정이 빈번히 일어나서 생각보다 많은 개발 시간이 소요됐었다.
 나중에는 동작 기획서, 로직 순서도 등 복잡한 과정을 어떻게 해결할지 미리 정해놓고 개발을 시작하는게 좋을 것 같다.

# 업데이트 예정 기능
 1. RoutineRecords가 많아지면 메모리 점유율이 높아질수있기에 RoutineRecords는 RoutineRecords 폴더를 만들어서 yyyy_MM.json 형태로 월별로 저장하기
  - 이에따라 TodoStorage의 Records에는 현재 표시중인 달의 RoutineRecords만 저장하게 변경하기 (캐시화)
  - ListWindow를 열때 TodoStorage의 Records에 접근하는게 아닌 ListWindow 자체에 모든 Records를 불러오게끔 만들기 (아니면 기간을 선택해서 해당 기간의 Records만 불러오기)
 2. RoutineRecords를 수정할땐 시작 날짜, 종료 날짜 안보이게하고 Title, Content, 날짜만 보여주기 (Schdule 창으로 적용시키거나 새로운 창 만들기)
