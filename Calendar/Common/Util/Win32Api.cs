/*
 * Win32 API와 통신할 때 사용하는 상수와 구조체들을 모아둔 클래스
 * 윈도우 창 전체화할때 작업 표시줄을 제외하기위해 사용
 */


/*
 * Win32 API와 통신할 때 사용하는 상수와 구조체들을 모아둔 클래스
 * 윈도우 창 전체화할때 작업 표시줄을 제외하기위해 사용
 */

using System.Runtime.InteropServices;

namespace Calendar.Common.Util
{
    internal static class Win32Api
    {
        #region Constants & Enums
        public enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000, // null
            MONITOR_DEFAULTTOPRIMARY = 0x00000001, // 주 모니터
            MONITOR_DEFAULTTONEAREST = 0x00000002 // 창과 가장 가까운 모니터
        }
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // 모니터 정보 구조체
        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            public int cbSize; // 메모리 크기
            public RECT rcMonitor; // 모니터의 전체 해상도 (Ex: 1920x1080)
            public RECT rcWork; // 작업 표시줄을 제외한 실제 사용 가능 영역
            public int dwFlags; // 모니터 상태 값(기본 모니터인지 등)
        }
        #endregion

        #region User32.dll Methods
        // user32.dll의 기능을 가져다 사용하겠다고 선언
        // extern: 외부 파일과 연결할때 사용
        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);

        [DllImport("user32.dll")]
        public static extern nint MonitorFromWindow(nint handle, uint flags);
        #endregion
    }
}
