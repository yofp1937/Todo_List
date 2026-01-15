/*
 * 공휴일 지정해놓는 클래스
 */
namespace TODO_List.Model
{
    public static class HolidayProvider
    {
        #region Property
        // 필요한 만큼 공휴일 추가하면 됨
        public static HashSet<DateTime> Holidays { get; private set; }
        #endregion

        #region 생성자
        static HolidayProvider()
        {
            Holidays = new HashSet<DateTime>()
            {
                new DateTime(2025, 1, 1), // 새해
                new DateTime(2025, 3, 1), // 삼일절
                new DateTime(2025, 5, 5), // 어린이날
                new DateTime(2025, 6, 6), // 현충일
                new DateTime(2025, 8, 15), // 광복절
                new DateTime(2025, 10, 3), // 개천절
                new DateTime(2025, 10, 9), // 한글날
                new DateTime(2025, 12, 25), // 크리스마스

                /* 음력 날짜 국가지정 공휴일
                 * 음력 12월 말일~1월2일       -   설날
                 * 음력 4월 8일                     -   부처님 오신날(석가탄신일)
                 * 음력 8월 14일~16일            -   추석
                 */

            };
        }
        #endregion

        #region 메서드
        public static bool IsHoliday(DateTime date)
        {
            return Holidays.Contains(date);
        }
        #endregion
    }
}
