/*
 * 컴퓨터 실행시 프로그램 자동 시작을 레지스트리에 등록해주는 클래스
 */

/*
 * 컴퓨터 실행시 프로그램 자동 시작을 레지스트리에 등록해주는 클래스
 */
using Microsoft.Win32;

namespace Calendar.Common.Service
{
    public class AutoStartService
    {
        #region Property
        // 윈도우 시작 시 자동으로 실행할 프로그램들의 목록이 담긴 Registery 주소
        private const string RegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        // 레지스트리에 저장될 이름
        private readonly string _appName;
        #endregion

        #region 생성자
        public AutoStartService(string appName)
        {
            _appName = appName;
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 자동 시작 등록 여부 확인
        /// </summary>
        public bool IsAutoStartEnabled()
        {
            // 레지스트리 핸들은 시스템 자원이므로 사용이 끝나면 즉시 닫기위해 using 사용 (자동으로 Dispose를 호출)
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key == null) return false;
                return key.GetValue(_appName) != null;
            }
        }

        /// <summary>
        /// 자동 시작 설정 또는 해제
        /// </summary>
        /// <param name="enable">true면 등록, false면 삭제</param>
        public void SetAutoStart(bool enable)
        {
            // 레지스트리 핸들은 시스템 자원이므로 사용이 끝나면 즉시 닫기위해 using 사용 (자동으로 Dispose를 호출)
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RegistryPath, true))
            {
                if (key == null) return;

                if (enable)
                {
                    // 현재 실행 중인 프로그램(.exe)의 전체 경로를 가져옴
                    string? exePath = Environment.ProcessPath;
                    if (exePath != null)
                    {
                        // 경로에 공백이 있을 수 있으므로 큰따옴표로 감싸서 레지스트리에 데이터 쓰기
                        key.SetValue(_appName, $"\"{exePath}\"");
                    }
                }
                else
                {
                    // 등록된 값이 있으면 삭제
                    if (key.GetValue(_appName) != null)
                    {
                        key.DeleteValue(_appName);
                    }
                }
            }
        }
        #endregion
    }
}
