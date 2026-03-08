/*
 * JSON 형식으로 저장할 데이터 틀
 */
using Calendar.Model.DataClass.TodoEntities;

namespace Calendar.Model.DataClass
{
    public class TodoStorage
    {
        public List<ScheduleData> Schedules { get; set; } = new();
        public List<RoutineData> Routines { get; set; } = new();
        public List<RoutineRecord> RoutineRecords { get; set; } = new();
        public DateTime LastUpdated { get; set; } = DateTime.Today.Date;
    }
}
