/*
 * 규칙이냐 일정이냐에 따라 데이터 저장이 달라서 Base 생성
 */
namespace TODO_List.Model.DataClass.AddTodo
{
    public class BaseTodoData : BaseViewModel // UI 연동을 위해 BaseViewModel 상속
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // JSON Key용
        public string TodoText { get; set; } = ""; // 할일 제목
        public string? TodoDetail { get; set; } // 할일 상세 내용
        public DateTime StartDate { get; set; } // 시작 날짜
        public bool IsCompleted { get; set; }
    }
}
