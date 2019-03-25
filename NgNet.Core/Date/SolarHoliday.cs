using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Date
{
    /// <summary>
    /// 阳历
    /// </summary>
    struct SolarHoliday
    {
        public int Month;
        public int Day;
        public int Recess; //假期长度
        public string HolidayName;
        public SolarHoliday(int month, int day, int recess, string name)
        {
            Month = month;
            Day = day;
            Recess = recess;
            HolidayName = name;
        }
    }
}
