/*
 * Settings.json에 저장될 데이터 구조
 */

using Calendar.Model.Enum;

namespace Calendar.Model
{
    public class AppSettings
    {
        public MinimizeMode MinimizeMode { get; set; } = MinimizeMode.Taskbar;
    }
}
