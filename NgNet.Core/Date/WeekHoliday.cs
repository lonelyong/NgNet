

namespace NgNet.Date
{
    /// <summary>
    /// 第几个月的第几周的星期几的节日
    /// </summary>
    struct WeekHoliday
    {
        public int Month;
        public int WeekAtMonth;
        public int WeekDay;
        public string HolidayName;

        public WeekHoliday(int month, int weekAtMonth, int weekDay, string name)
        {
            Month = month;
            WeekAtMonth = weekAtMonth;
            WeekDay = weekDay;
            HolidayName = name;
        }
    }
}
