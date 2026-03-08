/*
 * 공휴일 지정해놓는 클래스
 */
using Calendar.Model.DataClass;
using System.Globalization;

namespace Calendar.Model
{
    public static class HolidayProvider
    {
        #region Property
        private static KoreanLunisolarCalendar _lunarCalendar = new();
        // App.xaml.cs에서 Dictionary를 생성중일때 Calendar에서 Dictionary에 접근할때 오류 방지책
        private static readonly object _lock = new object();
        // 대체 공휴일 적용 날짜 (Holidays)
        private static readonly Dictionary<(int Month, int Day), string> Holidays = new()
        {
            { (3, 1), "삼일절" }, { (5, 5), "어린이날" }, { (8, 15), "광복절" }, { (10, 3), "개천절" }, { (10, 9), "한글날" }, { (12, 25), "성탄절" }
        };
        // 대체 공휴일 미적용 날짜 (NotAppliedHolidays)
        private static readonly Dictionary<(int Month, int Day), string> NotAppliedHolidays = new()
        {
            { (1, 1), "신정" }, { (6, 6), "현충일" }
        };

        // 미리 연도별 공휴일을 계산하여 저장해놓을 Dictionary
        private static Dictionary<DateTime, string> _cachedHolidays = new();
        // 어떤 연도가 계산됐는지 저장해둘 List
        private static List<int> _cachedYear = new();
        #endregion

        public static HolidayInfo GetHolidayInfo(DateTime date)
        {
            lock (_lock)
            {
                // 조회하는 연도의 데이터가 준비되지 않았으면 생성하여 받아오기
                if (!_cachedYear.Contains(date.Year))
                    InitTargetYaerHolidays(date.Year);

                if (_cachedHolidays.TryGetValue(date.Date, out string? holidayName))
                    return new HolidayInfo(true, holidayName);
            }
            return new HolidayInfo(false, string.Empty);
        }

        /// <summary>
        /// Calendar의 연도가 바뀌었을 경우 해당 연도의 공휴일을 미리 계산해놓기위한 메서드
        /// </summary>
        public static void InitTargetYaerHolidays(int year)
        {
            lock (_lock)
            {
                // 이미 계산된 연도면 스킵
                if (_cachedYear.Contains(year)) return;

                // 1. 양력 공휴일 등록
                RegisterSolarHolidayOnDict(year);
                // 2. 음력 공휴일 등록
                RegisterLunarHolidayOnDict(year);
                // 3. 대체 공휴일 등록
                RegisterReplacedHolidayOnDict(year);

                _cachedYear.Add(year);
            }
        }

        /// <summary>
        /// 양력 공휴일을 _cachedHolidays에 등록하는 메서드
        /// </summary>
        private static void RegisterSolarHolidayOnDict(int year)
        {
            foreach (var h in Holidays)
                _cachedHolidays[new DateTime(year, h.Key.Month, h.Key.Day)] = h.Value;
            foreach (var h in NotAppliedHolidays)
                _cachedHolidays[new DateTime(year, h.Key.Month, h.Key.Day)] = h.Value;
        }

        /// <summary>
        /// 음력 공휴일을 _cachedHolidays에 등록하는 메서드
        /// </summary>
        private static void RegisterLunarHolidayOnDict(int year)
        {
            // RegisterLunarHolidayOnDict 내부에서만 쓰이는 메서드
            void AddOrMerge(DateTime date, string name)
            {
                // 만약 해당 날짜에 이미 공휴일이 존재하면 줄바꿈 기호를 넣고 이름을 합침
                if (_cachedHolidays.TryGetValue(date, out string? outName))
                {
                    // 이름이 똑같으면(설날, 추석) 이름 합치기 패스
                    if (outName == name) return;
                    _cachedHolidays[date] += $"\n{name}";
                }
                else
                    _cachedHolidays[date] = name;
            }
            // 설날 (음력 12월 말일 ~ 1월 2일)
            AddOrMerge(GetSolarFromLunar(year, 1, 1).AddDays(-1), "설날");
            AddOrMerge(GetSolarFromLunar(year, 1, 1), "설날");
            AddOrMerge(GetSolarFromLunar(year, 1, 2), "설날");
            // 부처님 오신 날 (음력 4월 8일)
            AddOrMerge(GetSolarFromLunar(year, 4, 8), "부처님 오신 날");
            // 명절 (음력 8월 14일 ~ 16일)
            for (int date = 14; date <= 16; date++)
                AddOrMerge(GetSolarFromLunar(year, 8, date), "추석");
        }

        /// <summary>
        /// 대체 공휴일을 _cachedHolidays에 등록하는 메서드
        /// </summary>
        private static void RegisterReplacedHolidayOnDict(int year)
        {
            // 현재 등록된 year년도의 모든 공휴일을 날짜순으로 정렬해서 가져옴
            List<KeyValuePair<DateTime, string>> holidaysThisYear = _cachedHolidays.Where(x => x.Key.Year == year).OrderBy(x => x.Key).ToList();

            /* 대체 공휴일은 공휴일이 토요일이나 일요일과 겹치면 다음 첫번째 비공휴일을 공휴일로 지정하는것
                설날, 추석은 토요일 제외, 신정과 현충일은 대체공휴일 적용 대상 아님 */
            foreach (KeyValuePair<DateTime, string> holiday in holidaysThisYear)
            {
                DateTime holidayDate = holiday.Key;
                string holidayName = holiday.Value;

                // 신정과 현충일은 대체 공휴일 적용 대상이 아님
                if (holidayName == "신정" || holidayName == "현충일") continue;

                // 대체 공휴일을 생성해야하는지 여부 (true: 생성, false: 미생성)
                bool isReplaced = false;
                // 공휴일이 일요일과 겹치면 대체 공휴일 생성해야함
                if (holidayDate.DayOfWeek == DayOfWeek.Sunday)
                    isReplaced = true;
                // 설날, 추석을 제외한 공휴일이 토요일과 겹치면 대체 공휴일 생성해야함
                else if (holidayDate.DayOfWeek == DayOfWeek.Saturday && holidayName != "설날" && holidayName != "추석")
                    isReplaced = true;
                // 다른 공휴일과 겹치면(글자 내에 줄바꿈 기호가 존재하면) 대체 공휴일 생성해야함
                else if (holidayName.Contains("\n"))
                    isReplaced = true;

                if (isReplaced)
                {
                    AddReplacedHoliday(holidayDate);
                }
            }
        }

        /// <summary>
        /// 음력 날짜를 양력 날짜로 반환해주는 메서드 (음력 4월 8일을 넣으면 양력으로 언제인지 알려줌)
        /// </summary>
        private static DateTime GetSolarFromLunar(int year, int month, int day)
        {
            // 해당 연도의 윤달이 몇월인지 가져옴 (없으면 0)
            int leapMonth = _lunarCalendar.GetLeapMonth(year);

            int actualMonth = month;
            // 윤달이 있고 찾으려는 달이 윤달보다 크거나 같으면 +1해서 윤달 건너뛰게 만듦
            if (leapMonth > 0 && month >= leapMonth)
            {
                actualMonth++;
            }
            return _lunarCalendar.ToDateTime(year, actualMonth, day, 0, 0, 0, 0);
        }

        private static void AddReplacedHoliday(DateTime date)
        {
            // 다음 날짜가 다른 공휴일이거나, 토요일이거나, 일요일일경우 다음날로 이동
            DateTime replacedHoliday = date.AddDays(1);
            while (_cachedHolidays.ContainsKey(replacedHoliday) ||
                     replacedHoliday.DayOfWeek == DayOfWeek.Saturday ||
                     replacedHoliday.DayOfWeek == DayOfWeek.Sunday)
            {
                replacedHoliday = replacedHoliday.AddDays(1);
            }
            _cachedHolidays[replacedHoliday] = "대체 공휴일";
        }
    }
}
