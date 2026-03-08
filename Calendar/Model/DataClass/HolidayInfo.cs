/*
 * 공휴일 정보를 저장하는 구조체
 * 한번 만들어진 이후로는 값이 변하면 안되니 readonly
 */
namespace Calendar.Model.DataClass
{
    public readonly struct HolidayInfo
    {
        public bool IsHoliday { get; }
        public string? HolidayName { get; }

        public HolidayInfo(bool isHoliday, string holidayName)
        {
            IsHoliday = isHoliday;
            HolidayName = holidayName;
        }
    }
}
