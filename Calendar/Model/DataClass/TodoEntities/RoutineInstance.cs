/*
 * RoutineRecord를 상속받아 달력 UI에 표시하기 위한 클래스
 * 
 * RoutineData를 참조하여 RoutineData를 수정해야할때 Storage.Routines에서 검색하는 대신
 * ParentRoutineData를 통해 연결되게 만들어서 조회 성능을 최적화
 * 
 * RoutineRecord를 상속받는 이유는 RoutineData, RoutineRecord를 따로 들고있을시
 * View에서 Routine에 접근할땐 Record.TodoTitle 이런식으로 Record를 한번 타고와서 데이터를 찾아야하는데
 * Schedule에 접근할땐 TodoTitle 이렇게 바로 타고들어올수있어서
 * 이런 View에서의 접근을 최적하하기위해 RoutineRecord를 상속하게 만듦
 */
using Calendar.Model.Enum;

namespace Calendar.Model.DataClass.TodoEntities
{
    public class RoutineInstance : BaseTodoDataWithStatus
    {
        public RoutineData? RoutineData { get; }
        public RoutineRecord RoutineRecord { get; }

        public RoutineInstance(RoutineData? data, RoutineRecord record)
        {
            RoutineData = data;
            RoutineRecord = record;
        }

        // View에서의 접근성을 높이는 Proxy 속성
        public override string TodoTitle => RoutineRecord.TodoTitle;
        public override int TypePriority => RoutineRecord.TypePriority;
        public override TodoStatus Status
        {
            get => RoutineRecord.Status;
            set
            {
                if (RoutineRecord.Status != value)
                {
                    RoutineRecord.Status = value;
                    NotifyStatusChanged();
                }
            }
        }
    }
}