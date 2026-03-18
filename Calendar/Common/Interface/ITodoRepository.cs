/*
 * ViewModel이 데이터에 접근하기위해 방문하는 객체가 구현해야하는 Interface
 * 
 * ITodoRepository를 구현하는 객체들은 아래의 기능들을 구현해야한다.
 * 1.유효성 검사
 * 2.ITodoStorage에 데이터 변경 요청
 * 3.파일을 Json 형태로 저장
 */
using Calendar.Model.DataClass.TodoEntities;

namespace Calendar.Common.Interface
{
    public interface ITodoRepository
    {
        /// <summary>
        /// ITodoStorage를 반환해줍니다.
        /// </summary>
        /// <returns>TodoStorage 반환</returns>
        ITodoStorage GetTodoStorage();

        /// <summary>
        /// 전달받은 data를 저장소에 추가(동일한 Id를 가진 data가 존재하면 수정)해달라고 ITodoStorage에 요청하고,<br/>
        /// ITodoStorage에서 처리가 끝나면 ITodoStorage의 상태를 Json으로 비동기 저장합니다.
        /// </summary>
        /// <param name="data">저장 혹은 수정할 data</param>
        /// <param name="isNewRoutineData">data가 신규로 저장하는 RoutineData일 경우 True, 아니면 입력하지않거나 False</param>
        /// <returns>데이터 추가 혹은 수정과 Json 저장까지 성공 시 True, 하나라도 실패 시 False</returns>
        Task<bool> AddOrUpdateData_AsyncSave<T>(T data, bool isNewRoutineData = false) where T : BaseTodoData;

        /// <summary>
        /// 전달받은 단일 data를 저장소에서 삭제해달라고 ITodoStorage에 요청하고,<br/>
        /// ITodoStorage에서 처리가 끝나면 ITodoStorage의 상태를 Json으로 비동기 저장합니다.
        /// </summary>
        /// <param name="data">삭제할 단일 data</param>
        /// <returns>데이터 삭제와 Json 저장까지 성공 시 True, 하나라도 실패 시 False</returns>
        Task<bool> RemoveData_AsyncSave<T>(T data) where T : BaseTodoData;

        /// <summary>
        /// 전달받은 여러 data를 저장소에서 삭제해달라고 ITodoStorage에 요청하고,<br/>
        /// ITodoStorage에서 처리가 끝나면 ITodoStorage의 상태를 Json으로 비동기 저장합니다.
        /// </summary>
        /// <param name="data">삭제할 여러 data</param>
        /// <returns>데이터 삭제와 Json 저장까지 성공 시 True, 하나라도 실패 시 False</returns>
        Task<bool> RemoveData_AsyncSave<T>(IEnumerable<T> datas) where T : BaseTodoData;

        /// <summary>
        /// existingData로 전달받은 RoutineData는 ITodoRepository에게 삭제를 요청하고,<br/>
        /// newData로 전달받은 RoutineData는 ITodoRepository에게 추가를 요청한 뒤 ITodoStorage의 상태를 Json으로 비동기 저장합니다.
        /// </summary>
        /// <param name="existingData">삭제를 요청할 RoutineData</param>
        /// <param name="newData">추가를 요청할 RoutineData</param>
        /// <returns>데이터 수정, 추가와 Json 저장까지 성공 시 True, 하나라도 실패 시 False</returns>
        Task<bool> UpdateRoutineData_AsyncSave(RoutineData existingData, RoutineData newData);

        /// <summary>
        /// 프로그램이 종료될때 저장소의 상태 저장을 요청합니다.
        /// </summary>
        void WaitingForSavingData();
    }
}
