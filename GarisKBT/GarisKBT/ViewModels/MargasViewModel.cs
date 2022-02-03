using GarisKBT.Models;
using GarisKBT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GarisKBT.ViewModels
{
    public class MargasViewModel : BaseViewModel
    {
        private Marga _selectedItem;
        private bool isSearch;
        private string search;
        public MargasViewModel()
        {
            Title = "Marga Batak Toba dan Keturunan Si Raja Batak";
            Margas = new ObservableCollection<Marga>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Marga>(OnItemSelected);
            ItemSelectedSilsilah = new Command<Marga>(OnItemSelectedSilsilah);
        }

        public bool IsSearch
        {
            get { return isSearch; }
            set { SetProperty(ref isSearch, value); }
        }
        public Command<Marga> ItemTapped { get; }
        public Command<Marga> ItemSelectedSilsilah { get; }
        public Command LoadItemsCommand { get; }
        public ObservableCollection<Marga> Margas { get; }
        public string Search { get => search; set => search = value; }

        public Marga SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
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
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }


        public async Task SearchItems(string text)
        {
            IsBusy = true;
            IsSearch = true;
            try
            {
                Margas.Clear();
                var margas = await App.MargaManager.GetTasksAsync();
                var searched = margas.Where(x => x.Name.ToLowerInvariant().Contains(text.ToLowerInvariant()));
                foreach (var item in searched)
                {
                    Margas.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsSearch = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (!isSearch)
                {
                    Margas.Clear();
                    var margas = await App.MargaManager.GetTasksAsync();

                    if (!string.IsNullOrEmpty(Search))
                    {
                        foreach (var item in margas.Where(x => x.Name.ToLowerInvariant().Contains(Search.ToLowerInvariant())))
                        {
                            Margas.Add(item);
                        }
                    }
                    else
                    {
                        foreach (var item in margas)
                        {
                            Margas.Add(item);
                        }
                    }
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
        async void OnItemSelected(Marga item)
        {
            if (item == null)
                return;

            //This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MargaDetailPage)}?{nameof(MargaDetailViewModel.ItemId)}={item.Id}");
        }
        async void OnItemSelectedSilsilah(Marga item)
        {
            if (item == null)
                return;

            //This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MargaSilsilahPage)}?{nameof(MargaDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
