using GarisKBT.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MargaDetailPage : ContentPage
    {
        public MargaDetailPage()
        {
            InitializeComponent();
            BindingContext = new MargaDetailViewModel();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            WebView.GoBack();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            WebView.GoForward();
        }

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if(e.Status == GestureStatus.Running)
            {

                //SLContainer.Scale = e.Scale;
            }
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            var value = slider.Value;
            //SVContainer.Scale = value / 100;
        }
        //public Bitmap getBitmapFromView(View view)
        //{

        //    Bitmap bitmap = view.GetDrawingCache(true);

        //    byte[] bitmapData;

        //    using (var stream = new MemoryStream())
        //    {
        //        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
        //        bitmapData = stream.ToArray();
        //    }

        //    return bitmapData;
        //}
    }
}