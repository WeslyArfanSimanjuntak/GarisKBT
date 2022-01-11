using GarisKBT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GarisKBT.ViewModels
{
    public class MargasViewModel : BaseViewModel
    {
        private Marga _selectedItem;

        public MargasViewModel()
        {
            Title = "Browse";
            MargasViewModels = new ObservableCollection<Marga>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        public Command LoadItemsCommand { get; }
        public ObservableCollection<Marga> MargasViewModels { get; }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                MargasViewModels.Clear();
                var margas = await App.MargaManager.GetTasksAsync();
                foreach (var item in margas)
                {
                    MargasViewModels.Add(item);
                }
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
        public Marga SelectedMarga
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(Marga item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

    }
}
