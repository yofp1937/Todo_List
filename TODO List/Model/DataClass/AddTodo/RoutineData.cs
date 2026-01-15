/*
 * 규칙 추가에서 사용하는 데이터 타입
 */
using TODO_List.Model.Enum;

namespace TODO_List.Model.DataClass.AddTodo
{
    public class RoutineData : BaseTodoData
    {
        public RoutineType RoutineType { get; set; } // 일간, 주간, 월간, 연간 구분
        public int Frequency { get; set; } // 주기 (n일마다, n주마다)
        public bool IsIndefinite { get; set; } // 기한 없음 체크
        public DateTime? EndDate { get; set; } // 종료 날짜

        // --- 주간, 월간, 연간 리스트 ---

        // 1. 주간: 일~토 (0~6) 중 선택된 요일들
        public List<DayOfWeek>? SelectedDays { get; set; }

        // 2. 월간: 1~31일 중 선택된 날짜들
        public List<int>? SelectedMonthDays { get; set; }

        // 3. 연간: 특정 월/일의 조합 (중복 가능하므로 DateTime의 리스트로 관리)
        // 연도 정보는 무시하고 월/일만 사용
        public List<DateTime>? SelectedYearlyDates { get; set; }
    }
}