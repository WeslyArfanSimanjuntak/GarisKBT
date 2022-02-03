using GarisKBT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
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
            await _viewModel.SearchItems(searchBar.Text);
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Search = searchBar.Text;
        }
    }
}