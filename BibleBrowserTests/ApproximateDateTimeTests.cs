using EventBrowser.Domain;
using System;
using Xunit;

namespace BibleBrowserTests
{
    public class ApproximateDateTimeTests : TestBase
    {
        [Fact(DisplayName = "Add Year")]
        public void Add_Year()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddYears(3);

            Assert.Equal(startDate.Year, 3);
        }
        
        [Fact(DisplayName = "Subtract Year")]
        public void Subtract_Year()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddYears(-3);

            Assert.Equal(startDate.Year, -3);
        }

        [Fact(DisplayName = "Add Month")]
        public void Add_Month()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddMonths(2);

            Assert.Equal(startDate.Month, 3);
        }

        [Fact(DisplayName = "Add Months Beyond Year")]
        public void Add_Months_Beyond_Year()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddMonths(14);

            Assert.Equal(startDate.Month, 3);
            Assert.Equal(startDate.Year, 1);
        }

        [Fact(DisplayName = "Subtract Month")]
        public void Subtract_Month()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddMonths(-3);

            Assert.Equal(10, startDate.Month);
            Assert.Equal(-1, startDate.Year);

            startDate.AddMonths(-26);
            Assert.Equal(8, startDate.Month);
            Assert.Equal(-3, startDate.Year);
        }

        [Fact(DisplayName = "Test Range of Months")]
        public void Test_Range_of_Months()
        {
            var baseDate = new ApproximateDateTime();

            int expectedYear = -3;
            byte expectedMonth = 0;

            for (int i = -3 * 12; i <= 3 * 12; i++)
            {
                var date = baseDate.Copy;
                date.AddMonths(i);

                expectedMonth += 1;
                if (expectedMonth > 12)
                {
                    expectedMonth = 1;
                    expectedYear += 1;
                }

                Assert.Equal(expectedMonth, date.Month);
                Assert.Equal(expectedYear, date.Year);
            }
        }

        [Fact(DisplayName = "Add Day")]
        public void Add_Day()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddDays(2);

            Assert.Equal(startDate.Day, 3);
        }

        [Fact(DisplayName = "Subtract Day")]
        public void Subtract_Day()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddDays(-3);

            Assert.Equal(28, startDate.Day);
            Assert.Equal(12, startDate.Month);
            Assert.Equal(-1, startDate.Year);
        }

        [Fact(DisplayName = "Test Range of Days")]
        public void Test_Range_of_Days()
        {
            var baseDate = new ApproximateDateTime();

            int expectedYear = -1;
            byte expectedMonth = 10;
            byte expectedDay = 1;

            for (int i = -3 * 30; i <= 3 * 30; i++)
            {
                var date = baseDate.Copy;
                date.AddDays(i);
                
                Assert.Equal(expectedDay, date.Day);
                Assert.Equal(expectedMonth, date.Month);
                Assert.Equal(expectedYear, date.Year);

                expectedDay += 1;
                if (expectedDay > 30)
                {
                    expectedDay = 1;
                    expectedMonth += 1;
                    if (expectedMonth > 12)
                    {
                        expectedMonth = 1;
                        expectedYear += 1;
                    }
                }
            }
        }
        
        [Fact(DisplayName = "Subtract Hour")]
        public void Subtract_Hour()
        {
            var startDate = new ApproximateDateTime();
            startDate.AddHours(-3);

            Assert.Equal(21, startDate.Hour);
            Assert.Equal(30, startDate.Day);
            Assert.Equal(12, startDate.Month);
            Assert.Equal(-1, startDate.Year);
        }

        [Fact(DisplayName = "Test Range of Hours")]
        public void Test_Range_of_Hours()
        {
            var baseDate = new ApproximateDateTime();
            
            int expectedYear = -1;
            byte expectedMonth = 12;
            byte expectedDay = 28;
            byte expectedHour = 0;

            for (int i = -3 * 24; i <= 3 * 24; i++)
            {
                var date = baseDate.Copy;
                date.AddHours(i);

                Assert.Equal(expectedHour, date.Hour);
                Assert.Equal(expectedDay, date.Day);
                Assert.Equal(expectedMonth, date.Month);
                Assert.Equal(expectedYear, date.Year);

                expectedHour += 1;
                if (expectedHour >= 24)
                {
                    expectedHour = 0;
                    expectedDay += 1;
                    if (expectedDay > 30)
                    {
                        expectedDay = 1;
                        expectedMonth += 1;
                        if (expectedMonth > 12)
                        {
                            expectedMonth = 1;
                            expectedYear += 1;
                        }
                    }
                }
            }
        }
    }
}
