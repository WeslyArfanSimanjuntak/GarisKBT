using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace GarisKBT.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class MargaDetailViewModel : BaseViewModel
    {
        private int itemId;
        private int id;
        private string name;
        private string nim;
        private int? superiorId;
        private string wikipediaLink;
        public int Id { get => id; set => id = value; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Nim
        {
            get => nim;
            set => SetProperty(ref nim, value);
        }
        public int? SuperiorId
        {
            get => superiorId;
            set => SetProperty(ref superiorId, value);
        }
        public string WikipediaLink
        {
            get => wikipediaLink;
            set => SetProperty(ref wikipediaLink, value);
        }
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadId(value);
            }
        }
        public async void LoadId(int id)
        {
            try
            {
                var marga = await App.MargaManager.GetMargaDetailAsync(id);
                Id = marga.Id;
                Name = marga.Name;
                SuperiorId = marga.SuperiorId;
                Nim = marga.Nim;
                WikipediaLink = marga.WikipediaLink;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
