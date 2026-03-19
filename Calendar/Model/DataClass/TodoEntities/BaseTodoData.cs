/*
 * 규칙이냐 일정이냐에 따라 데이터 저장이 달라서 Base 생성
 */
namespace Calendar.Model.DataClass.TodoEntities
{
    public abstract class BaseTodoData : BaseDataClass // UI 연동을 위해 BaseDataClass 상속
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // JSON Key용
        public virtual string TodoTitle { get; set; } = ""; // 할일 제목
        public string? TodoContent { get; set; } // 할일 상세 내용
        public DateTime StartDate { get; set; } // 시작 날짜

        // 정렬용 데이터 생성 시간
        public long CreatedTicks { get; protected set; } = DateTime.Now.Ticks;
        // 정렬용 순서값
        public virtual int TypePriority => this switch
        {
            ScheduleData => 0,
            RoutineData => 1,
            RoutineRecord => 2,
            _ => 99
        };

        // SidePanelTasksView와 ListView에서 해당 데이터가 선택됐는지 여부를 판별하기 위한 변수
        private bool _isCheckd;
        // 데이터 저장할때 저장될 필요 없음
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsChecked
        {
            get => _isCheckd;
            set => SetProperty(ref _isCheckd, value);
        }

        protected BaseTodoData() { }
        /// <summary>
        /// 똑같은 데이터를 가진 복사체를 만들어야할때 사용
        /// </summary>
        protected BaseTodoData(BaseTodoData other)
        {
            Id = other.Id;
            TodoTitle = other.TodoTitle;
            TodoContent = other.TodoContent;
            StartDate = other.StartDate;
            CreatedTicks = other.CreatedTicks;
        }
    }
}
