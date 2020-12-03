using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;

namespace BabyAgeApp.Droid
{
    [Preserve(AllMembers = true)]
    [Application]
    public class ThisApp : Application
    {
        protected ThisApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        [Export("SetBirthday")]
        public void SetBirthday(string birthdayString)
        {
            var birthday = DateTime.ParseExact(birthdayString, "O", CultureInfo.CurrentCulture);

            BirthdayProvider.Date = birthday;
        }

        [Export("SetNow")]
        public void SetNow(string nowString)
        {
            var now = DateTime.ParseExact(nowString, "O", CultureInfo.CurrentCulture);

            DateTimeProvider.Now = now;
        }
    }
}