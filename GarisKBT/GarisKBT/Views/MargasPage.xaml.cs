using GarisKBT.Models;
using GarisKBT.ViewModels;
using Plugin.Toast;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MargasPage : ContentPage
    {
        MargasViewModel _viewModel;
        public MargasPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MargasViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        public async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            _viewModel.Search = searchBar.Text;
            _viewModel.IsSearch = true;
            //DisplayAlert(,"Data Not Found");
            ;
            await _viewModel.SearchItems(searchBar.Text);
            if (_viewModel.Margas.Count == 0)
            {
                await DisplayAlert("Info", "Marga Tidak Ditemukan", "Ok");
                //CrossToastPopUp.Current.ShowToastError("Marga Tidak Ditemukan");
            }
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Search = searchBar.Text;
        }
    }
}