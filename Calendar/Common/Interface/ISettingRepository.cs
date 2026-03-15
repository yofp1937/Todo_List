/*
 * 프로그램의 설정 값을 저장하는 저장소의 역할을 하는 Class가 구현해야하는 Interface
 */
using Calendar.Model;

namespace Calendar.Common.Interface
{
    public interface ISettingRepository
    {
        /// <summary>
        /// AppSettings를 반환해줍니다.
        /// </summary>
        AppSettings GetSettings();

        /// <summary>
        /// 변경된 AppSettings를 저장합니다.
        /// </summary>
        Task<bool> SaveSettings_AsyncSave(AppSettings settings);
    }
}
