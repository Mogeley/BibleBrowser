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

            Assert.Equal(startDate.Month, 9);
            Assert.Equal(startDate.Year, -1);
        }

    }
}
