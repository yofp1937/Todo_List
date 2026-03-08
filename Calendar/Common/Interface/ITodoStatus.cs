/*
 * 상태 데이터가 필요한 Schedule, Routine에 상태 관련 데이터를 구현하게 강제하는 interface
 * View에서 Status에 쉽게 접근할수있게 bool 변수 추가
 */
using Calendar.Model.Enum;

namespace Calendar.Common.Interface
{
    public interface ITodoStatus
    {
        TodoStatus Status { get; set; }
        bool IsWaiting { get; }
        bool IsCompletion { get; }
        bool IsFailure { get; }
    }
}
