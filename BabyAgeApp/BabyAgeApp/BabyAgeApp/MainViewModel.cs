using System;

namespace BabyAgeApp
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public MainViewModel()
        {
            BirthdayProvider.PropertyChanged += ProviderDate_Changed;
            DateTimeProvider.PropertyChanged += ProviderDate_Changed;
        }

        private void ProviderDate_Changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(DaysOld));
            OnPropertyChanged(nameof(WeeksOld));
            OnPropertyChanged(nameof(MonthsOld));
        }

        public string DaysOld => TimespanSinceBirth().TotalDays.ToString("0");

        public string WeeksOld => (TimespanSinceBirth().TotalDays / 7d).ToString("0");
        public string MonthsOld => (TimespanSinceBirth().TotalDays / 30).ToString("0");

        private TimeSpan TimespanSinceBirth()
        {
            return DateTimeProvider.Now - BirthdayProvider.Date;
        }
    }
}