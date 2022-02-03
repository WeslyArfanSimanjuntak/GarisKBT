using GarisKBT.Models;
using GarisKBT.ViewModels;
using GarisKBT.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void searchBarMarga_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private void searchBarMarga_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}