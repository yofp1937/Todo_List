/*
 * JSON 형식으로 저장할 데이터 틀
 */
namespace TODO_List.Model.DataClass.AddTodo
{
    public class TodoStorage
    {
        public List<ScheduleData> Schedules { get; set; } = new();
        public List<RoutineData> Routines { get; set; } = new();
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
