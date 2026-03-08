/*
 * RoutineData의 실제 날짜별 완료 여부를 해당 클래스에서 관리(실질적인 Routine)
 * 
 * 1. RoutineRecord 하나마다 RoutineInstance 하나씩 생성
 * 2. RoutineRecord에 직접 접근하려면 과거의 데이터에 접근해야함(과거의 데이터만 저장소에 저장됨)
 * 3. 진행중인 RoutineData의 RoutineRecord를 제거하면 Status가 초기화된채로 재생성됨
 * 4. 이미 종료된 RoutineData의 RoutineRecord를 제거하면 복원 불가능(재생성 안되니 제거 가능) 
 */
namespace Calendar.Model.DataClass.TodoEntities
{
    public class RoutineRecord : BaseTodoDataWithStatus
    {
        public Guid ParentRoutineId { get; set; } // 어떤 RoutineData에 대한 기록인지
        public DateTime Date { get; set; } // 날짜

        public RoutineRecord() { }
        /// <summary>
        /// 똑같은 데이터를 가진 복사체를 만들어야할때 사용 (base도 호출하여 부모 생성자도 호출)
        /// </summary>
        protected RoutineRecord(RoutineRecord other) : base(other)
        {
            ParentRoutineId = other.ParentRoutineId;
            Date = other.Date;
        }

        /// <summary>
        /// RoutineData를 기반으로 새로운 RoutineRecord를 생성하는 생성자
        /// </summary>
        public RoutineRecord(RoutineData routineData, DateTime date)
        {
            Id = Guid.NewGuid();
            TodoTitle = routineData.TodoTitle;
            TodoContent = routineData.TodoContent;
            StartDate = routineData.StartDate;

            ParentRoutineId = routineData.Id;
            Date = date;
        }
    }
}
