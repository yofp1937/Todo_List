/*
 * Messenger에서 주고받는 Data 처리 관련 메세지
 */
namespace Calendar.Common.Messages
{
    public static class DataMessages
    {
        /// <summary>
        /// Messenger를 통해 3초 후 저장 실행을 DataManager가 호출하게 전달하고싶을떄 사용하는 Message
        /// </summary>
        public class SaveDataAfter3Seconds { }
    }
}
