using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WikipediaBatak : ContentPage
    {
        public WikipediaBatak()
        {
            InitializeComponent();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            WebView.GoBack();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            WebView.GoForward();
        }
    }
}