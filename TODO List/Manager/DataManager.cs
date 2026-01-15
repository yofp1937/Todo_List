/*
 * 싱글톤 패턴으로 프로그램의 데이터 전체 관리
 */
using TODO_List.Common.Util;
using TODO_List.Model.DataClass.AddTodo;

namespace TODO_List.Manager
{
    public class DataManager
    {
        // -- 싱글톤 패턴 설정 --
        private static DataManager? _instance;
        public static DataManager Instance => _instance ??= new DataManager();

        // -- 할 일, 규칙 데이터 파일 이름
        private const string FileName = "TodoData.json";

        // 프로그램 실행 중 메모리에 들고 있을 데이터 객체
        public TodoStorage CurrentStorage { get; private set; }

        private DataManager()
        {
            // 초기화 시 파일에서 데이터를 읽어옵니다.
            LoadAllData();
        }

        /// <summary>
        /// 파일에서 데이터를 불러와 메모리에 적재
        /// </summary>
        public void LoadAllData()
        {
            var data = FileHelper.LoadJson<TodoStorage>(FileName);
            // 파일이 없으면 새 객체를 생성합니다.
            CurrentStorage = data ?? new TodoStorage();
        }

        /// <summary>
        /// 현재 메모리의 데이터를 파일로 저장
        /// </summary>
        public void SaveAllData()
        {
            FileHelper.SaveJson(FileName, CurrentStorage);
        }

        /// <summary>
        /// 새로운 일정을 추가하고 저장
        /// </summary>
        public void AddSchedule(ScheduleData schedule)
        {
            CurrentStorage.Schedules.Add(schedule);
            SaveAllData();
        }

        /// <summary>
        /// 새로운 규칙(루틴)을 추가하고 저장
        /// </summary>
        public void AddRoutine(RoutineData routine)
        {
            CurrentStorage.Routines.Add(routine);
            SaveAllData();
        }
    }
}
