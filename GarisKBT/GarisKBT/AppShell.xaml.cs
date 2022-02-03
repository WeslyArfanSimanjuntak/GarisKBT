using GarisKBT.ViewModels;
using GarisKBT.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GarisKBT
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(MargaDetailPage), typeof(MargaDetailPage));
            Routing.RegisterRoute(nameof(MargaSilsilahPage), typeof(MargaSilsilahPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
