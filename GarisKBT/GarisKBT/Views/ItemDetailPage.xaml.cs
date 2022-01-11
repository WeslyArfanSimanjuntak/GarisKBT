using GarisKBT.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GarisKBT.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}