using System;
using System.Collections.Generic;
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

        [Export("RaiseToast")]
        public void RaiseToast(Java.Lang.String message)
        {
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}