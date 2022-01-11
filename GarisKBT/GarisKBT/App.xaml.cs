using GarisKBT.Data;
using GarisKBT.Services;
using GarisKBT.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT
{
    public partial class App : Application
    {
        public static MargaManager MargaManager { get; private set; }
        public static string error = "";
        public App()
        {
            InitializeComponent();
            MargaManager = new MargaManager(new RestServices());
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
