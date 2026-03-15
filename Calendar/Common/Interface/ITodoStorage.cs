/*
 * 데이터를 실제로 들고있는 객체가 구현해야하는 Interface
 * 
 * ITodoStorage를 구현하는 객체들은 아래 기능들을 구현해야한다
 * 1.저장소에 접근하여 데이터를 읽어와 들고있어야함
 * 2.현재 데이터 내에서 요청받은 데이터를 검색, 수정, 입력, 삭제하는 기능
 * 3.데이터의 수정, 삭제 등에의해 필요없어진 데이터의 정리
 */
using Calendar.Model.DataClass.TodoEntities;

namespace Calendar.Common.Interface
{
    public interface ITodoStorage
    {
        #region Property
        /// <summary>
        /// 저장소에 등록된 모든 ScheduleData 목록
        /// </summary>
        IReadOnlyList<ScheduleData> ScheduleDatas { get; }
        /// <summary>
        /// 저장소에 등록된 모든 RoutineData 목록
        /// </summary>
        IReadOnlyList<RoutineData> RoutineDatas { get; }
        /// <summary>
        /// 저장소에 등록된 모든 RoutineRecord 목록
        /// </summary>
        IReadOnlyList<RoutineRecord> RoutineRecords { get; }
        #endregion

        #region Method
        /// <summary>
        /// 저장소에 등록된 _lastUpdated 날짜를 확인한 뒤 Status 변경 메서드를 호출합니다.
        /// </summary>
        void CheckLastUpdated();

        /// <summary>
        /// 전달받은 data와 일치하는 타입의 List에 접근해 data.Id와 Id가 일치하는 데이터를 찾아 반환해줍니다.
        /// </summary>
        /// <param name="data">실제 저장소에서 찾고싶은 data</param>
        /// <returns>일치하는 원본 데이터, 없을 경우 null</returns>
        T? FindOriginalData<T>(T data) where T : BaseTodoData;

        /// <summary>
        /// 전달받은 data와 일치하는 타입의 List에 접근해 data가 저장돼있는지 확인하고<br/>
        /// 데이터가 저장돼있으면 해당 data를 수정, 없으면 data를 추가합니다.
        /// </summary>
        /// <param name="data">저장하거나 수정하고싶은 data</param>
        /// <param name="isNewRoutineData">data가 신규로 저장하는 RoutineData일 경우 True, 아니면 입력하지않거나 False</param>
        /// <returns>데이터 추가 혹은 수정 성공 시 True, 실패 시 False</returns>
        bool AddOrUpdateData<T>(T data, bool isNewRoutineData = false) where T : BaseTodoData;

        /// <summary>
        /// 전달받은 data와 일치하는 타입의 List에 접근해 data를 삭제합니다
        /// </summary>
        /// <param name="data">삭제하고싶은 data</param>
        /// <returns>데이터 삭제 성공 시 True, 실패 시 False</returns>
        bool RemoveData<T>(T data) where T : BaseTodoData;
        #endregion
    }
}
