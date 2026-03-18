# WPF_Calendar 한 줄 소개
일정, 규칙 데이터를 입력하면 달력 UI를 통해 매일, 매주, 매달 해야 할 일을 간단하게 확인하고 완료 여부를 관리할 수 있는 프로그램

# 다운로드 방법
https://github.com/yofp1937/WPF_Calendar/releases 에서<br/>
최상단 게시글의 Assets 하단에 있는 Calendar.exe 파일 다운받아 실행하기

---

# 목차
 1. [개요](#1-개요)
 
 2. [프로그램 작동 영상과 설명](#2-프로그램-작동-영상과-설명)
 
 3. [주요 로직 설명](#3-주요-로직-설명)
 
 4. [개발 중 어려웠던 부분](#4-개발-중-어려웠던-부분)
 
 5. [아쉬웠던 점](#5-아쉬웠던-점)
 
 6. [업데이트 예정](#6-업데이트-예정)

 ---

# 1. 개요
 ### 1-1. 프로젝트 설명
 WPF 개발은 처음이라 MVVM 패턴, Data Binding, Command 등 WPF만의 패턴과 데이터 처리 방법 등을 공부하기위해 진행한 간단한 달력 프로젝트입니다.
 
 ### 1-2. 개발 기간
　2025.12.01 ~ 26.03.07 (ver 1.0 - 기본 기능 구현 완료)

 ### 1-3. 사용 기술
  #### 　① C#, WPF(.NET)
  #### 　② MVVM 패턴
  #### 　③ Messenger 패턴
  #### 　④ JSON Serialization

 ### 1-4. 사용 라이브러리
  #### 　① WindowForms.NotifyIcon
  #### 　② Microsoft.Win32.Registry
  #### 　③ System.Globalization
  　　- System.Globalization.Calendar, System.Globalization.KoreanLunisolarCalendar

-----

# 2. 프로그램 작동 영상과 설명
 ### 2-1. 달력
https://github.com/user-attachments/assets/ef2b8fac-d1e5-403b-9c59-ca365c8b9da0

 - 달력의 날짜를 클릭하면 해당 날짜의 일정이 왼쪽 패널에 표시되고, 체크박스로 완료 여부를 변경할 수 있습니다.
 - 체크박스를 체크하여 데이터의 상태(완료, 실패, 대기)를 변경하면 즉시 저장하지 않고, 3초동안 입력이 없을때 저장합니다.(값을 연속해서 바꿔도 한번만 저장)
 - 한 날짜의 일정, 규칙 갯수가 7개를 넘어가면 맨 아래 표시되는 일정 텍스트가 ...(+n)으로 변경됩니다.
 - 수백, 수천개에 달할수있는 미래 규칙은 미리 저장하지않고 달력을 그리는 시점에 실시간으로 계산해 UI에 표시

 ### 2-2. 일정, 규칙
  - ⑴ ScheduleData: 일정 관리용 DTO로 제목, 내용, 날짜, 완료 여부 등을 Property로 가집니다.
  - ⑵ RoutineData: 규칙의 설계도로 제목, 내용, 규칙 주기, 빈도 등의 여러 정보를 Property로 가집니다.
  - ⑶ RoutineRecord: 특정 날짜의 규칙 실행 결과를 저장하는 DTO로 규칙의 성공/실패 상태를 저장하는것이 주 목적입니다.<br/>
　　　　　　　　  원본 RoutineData가 변경돼도 과거 기록은 보존되도록 독립된 스냅샷 정보를 유지하게 설계했습니다.
  - ⑷ RoutineInstance: 규칙을 UI에 표시하기 위한 가상 모델로 과거 규칙은 RoutineRecord 기반으로,<br/>
　　　　　　　　　미래 규칙은 RoutineData 기반으로 데이터를 결합해 View에 일관된 데이터를 제공합니다.

 #### 　① 일정, 규칙 등록
https://github.com/user-attachments/assets/5f86c378-0f60-46b4-b69e-863438edb049

 - 달력에서 + 버튼을 눌러 일정, 규칙을 등록할 수 있습니다.
 - 데이터를 입력하고 등록 버튼 클릭 시 데이터가 등록됩니다.

 #### 　② 일정, 규칙 수정
https://github.com/user-attachments/assets/512f9474-eea9-4700-bd09-599c0ccb1ec4

 - 왼쪽 할일 목록에서 데이터를 더블 클릭하거나, 목록 창을 열어서 데이터를 수정할 수 있습니다.
 - 수정할 데이터를 입력하고 수정 버튼 클릭시 데이터가 수정됩니다.

 #### 　③ 일정, 규칙 삭제
https://github.com/user-attachments/assets/ff88e0a8-62e5-4e22-a758-e3b7219be7d5

 - 수정 창에서 삭제 버튼을 누르거나, 목록 창에서 데이터를 삭제할 수 있습니다.

 #### 　④ 일정, 규칙 목록
https://github.com/user-attachments/assets/50ee4d59-0672-42b6-aec3-de4c7934404d

 - 메인 Window의 목록 버튼을 누를 시 ListWindow가 생성 됨
 - 목록 버튼을 눌러서 등록한 모든 일정과 규칙을 확인하고 삭제, 수정 할 수 있음
 - 상단 탭으로 일정, 규칙, 과거 규칙 세가지 형태의 데이터에 접근이 가능
 - 체크박스를 선택해 삭제할 일정을 여러개 선택할 수 있음
 - 체크박스 외의 흰 바탕 부분을 클릭하면 일정이 파란색으로 선택되는데 수정 버튼을 누르면 파란색으로 선택된 데이터를 수정할 수 있음 (더블 클릭으로도 수정 가능)

 ### 2-6. 설정
https://github.com/user-attachments/assets/92bb2009-1e69-4366-93db-fb395b1bd649

 - 설정 버튼을 눌러서 프로그램 설정을 변경할 수 있습니다.
 - 최소화시 시스템 트레이로 이동을 키면 Messenger를 통해 Minimize를 요청할 경우 .Hide()로 프로그램이 보이지않게 상태를 변경합니다.
 - 프로그램 자동 실행을 키면 Registry에 접근해 현재 사용자의 권한으로 자동 실행을 등록합니다.

-----

# 3. 주요 로직 설명
 ### 3-1. 전체 데이터 흐름
<img width="4044" height="1764" alt="Image" src="https://github.com/user-attachments/assets/984e201c-1b9e-44fa-ae0c-19bf0094df58" />
  
 ### 3-2. 주요 데이터 흐름
 <img width="3484" height="1272" alt="Image" src="https://github.com/user-attachments/assets/0d27ed1c-e477-4b41-a909-35d2a9a7f716" />
 
 - ViewModel: 사용자가 입력한 Binding Data를 기반으로 Data를 생성하여 ITodoRepository에 처리를 요청하는 역할<br/>
 - ITodoRepository: ViewModel에게 전달받은 Data를 ITodoStorage로 전달하고<br/>
 　　　　　　　　처리 결과에따라 Storage의 상태를 Json 형태로 Local Repository에 저장하는 역할<br/>
 - ITodoStorage: ITodoRepository에게 전달받은 Data를 요청에따라 처리하고 결과를 반환해주는 역할<br/>
 
  #### 　① 데이터 추가, 수정 <a name="data-add-update"></a>
  <img width="4324" height="2780" alt="Image" src="https://github.com/user-attachments/assets/42987783-4acd-485b-9d21-67e7348c5cae" />

  - Data를 추가하거나 수정하려면 ViewModel에서 ITodoRepository의 AddOrUpdateData_AsyncSave에 Data를 넣어서 호출해야합니다.
  - ITodoRepository는 데이터 처리 요청을 그대로 ITodoStorage에게 전달합니다.
  - 동일한 Id의 데이터가 존재하면 [3-2. ② 데이터 삭제](#data-remove)에 기술된 Data 삭제 로직이 동작합니다.
  
  #### 　② 데이터 삭제 <a name="data-remove"></a>
  <img width="4404" height="3564" alt="Image" src="https://github.com/user-attachments/assets/91809a60-59a2-4705-85c5-3d089287e647" />

  - Data를 삭제하려면 ViewModel에서 ITodoRepository의 RemoveData_AsyncSave에 Data를 넣어서 호출해야합니다.
  - ITodoRepository는 데이터 처리 요청을 그대로 ITodoStorage에게 전달합니다.
  - ViewModel에서 List 형태의 Data를 넘기면 foreach로 하나씩 제거를 요청합니다.

 ### 3-3. 달력 생성 데이터 흐름

 #### 　① 달력 생성 데이터 흐름도 (CalendarViewModel)
 <img width="4204" height="2844" alt="Image" src="https://github.com/user-attachments/assets/73e4c87d-cfaa-4b4d-b1c0-10d6f6edc1f8" />
 
 - 달력의 연도가 바뀌면 [HolidayProvider](#holiday-provider)로 해당 연도의 공휴일을 생성합니다.
 - 달력을 그릴때마다 우선적으로 첫주 ~ 마지막주까지 날짜를 생성합니다.(날짜는 [CalendarDayModel](#②-calendardaymodel)이라는 Class로 구현)
 - **날짜 생성 이후 날짜마다 표시해야할 일정, 규칙이 있는지 확인하고 등록합니다.**
 
 #### 　② CalendarDayModel 데이터 흐름도
 <img width="3604" height="2168" alt="Image" src="https://github.com/user-attachments/assets/3e720f36-f317-4288-a804-3d9ebef500ce" />
 
 - CalendarDayModel은 달력의 날짜 하루당 하나씩 생성합니다.
 - 설정된 날짜에 표시할 일정 List, 규칙 List와 **외부에서 접근시 자동으로 일정 List와 규칙 List를 설정에따라 정렬하여 반환해주는 AllTasksView가 존재**합니다.
 - CalendarViewModel에서 CalendarDayModel이 생성되면 생성자에서 날짜, 요일, 휴일 여부 등 기본적인 데이터를 자동으로 설정합니다.
 - **SidePanelTasksView에서 선택된 날짜의 일정, 규칙을 표시하기위해 CalendarDayModel의 AllTasksView를 Binding합니다.**
 - **생성자에서 일정, 규칙 List에 Data가 추가될 경우 OnPropertyChanged를 호출해 UI가 AllTasksView를 갱신하게 해주는 이벤트를 설정합니다.**

 #### 　③ HolidayProvider (음력 공휴일을 양력 날짜로 치환, 대체 공휴일 계산) <a name="holiday-provider"></a>
 <img width="4248" height="2848" alt="Image" src="https://github.com/user-attachments/assets/1103b793-af6d-47ea-8ef3-b1117b06856d" />
 
 - 음력 공휴일은 .NET 프레임워크에 내장된 KoreanLunisolarCalendar 라이브러리를 활용해 구현합니다.
 - 달력의 연도가 바뀌면 HolidayProvider에서 해당 연도의 공휴일과 임시 공휴일을 계산하여 가지고있고,<br/>
 CalendarDayModel에서 접근해 자신이 휴일인지 확인합니다.

 ### 3-4. 일정, 규칙 관련 데이터 흐름

 #### 　① 일정, 규칙 추가 데이터 흐름도
 <img width="4164" height="324" alt="Image" src="https://github.com/user-attachments/assets/396d5e27-9c6a-4bd2-aa31-7b4d2a57804e" />
 
 - 데이터를 입력하고 등록 버튼을 누르면 ViewModel에서 데이터를 기반으로 Instance를 생성합니다.
 - ITodoRepository의 AddOrUpdateData_AsyncSave에 Instance를 삽입해 호출합니다.([3-2. ① 데이터 추가, 수정](#data-add-update)의 로직대로 동작)
 - 처리가 끝나면 ViewModel에선 Messenger에 UI 업데이트를 요청하는 Messege를 전송합니다.

 #### 　② 일정, 규칙 수정 데이터 흐름도
 <img width="4484" height="2060" alt="Image" src="https://github.com/user-attachments/assets/ae379c23-faad-4858-a555-342a65c202d7" />
 
 - 수정하려는 데이터를 입력하고 수정 버튼을 누르면 ViewModel에서 데이터를 기반으로 Instance를 수정하거나 생성합니다.
 - ScheduleData, RoutineRecord 수정이거나 RoutineData의 Title, Content만 수정이면<br/>
 　ITodoRepository의 AddOrUpdateData_AsyncSave를 호출해 데이터를 수정합니다.([3-2. ① 데이터 추가, 수정](#data-add-update)의 로직대로 동작)
 - RoutineData의 중요 정보 수정인경우 ITodoRepository의 UpdateRoutineData_AsyncSave를 호출해 데이터를 수정합니다.
 - UpdateRoutineData_AsyncSave는 기존 RoutineData는 RemoveData로 삭제 처리, 신규 RoutineData는 AddOrUpdate로 등록 처리하고 Json 비동기 저장을 한번 실행합니다.
 - 처리가 끝나면 ViewModel에선 Messenger에 UI 업데이트를 요청하는 Messege를 전송합니다.
 
 #### 　③ 일정, 규칙 삭제 데이터 흐름도
 <img width="3724" height="2164" alt="Image" src="https://github.com/user-attachments/assets/0434a787-092b-4439-aea4-49894fa6fa42" />
 
 - 삭제하려는 Data의 EditTodoWindow를 호출하여 삭제 버튼을 누르면 해당 Data를 저장소에서 제거합니다.
 - ITodoRepository의 RemoveData_AsyncSave에 Instance를 삽입해 호출합니다.([3-2. ② 데이터 삭제](#data-remove)의 로직대로 동작)
 
 ### 3-5. 일정, 규칙 목록 흐름도
 <img width="4404" height="2484" alt="Image" src="https://github.com/user-attachments/assets/20dd293a-b7af-41f5-9aa5-145c4a961e61" />
 
 - 메인 Window의 목록 버튼을 누를 시 ListWindow가 생성됩니다.
 - 체크박스로 데이터를 선택하면 CheckedList에 Data가 등록되고, 해제하면 삭제됩니다.
 - 삭제 버튼을 눌러서 ChecekdList에 들어있는 Data를 삭제합니다.
 - List에있는 Data를 더블클릭하거나 선택한 후 수정 버튼을 누르면 Messenger에게 EditTodoWindow 생성을 요청합니다.

 ### 3-6. 이외의 프로젝트 설계 내용
 #### ① 프로그램 실행시
  - 프로그램 실행 시 Local 저장소에서 ITodoStorage가 읽어와졌을때 LastUpdate가 오늘 이전이면 오늘 이전 날짜들의 일정, 규칙들의 Status를 Failure로 변경하게 설계했습니다.
  - 프로그램 실행 시 WindowService Instance를 생성하고, Messenger의 Window 생성 요청 Message를 구독하게 만들고,
    이후 ViewModel에서 Messenger에게 Window 생성 요청을 전송하면 WindowService에서 Window를 생성하게 설계했습니다.

 #### ② UI 관련
  - Window를 전체화면으로 변경할때 작업 표시줄까지 가리지않게 설계했습니다.

 ### ③ 데이터 저장
  - 데이터를 처리할때 UI가 멈추는 현상을 방지하기위해 async/await을 사용해 데이터를 비동기로 처리하게 설계했습니다.
  - 성능 최적화를 위해 CancellationTokenSource를 사용하여 3초동안 입력이 멈추면 데이터를 저장하게 설계했습니다.
  - 안정성을 위해 데이터 저장시 .tmp 파일로 임시 저장한뒤 원본 파일과 교체하여 데이터 파손을 방지하게 설계했습니다.
  
-----

# 4. 개발 중 어려웠던 부분
 - 문제 발생과 원인 분석, 해결의 자세한 내용은 문서 폴더의 문제 해결.txt에 기술
 
 ### ① WPF ItemsControl의 렌더링 시점에 따른 트리거 오작동 (/View/Calendar/CalendarView.xaml)
  > [문제]<br/>
  CalendarView의 ItemsControl 내부에서 MultiDataTrigger로 AlternationCount를 사용해<br/>
  하위 객체에 부여된 AlternationIndex 값에 따라 데이터를 변경하려 했으나 모든 하위 객체의 AlternationIndex가 0으로 인식됨
  
  > [원인]<br/>
  WPF의 ItemControls 렌더링 생명주기상 MultiDataTrigger의 적용 시점이 AlternationCount를 부여하는 단계보다 빠르기 때문에<br/>
  초기 로드시 AlternationIndex값을 정상적으로 참조하지 못하는것을 확인

  > [해결]<br/>
  MultiDataTrigger는 부모 요소의 속성 변화를 제대로 감지하지 못하기에 동일 계층의 Grid.Tag를 중간 매개체로 활용해<br/>
  Grid.Tag에 AlternationCount값을 바인딩하여 값의 변화를 MultiDataTrigger가 즉시 감지할수있도록 우회
  
 ### ② 파일 미종료로 인한 Json 저장 실패 (/Common/Util/FileHelper.cs)
  > [문제]<br/>
  FileHelper에서 데이터를 Json 형식으로 저장하려했는데, 기존 Json 데이터만 남아있고 변경 사항이 반영되지 않음

  > [원인]<br/>
  LoadJson 과정에서 파일을 읽어온 후 닫지 않아서 다른 프로세스(저장 로직)가 해당 파일에 접근할때 '파일 쓰기 권한' 충돌 발생

  > [해결]<br/>
  using 블록을 이용해 Json 데이터를 읽은 후 확실하게 파일을 종료하여 저장 로직이 접근 권한을 즉시 확보할수 있도록 수정

 ### ③ 메서드 충돌로 인해 'System.InvalidOperationException'(PresentationCore.dll)' 예외 발생
  > [문제]<br/>
  커스텀 타이틀바의 최소화 버튼 클릭시 'System.InvalidOperationException'(PresentationCore.dll)' 발생 및 프로그램 정지

  > [원인]<br/>
  최소화 버튼 클릭시 WindowBehavior.DragMove와 WindowService.Minimize 요청이 동시에 발생하여 윈도우 상태 변경 로직이 충돌함

  > [해결]<br/>
  마우스 Pressed 상태일 때만 DragMove가 동작하도록 방어 코드 작성, WindowBehaivor에도 추가 방어 코드 작성

 ### ④ 프로그램 종료 시 비동기 메서드의 동기적 호출로 인한 데드락 발생 (/App.xaml.cs (OnExit))
  > [문제]<br/>
  프로그램 종료 후에도 프로세스가 소멸되지않고 메모리를 점유함

  > [원인]<br/>
  OnExit에서 비동기 저장 메서드를 GetResult()를 사용하여 호출하니 UI 스레드가 비동기 작업 완료 신호를 기다리는 동안
  <br/>작업은 완료 후 복귀할 UI 스레드가 차단되어있어 서로 무한 대기하는 데드락 발생

  > [해결]<br/>
  Task.Run을 사용해 비동기 작업 자체를 백그라운드 스레드로 위임하여 UI 스레드와의 의존성을 분리함

-----

# 5. 아쉬웠던 점
 ### ① 세부 설계 기획의 부재로 인한 개발 시간 증가
 > 화면 기획서만 작성하고 데이터 흐름, 분기별 로직 등 세부 동작 설계서 없이 개발을 시작했더니 Routine을 설계할때 수정을 정말 많이했다.<br/>
 앞으로 순서도나 핵심이 되는 로직의 방향성 등은 제대로 기획서를 작성해놓고 개발을 시작해야겠다고 느꼈다.<br/>

 ### ② 주석 미작성으로 인한 개발 시간 증가
 > 메서드를 만들고 주석을 제대로 달아놓지 않으니 나중에 수정할때 동작방식을 다시 확인하느라 시간이 오래 걸렸다.</br>
 나중에 주석을 확인해 간단하게 로직을 이해할수있게 주석도 상세하게 잘 달아놓는것도 중요한 것 같다.

 ### ③ 추상화 및 다형성 활용의 미흡
  > 상속과 Interface를 사용해 확장성은 높이고 결합도는 낮춘 설계를 하려했다.<br/>
  그런데 핵심 로직에서 witch문을 사용해 하위 객체의 구체적인 타입을 직접 참조하고있었다.<br/>
  다형성과 여러가지 패턴들을 공부하여 필요한 상황에 적절하게 사용할수있게 만들어야할 것 같다.

-----

# 6. 업데이트 예정
 ### ① RoutineRecord가 많아지면 메모리 점유율이 높아질수있기에 RoutineRecord는 RoutineRecords 폴더를 만들어서 yyyy_MM.json 형태로 월별로 저장하기
  - 이에따라 TodoStorage의 Records에는 현재 표시중인 달의 RoutineRecords만 저장하게 변경하기 (캐시화)
  - ListWindow를 열때 TodoStorage의 Records에 접근하는게 아닌 ListWindow 자체에 모든 Records를 불러오게끔 만들기 (아니면 기간을 선택해서 해당 기간의 Records만 불러오기)
 ### ② RoutineRecord를 수정할땐 시작 날짜, 종료 날짜 안보이게하고 Title, Content, 날짜만 보여주기
  - Schdule 창으로 적용시키거나 새로운 창 만들기

-----
