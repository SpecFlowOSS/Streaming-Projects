using System;
using System.Collections.Generic;
using System.Text;

namespace BabyAgeApp
{
    public class BirthdayProvider : StaticNotifyPropertyChanged
    {
        private static DateTime _date = new DateTime(2020, 08, 18, 12, 56, 0);

        public static DateTime Date
        {
            get => _date;
            set
            {
                _date = value; 
                OnPropertyChanged();
            }
        }
    }
}
