using System;

namespace NgNet.Date
{
    public class RandomHelper
    {
        private DateTime _start;
        private DateTime _end;
        private DateTime _timeStart;
        private DateTime _timeEnd;
        private DateTime _dateStart;
        private DateTime _dateEnd;
        private TimeSpan _timeSpan;

        private System.Random rnd = new System.Random();


        public RandomHelper(DateTime start, DateTime end)
        {
            Reset(start, end);
        }

        /// <summary>
        /// 获取下一个随机Time
        /// </summary>
        /// <returns></returns>
        public DateTime NextTime()
        {
            return _timeStart.AddMilliseconds(_timeSpan.TotalMilliseconds * rnd.NextDouble());
        }

        /// <summary>
        /// 获取下一个随机Date
        /// </summary>
        /// <returns></returns>
        public DateTime NextDate()
        {
            DateTime dt1= _dateStart.AddDays(_timeSpan.TotalDays * rnd.NextDouble());
            return new DateTime(dt1.Year, dt1.Month, dt1.Day);
        }

        /// <summary>
        /// 获取下一个随机DateTime
        /// </summary>
        /// <returns></returns>
        public DateTime Next()
        {
            return _start.AddMilliseconds(_timeSpan.TotalMilliseconds * rnd.NextDouble());
        }

        /// <summary>
        /// 重新设置时间间隔，和单位
        /// </summary>
        /// <param name="start">起始时间</param>
        /// <param name="end">结束时间</param>
        public void Reset(DateTime start, DateTime end)
        {
            DateTime tmp;
            if(start > end)
            {
                tmp = start;
                start = end;
                end = tmp;
            }
            _start = start;
            _end = end;
            _timeStart = new DateTime(1, 1, 1, start.Hour, start.Minute, start.Second, start.Millisecond);
            _timeEnd = new DateTime(1, 1, 1, end.Hour, end.Minute, end.Second, end.Millisecond);
            _dateStart = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            _dateEnd = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);
            _timeSpan = end - start;
        }
    }
}
