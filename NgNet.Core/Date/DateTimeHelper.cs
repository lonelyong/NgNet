using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Date
{
    public static class DateTimeHelper
    {
        #region static methods
        public static bool IsDateString(string dateString)
        {
            bool _rtn; DateTime _dt;
            _rtn = DateTime.TryParse(dateString, out _dt);
            return _rtn;
        }
        #endregion
    }
}
