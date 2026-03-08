/*
 * Messenger에서 주고받는 Todo 관련 메세지 정의 클래스
 */
namespace Calendar.Common.Messages
{
    public static class TodoMessages
    {
        /// <summary>
        /// 규칙, 일정 데이터가 변경돼서 관련 UI를 업데이트 해야할때 호출
        /// </summary>
        public class RefreshTodoUI { }
    }
}
