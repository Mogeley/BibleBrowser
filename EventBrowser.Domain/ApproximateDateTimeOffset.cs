namespace EventBrowser.Domain
{
    public class ApproximateDateTimeOffset
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }

        public ApproximateDateTimeOffset(int years = 0, int months = 0, int days = 0, int hours = 0)
        {
            Years = years;
            Months = months;
            Days = days;
            Hours = hours;
        }
    }

}
