/*
 * ScheduleData, RoutineRecord는 완료, 실패 여부가 필요한데
 * RoutineData는 완료, 실패 여부가 필요 없기때문에 중간에서 상태 여부까지 포함하는 BaseClass가 필요해서 구현
 */
using Calendar.Common.Interface;
using Calendar.Model.Enum;
using System.Text.Json.Serialization;

namespace Calendar.Model.DataClass.TodoEntities
{
    public class BaseTodoDataWithStatus : BaseTodoData, ITodoStatus
    {
        private TodoStatus _status = TodoStatus.Waiting;
        public virtual TodoStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        [JsonIgnore]
        public bool IsWaiting => Status == TodoStatus.Waiting;
        [JsonIgnore]
        public bool IsCompletion => Status == TodoStatus.Completion;
        [JsonIgnore]
        public bool IsFailure => Status == TodoStatus.Failure;

        public void NotifyStatusChanged()
        {
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(IsWaiting));
            OnPropertyChanged(nameof(IsCompletion));
            OnPropertyChanged(nameof(IsFailure));
        }
    }
}