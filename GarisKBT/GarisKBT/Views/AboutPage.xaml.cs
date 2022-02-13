using GarisKBT.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel _viewModel;
        public AboutPage()
        {
            BindingContext = _viewModel = new AboutViewModel();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ShowWV();
        }
        public async void ShowWV()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                var url = await App.MargaManager.GetGuidanceLink("YoutubeGuide");
                //var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                //// Orientation (Landscape, Portrait, Square, Unknown)
                //var orientation = mainDisplayInfo.Orientation;

                //// Rotation (0, 90, 180, 270)
                //var rotation = mainDisplayInfo.Rotation;

                //// Width (in pixels)
                //var width = mainDisplayInfo.Width;

                //// Width (in xamarin.forms units)
                //var xamarinWidth = width / mainDisplayInfo.Density;

                //// Height (in pixels)
                //var height = mainDisplayInfo.Height;

                //// Screen density
                //var density = mainDisplayInfo.Density;
                //this.WV.Width = mainDisplayInfo.Width;
                this.WV.Source = url;
                this.WV.IsVisible = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}