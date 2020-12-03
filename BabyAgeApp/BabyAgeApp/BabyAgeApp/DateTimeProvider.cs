using System;
using System.Collections.Generic;
using System.Text;

namespace BabyAgeApp
{
    public class DateTimeProvider
    {
        private static TimeSpan _difference = TimeSpan.Zero;
        

        public static DateTime Now
        {
            get
            {
                return DateTime.Now + _difference;
            }
            set
            {
                _difference = DateTime.Now - value;
            }
        }
    }
}
