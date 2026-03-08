/*
 * 규칙 설정 ViewModel들이 반드시 가져야하는 Interface
 */
namespace Calendar.Common.Interface
{
    public interface IRoutineViewModel
    {
        /// <summary>
        /// 상위 ViewModel에게 View에서 선택된 값들을 List<DayOfWeek> 형식으로 반환<para>
        /// (데이터를 저장하기위해 사용자가 선택한 데이터를 추출)
        /// </summary>
        object GetRoutineData();

        /// <summary>
        /// 상위 ViewModel에서 전달받은 data를 각 ViewModel이 자신에게 맞게끔 변환하여 자신에게 적용시킴<para>
        /// (EditTodoViewModel에서 Window를 생성할때 Storage의 데이터를 Window에 세팅하는 용도)
        /// </summary>
        void SetRoutineData(object data);

        /// <summary>
        /// 사용자가 View에서 필수 데이터를 선택했는지 확인해서 true, false 반환
        /// </summary>
        bool GetEnteredRequireData();
    }
}
