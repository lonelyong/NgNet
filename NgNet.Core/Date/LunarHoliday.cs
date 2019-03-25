using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Date
{
    /// <summary>
    /// 农历
    /// </summary>
    struct LunarHoliday
    {
        public int Month;
        public int Day;
        public int Recess;
        public string HolidayName;

        public LunarHoliday(int month, int day, int recess, string name)
        {
            Month = month;
            Day = day;
            Recess = recess;
            HolidayName = name;
        }
    }
}
