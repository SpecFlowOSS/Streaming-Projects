using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BabyAgeApp.Annotations;

namespace BabyAgeApp
{
    public class StaticNotifyPropertyChanged
    {
        public static event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected static void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
