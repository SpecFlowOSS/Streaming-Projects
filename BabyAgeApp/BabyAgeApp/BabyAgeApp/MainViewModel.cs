using System;

namespace BabyAgeApp
{
    public class MainViewModel
    {
        

        public string DaysOld => TimespanSinceBirth().TotalDays.ToString("#");

        public string WeeksOld => (TimespanSinceBirth().TotalDays / 7d).ToString("#");
        public string MonthsOld => (TimespanSinceBirth().TotalDays / 30).ToString("#");

        private TimeSpan TimespanSinceBirth()
        {
            return DateTimeProvider.Now - BirthdayProvider.Date;
        }
    }
}