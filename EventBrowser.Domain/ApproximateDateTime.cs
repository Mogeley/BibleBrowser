using System;
using System.Collections.Generic;
using System.Text;

namespace EventBrowser.Domain
{
    /// <summary>
    /// Solar Calendar class which conforms to astronomical constants
    /// </summary>
    public class ApproximateDateTime : IComparable<ApproximateDateTime>
    {
        public int Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hour { get; set; }
        
        /// <summary>
        /// Instantiates TimelineDateTime with Year, Month, Day, and Hour
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        public ApproximateDateTime(int year, byte month, byte day, byte hour)
        {
            Year = year;
            Month = ValidMonth(month);
            Day = ValidDay(day);
            Hour = ValidHour(hour);
        }

        /// <summary>
        /// Instantiates TimelineDateTime with Year, Month, and Day
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public ApproximateDateTime(int year, byte month, byte day)
        {
            Year = year;
            Month = ValidMonth(month);
            Day = ValidDay(day);
            Hour = Timeline.Day.Min;
        }

        /// <summary>
        /// Instantiates TimelineDateTime with Year and Month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public ApproximateDateTime(int year, byte month)
        {
            Year = year;
            Month = ValidMonth(month);
            Day = Timeline.Day.Min;
            Hour = Timeline.Hour.Min;
        }

        /// <summary>
        /// Instantiates TimelineDateTime with Year
        /// </summary>
        /// <param name="year"></param>
        public ApproximateDateTime(int year)
        {
            Year = year;
            Month = Timeline.Month.Min;
            Day = Timeline.Day.Min;
            Hour = Timeline.Hour.Min;
        }

        /// <summary>
        /// Instantiates a minimum TimelineDateTime
        /// </summary>
        public ApproximateDateTime()
        {
            Year = 0;
            Month = Timeline.Month.Min;
            Day = Timeline.Day.Min;
            Hour = Timeline.Hour.Min;
        }

        public bool Equals(ApproximateDateTime date)
        {
            return Year == date.Year
                && Month == date.Month
                && Day == date.Day
                && Hour == date.Hour;
        }

        public ApproximateDateTime Copy =>
            new ApproximateDateTime(Year, Month, Day, Hour);

        public override string ToString()
        {
            return $"{Year}-{Month}-{Day} {Hour}:00:00";
        }

        /// <summary>
        /// Adds an ApproximateDateTimeOffset to the current ApproximateDateTime
        /// </summary>
        /// <param name="offset"></param>
        public ApproximateDateTime AddOffset(ApproximateDateTimeOffset offset)
        {
            if (offset != null)
            {
                AddHours(offset.Hours);
                AddDays(offset.Days);
                AddMonths(offset.Months);
                AddYears(offset.Years);
            }
            return this;
        }

        /// <summary>
        /// Subtracts an ApproximateDateTimeOffset from the current ApproximateDateTime
        /// </summary>
        /// <param name="offset"></param>
        public ApproximateDateTime SubtractOffset(ApproximateDateTimeOffset offset)
        {
            if (offset != null)
            {
                AddHours(-offset.Hours);
                AddDays(-offset.Days);
                AddMonths(-offset.Months);
                AddYears(-offset.Years);
            }
            return this;
        }

        /// <summary>
        /// Add Hours to Hours
        /// </summary>
        /// <param name="hours"></param>
        public void AddHours(int hours)
        {
            Hour = (byte)((Hour + hours) % 24);
            AddDays((Hour + hours) / 24);
        }
        
        /// <summary>
        /// Add days to Days of Month
        /// </summary>
        /// <param name="days"></param>
        public void AddDays(int days)
        {
            Day = (byte)((Day + days) % 30);
            AddMonths((Day + days) / 30);
        }

        /// <summary>
        /// Add Months to Month
        /// </summary>
        /// <param name="months"></param>
        public void AddMonths(int months)
        {
            // TODO: fix how negative values are handled, they do not properly fit to range 1-12...
            Month = (byte)((Month + months) % 12);
            AddYears((Month + months) / 12);
        }

        /// <summary>
        /// Add Years to Year
        /// </summary>
        /// <param name="years"></param>
        public void AddYears(int years)
        {
            Year = Year + years;
        }

        private byte ValidMonth(byte month)
        {
            if (month >= Timeline.Month.Min && month <= Timeline.Month.Max)
                return month;

            throw new Exception($"Day: {month} is outside the valid range of {Timeline.Month.Min}-{Timeline.Month.Max}");
        }

        private byte ValidDay(byte day)
        {
            if (day >= Timeline.Day.Min && day <= Timeline.Day.Max)
                return day;

            throw new Exception($"Day: {day} is outside the valid range of {Timeline.Day.Min}-{Timeline.Day.Max}");
        }

        private byte ValidHour(byte hour)
        {
            if (hour >= Timeline.Hour.Min && hour <= Timeline.Hour.Max)
                return hour;

            throw new Exception($"Hour: {hour} is outside the valid range of {Timeline.Hour.Min}-{Timeline.Hour.Max}");
        }

        public bool GreaterThan(ApproximateDateTime other)
        {
            return CompareTo(other) > 0;
        }

        public bool LessThan(ApproximateDateTime other)
        {
            return CompareTo(other) < 0;
        }

        public int CompareTo(ApproximateDateTime other)
        {
            if (Year > other.Year) return 1;
            else if (Year < other.Year) return -1;
            else if (Month > other.Month) return 1;
            else if (Month < other.Month) return -1;
            else if (Day > other.Day) return 1;
            else if (Day < other.Day) return -1;
            else if (Hour > other.Hour) return 1;
            else if (Hour < other.Hour) return -1;
            return 0;
        }
    }
    
    public class ApproximateDateSpan
    {
        public ApproximateDateTime StartDate { get; set; }
        public ApproximateDateTime EndDate { get; set; }

        public ApproximateDateSpan(ApproximateDateTime startDate, ApproximateDateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            if (StartDate.Year != EndDate.Year)
            {
                return $"{StartDate.Year} - {EndDate.Year}";
            }
            if (StartDate.Month != EndDate.Month)
            {
                return $"{StartDate.Year}/{StartDate.Month} - {EndDate.Year}/{EndDate.Month}";
            }
            if (StartDate.Day != EndDate.Day)
            {
                return $"{StartDate.Year}/{StartDate.Month}/{StartDate.Day} - {EndDate.Year}/{EndDate.Month}/{EndDate.Day}";
            }
            return $"{StartDate.Year}/{StartDate.Month}/{StartDate.Day} {StartDate.Hour}00 - {EndDate.Year}/{EndDate.Month}/{EndDate.Day} {EndDate.Hour}00";
        }
    }

    namespace Timeline
    {
        public static class Year
        {
            public const double DaysPerYear = 365.256363004D;
        }

        public static class Month
        {
            public const byte Min = 1;
            public const byte Max = 13;
        }

        public static class Day
        {
            public const byte Min = 1;
            public const byte Max = 31;
        }

        public static class Hour
        {
            public const byte Min = 0;
            public const byte Max = 23;
        }
    }
    
}
