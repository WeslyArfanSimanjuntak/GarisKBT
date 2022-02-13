using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GarisKBT.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            GetLinkCommand = new Command(async () => await GetLink("YoutubeGuide"));
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand GetLinkCommand { get; }
        private string linkGuidance;
        public string LinkGuidance
        {
            get => linkGuidance;
            set => SetProperty(ref linkGuidance, value);
        }
    
        public async Task GetLink(string text)
        {
            IsBusy = true;
            try
            {
                LinkGuidance  = await App.MargaManager.GetGuidanceLink(text);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}