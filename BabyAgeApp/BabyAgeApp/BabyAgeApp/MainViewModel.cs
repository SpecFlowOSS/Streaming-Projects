using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BabyAgeApp
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public MainViewModel()
        {
            //BirthdayProvider.PropertyChanged += ProviderDate_Changed;
            //DateTimeProvider.PropertyChanged += ProviderDate_Changed;

            RefreshCommand = new Command(RefreshUI);
        }

        private void ProviderDate_Changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            OnPropertyChanged(nameof(SecondsOld));
            OnPropertyChanged(nameof(MinutesOld));
            OnPropertyChanged(nameof(HoursOld));
            OnPropertyChanged(nameof(DaysOld));
            OnPropertyChanged(nameof(WeeksOld));
            OnPropertyChanged(nameof(MonthsOld));
        }

        public string DaysOld => TimespanSinceBirth().TotalDays.ToString("0");

        public string WeeksOld => (TimespanSinceBirth().TotalDays / 7d).ToString("0");
        public string MonthsOld => (TimespanSinceBirth().TotalDays / 30).ToString("0");

        public string HoursOld => (TimespanSinceBirth().TotalHours).ToString("0");
        public string MinutesOld => (TimespanSinceBirth().TotalMinutes).ToString("0");
        public string SecondsOld => (TimespanSinceBirth().TotalSeconds).ToString("0");

        public ICommand RefreshCommand { get; }

        private TimeSpan TimespanSinceBirth()
        {
            return DateTimeProvider.Now - BirthdayProvider.Date;
        }
    }
}