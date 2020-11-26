using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BabyAgeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage {BindingContext = new MainViewModel()};
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
