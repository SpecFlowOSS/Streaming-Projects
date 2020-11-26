using System;

namespace BabyAgeApp
{
    public class MainViewModel
    {
        private readonly DateTime _birthdayOfBaby = new DateTime(2020, 08, 18, 12, 56, 0);

        public string DaysOld => TimespanSinceBirth().TotalDays.ToString("#");

        public string WeeksOld => (TimespanSinceBirth().TotalDays / 7d).ToString("#");
        public string MonthsOld => (TimespanSinceBirth().TotalDays / 30).ToString("#");

        private TimeSpan TimespanSinceBirth()
        {
            return DateTime.Now - _birthdayOfBaby;
        }
    }
}