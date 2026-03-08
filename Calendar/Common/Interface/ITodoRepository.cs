/*
 * 일정, 규칙을 저장하는 저장소의 역할을 하는 Class들이 구현해야하는 Interface
 */
using Calendar.Model.DataClass;
using Calendar.Model.DataClass.TodoEntities;

namespace Calendar.Common.Interface
{
    public interface ITodoRepository
    {
        /// <summary>
        /// 데이터 저장 및 업데이트 (성공 여부를 bool로 반환)
        /// </summary>
        Task<bool> AddOrUpdateData_AsyncSave<T>(T data) where T : class;

        /// <summary>
        /// 데이터 제거
        /// </summary>
        Task<bool> DeleteData_AsyncSave<T>(T data) where T : class;

        /// <summary>
        /// routineData에 변경이 생겼을때 Status값이 Waiting인(저장소에 저장된 미래 Record) Record 전부 삭제
        /// </summary>
        void DeleteGarbageRecordsInStorage(RoutineData routineData);

        /// <summary>
        /// 현재 저장소를 보여줌
        /// </summary>
        TodoStorage GetTodoStorage();

        /// <summary>
        /// 3초 뒤 데이터 저장
        /// </summary>
        void RequestSaveAfter3Seconds();

        /// <summary>
        /// 비정상 종료일때 데이터 저장 대기
        /// </summary>
        void WaitingForSavingData();
    }
}
