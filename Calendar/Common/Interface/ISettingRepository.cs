/*
 * 프로그램의 설정 값을 저장하는 저장소의 역할을 하는 Class가 구현해야하는 Interface
 */

/*
 * 프로그램의 설정 값을 저장하는 저장소의 역할을 하는 Class가 구현해야하는 Interface
 */
using Calendar.Model;

namespace Calendar.Common.Interface
{
    public interface ISettingRepository
    {
        /// <summary>
        /// AppSettings 읽어오기
        /// </summary>
        AppSettings GetSettings();

        /// <summary>
        /// 변경된 설정값 저장하기
        /// </summary>
        Task<bool> SaveSettings_AsyncSave(AppSettings settings);
    }
}
