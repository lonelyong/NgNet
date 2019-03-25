using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet
{
    public class RandomHelper : Random 
    {
        #region constructor
        public RandomHelper(int seed) : base(seed)
        {
             
        }

        public RandomHelper() : base()
        {
         
        }
        #endregion

        #region public methods
        public DateTime NextDate(DateTime minDate, DateTime maxDate)
        {
            DateTime dt1 = new DateTime(minDate.Year, minDate.Month, minDate.Day, 0, 0, 0, 0);
            DateTime dt2 = new DateTime(maxDate.Year, maxDate.Month, maxDate.Day, 0, 0, 0, 0);
            if (dt1 > dt2)
                throw new Exception(string.Format("{0}的Date部分不能大于{1}Date部分", "mimDate", "maxDate"));
            TimeSpan ts = dt2 - dt1;
            dt1 = dt1.AddDays(NextDouble() * ts.TotalDays);
            return new DateTime(dt1.Year, dt1.Month, dt1.Day);
        }

        public DateTime NextTime()
        {
            return new DateTime(1, 1, 1, NextHour(), NextMinute(), NextSecond(), NextMillisecond());
        }

        public DateTime NextTime(DateTime minTime, DateTime maxTime)
        {
            DateTime dt1 = new DateTime(1, 1, 1, minTime.Hour, minTime.Minute, minTime.Second, minTime.Millisecond);
            DateTime dt2 = new DateTime(1, 1, 1, maxTime.Hour, maxTime.Minute, maxTime.Second, maxTime.Millisecond);
            TimeSpan ts = dt2 - dt1;
            if (dt1 > dt2)
                throw new Exception(string.Format("minDate 的Time部分不能大于 maxDate的Time部分"));
            return minTime.AddMilliseconds(ts.TotalMilliseconds * NextDouble());

            #region oldmethod
            /*
            int hour = Next(minTime.Hour, maxTime.Hour + 1);
            int minute = NextMinute();
            int second = NextSecond();
            int millisecond = NextMillisecond();

            if(minTime.Hour == maxTime.Hour)
            {
                if (minTime.Minute == maxTime.Minute)
                {
                    minute = minTime.Minute;
                    if (minTime.Second == maxTime.Second)
                    {
                        second = minTime.Second;
                        if (minTime.Millisecond == maxTime.Millisecond)
                            millisecond = minTime.Millisecond;
                        else
                            millisecond = Next(minTime.Millisecond, maxTime.Millisecond);
                    }
                    else
                        second = Next(minTime.Second, maxTime.Second + 1);
                }
                else 
                    minute = Next(minTime.Minute, maxTime.Minute + 1);
            }
            if (hour == maxTime.Hour && minute < minTime.Minute)
                minute = Next(minTime.Minute, 60);
            if (hour == minTime.Hour && minute == minTime.Minute && second < minTime.Second)
                second = Next(minTime.Second, 60);
            if (hour == minTime.Hour && minute == minTime.Minute && second == minTime.Second && millisecond < minTime.Millisecond)
                millisecond = Next(minTime.Millisecond, 1000);
           
            if (hour == maxTime.Hour && minute > maxTime.Minute)
                minute = Next(0, maxTime.Minute + 1);
            if (hour == maxTime.Hour && minute == maxTime.Minute && second > maxTime.Second)
                second = Next(0, maxTime.Second + 1);
            if (hour == maxTime.Hour && minute == maxTime.Minute && second == maxTime.Second && millisecond > maxTime.Millisecond)
                millisecond = Next(0, maxTime.Millisecond);
            return new DateTime(1, 1, 1, hour, minute, second, millisecond);
            */
            #endregion
        }

        public DateTime NextDateTime(DateTime minDateTime, DateTime maxDateTime)
        {
            if (minDateTime > maxDateTime)
                throw new Exception(string.Format("minDateTime 不能大于 maxDateTime"));

            TimeSpan ts = maxDateTime - minDateTime;

            return minDateTime.AddMilliseconds(ts.TotalMilliseconds * NextDouble());
            #region oldmethod
            /* 
            int year = Next(minDateTime.Year, maxDateTime.Year  + 1);
            int month = NextMonth();
            int day = NextDay(year, month);
            int hour = NextHour();
            int minute = NextMinute();
            int second = NextSecond();
            int millisecond = NextMillisecond();

            #region 判断相等情况
            if (minDateTime.Year == maxDateTime.Year)
            {
                year = minDateTime.Year;
                if (minDateTime.Month == maxDateTime.Month)
                {
                    month = minDateTime.Month;
                    if (minDateTime.Day == maxDateTime.Day)
                    {
                        day = minDateTime.Day;
                        if (minDateTime.Hour == maxDateTime.Hour)
                        {
                            hour = minDateTime.Hour;
                            if (minDateTime.Minute == maxDateTime.Minute)
                            {
                                minute = minDateTime.Minute;
                                if (minDateTime.Second == maxDateTime.Second)
                                {
                                    second = minDateTime.Second;
                                    if (minDateTime.Millisecond == minDateTime.Millisecond)
                                    {
                                        millisecond = minDateTime.Millisecond;
                                    }
                                    else
                                        millisecond = Next(minDateTime.Millisecond, maxDateTime.Millisecond);
                                }
                                else
                                    second = Next(minDateTime.Second, maxDateTime.Second + 1);
                            }
                            else
                                minute = Next(minDateTime.Minute, maxDateTime.Minute + 1);
                        }
                        else
                            hour = Next(minDateTime.Hour, maxDateTime.Hour + 1);
                    }
                    else
                        day = Next(minDateTime.Day, maxDateTime.Day + 1);
                }
                else
                    month = Next(minDateTime.Month, maxDateTime.Month + 1);
            }
            else
                year = Next(minDateTime.Year, maxDateTime.Year + 1);
            #endregion

            #region 判断是否超出范围
            if (year == minDateTime.Year && month < minDateTime.Month)
                month = Next(minDateTime.Month, 13);
            if (year == minDateTime.Year && month == minDateTime.Month && day < minDateTime.Day)
                day = Next(minDateTime.Day, DateTime.DaysInMonth(year, month) + 1);
            if (year == minDateTime.Year && month == minDateTime.Month && day == minDateTime.Day)
            {
                if (hour < minDateTime.Hour)
                    hour = Next(minDateTime.Hour, 24);
                if (hour == minDateTime.Hour && minute < minDateTime.Minute)
                    minute = Next(minDateTime.Minute, 60);
                if (hour == minDateTime.Hour && minute == minDateTime.Minute && second < minDateTime.Second)
                    second = Next(minDateTime.Second, 60);
                if (hour == minDateTime.Hour && minute == minDateTime.Minute && second == minDateTime.Second)
                    millisecond = Next(minDateTime.Millisecond, 1000);
            }
            if (year == maxDateTime.Year && month > maxDateTime.Month)
                month = Next(1, maxDateTime.Month + 1);
            if (year == maxDateTime.Year && month == maxDateTime.Month && day > maxDateTime.Day)
                day = Next(1, maxDateTime.Day + 1);
            if (year == maxDateTime.Year && month == maxDateTime.Month && day == maxDateTime.Day)
            {
                if (hour > maxDateTime.Hour)
                    hour = Next(0, maxDateTime.Hour);
                if (hour == maxDateTime.Hour && minute > maxDateTime.Minute)
                    minute = Next(1, maxDateTime.Minute);
                if (hour == maxDateTime.Hour && minute == maxDateTime.Minute && second < maxDateTime.Second)
                    second = Next(1, maxDateTime.Second);
                if (hour == maxDateTime.Hour && minute == maxDateTime.Minute && second == maxDateTime.Second)
                    millisecond = Next(1, maxDateTime.Millisecond);
            }
            #endregion

            //return $"{year} - {month} - {day}  {hour}:{minute}:{second}:{millisecond}";
           return new DateTime(year, month, day, hour, minute, second, millisecond);
           */
            #endregion
        }

        public int NextYear(int minYear, int maxYear)
        {
            if (minYear < maxYear)
                throw new Exception(string.Format("minYear 不能大于 maxYear"));
            return Next(minYear, maxYear);
        }
       
        public int NextMonth()
        {
            return Next(1, 12);
        }

        public int NextWeekDay()
        {
            return Next(1, 7);
        }

        public int NextDay(int maxDays)
        {
            if (maxDays != 31 && maxDays != 30 && maxDays != 29 && maxDays != 28)
                throw new ArgumentOutOfRangeException(string.Format("参数 \"{0}\" 超出范围，正确的参数值为: 28,29,30,31"), "maxDays");
            return Next(1, maxDays + 1);
        }

        public int NextDay(int year, int month)
        {
            var daysInMouth = DateTime.DaysInMonth(year, month);
            return Next(1, daysInMouth);
        }

        public int NextHour()
        {
            return Next(0, 24);
        }

        public int NextMinute()
        {
            return Next(0, 60);
        }

        public int NextSecond()
        {
            return Next(0, 60);
        }

        public int NextMillisecond()
        {
            return Next(0, 1000);
        }

        public System.Drawing.Point NextPoint(int width, int height)
        {
            return new System.Drawing.Point(Next(0, width), Next(0, height));
        }

        public System.Drawing.Color NextColor()
        {
            return System.Drawing.Color.FromArgb(Next());
        }

        public char NextFigure()
        {
            return Text.Consts.BaseFigures[Next(0, 10)];
        }

        public char NextLetter()
        {
            return Text.Consts.EnglishLetters[Next(0, 26)];
        }

        public char NextChineseLetter()
        {
            return Text.Consts.ChineseFigures[Next(0, 10)];
        }

        public char NextCapitalChineseLetter() 
        {
            return Text.Consts.CapitalChineseFigures[Next(0, 10)];
        }
        #endregion
    }
}
