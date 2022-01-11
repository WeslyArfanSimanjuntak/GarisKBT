using GarisKBT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GarisKBT : ContentPage
    {
        private List<Marga> keturunan;
        private DateTime lastInputSearch;
        private List<Marga> marga;
        public GarisKBT()
        {
            InitializeComponent();
            keturunan = new List<Marga>();
            //LoadMargaAsync();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.marga = await App.MargaManager.GetTasksAsync();

            foreach (var item in marga)
            {


                if (item.SuperiorId != null)
                {
                    var orderedMarga = marga.Where(x => x.SuperiorId == item.SuperiorId).OrderBy(x => x.NIM);
                    int count = 0;
                    foreach (var ordered in orderedMarga)
                    {
                        count++;
                        if (ordered.Id == item.Id)
                        {
                            item.NIM = count.ToString();
                        }
                    }

                }
            }
            //foreach (var item in marga)
            //{
            //   var line = marga.Where
            //}

            //this.MargaListView.ItemsSource = this.marga;


            var childPanel = new StackLayout();
            childPanel.Orientation = StackOrientation.Horizontal;
            childPanel.HorizontalOptions = LayoutOptions.Center;
            var button = new Button();
            var raja = marga.FirstOrDefault();
            if (raja != null)
            {
                keturunan.Add(raja);
                button.Text = "1. 1" + raja.Name.ToUpper();
                button.BackgroundColor = Color.LightGreen;
                button.HorizontalOptions = LayoutOptions.End;
                button.Clicked += MargaButton_Clicked;
                button.CommandParameter = raja.Id;
                childPanel.Children.Add(button);
                Panel.Children.Add(childPanel);

            }


        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    var inputMarga = this.InputMarga.Text;

        //    if (inputMarga != null)
        //    {
        //        var search = new Search();
        //        search.JSONString = JsonConvert.SerializeObject(inputMarga);
        //        search.CreatedBy = DeviceInfo.Manufacturer + " - " + DeviceInfo.Model + " - " + DeviceInfo.VersionString;
        //        search.CreatedDate = DateTime.Now;
        //        var searchJson = JsonConvert.SerializeObject(search);
        //        App.MargaManager.SaveSearchDataAsync(searchJson);
        //        var marga = this.marga.Where(x => x.Name.ToLower().ToString().Contains(inputMarga.ToLower())).FirstOrDefault();
        //        keturunan = new List<Marga>();
        //        if (marga != null)
        //        {
        //            keturunan.Add(marga);
        //            while (marga.SuperiorId != null)
        //            {
        //                marga = this.marga.Where(x => x.Id == marga.SuperiorId).FirstOrDefault();
        //                keturunan.Add(marga);
        //            }
        //        }
        //        keturunan.Reverse();
        //        GenerateWithBoxStyle(keturunan, null);
        //    }
        //}
        private void GenerateWithBoxStyle(List<Marga> keturunan, List<Marga> descendents)
        {
            Panel.Children.Clear();
            int counter = 0; var secondCounter = 0;
            foreach (var item in keturunan)
            {
                var childPanel = new StackLayout();
                childPanel.Orientation = StackOrientation.Horizontal;
                childPanel.HorizontalOptions = LayoutOptions.Center;
                counter++;
                secondCounter = 0;
                var saudaraSemua = this.marga.Where(x => x.SuperiorId == item.SuperiorId);
                foreach (var brother in saudaraSemua.OrderBy(x => x.NIM))
                {
                    secondCounter++;
                    var button = new Button();

                    if (brother.Id == item.Id)
                    {
                        button.Text = counter.ToString() + ". " + secondCounter + ". " + brother.Name.ToUpper();
                        button.BackgroundColor = Color.LightGreen;
                        button.HorizontalOptions = LayoutOptions.End;
                        button.Clicked += MargaButton_Clicked;
                        button.CommandParameter = brother.Id;
                    }
                    else
                    {
                        button.Text = counter.ToString() + ". " + secondCounter + ". " + brother.Name.ToUpper();
                        button.HorizontalOptions = LayoutOptions.End;
                        button.Clicked += MargaButton_Clicked;
                        button.CommandParameter = brother.Id;
                    }
                    childPanel.Children.Add(button);
                }

                Panel.Children.Add(childPanel);
            }
            if (descendents != null && descendents.Any())
            {
                counter++;
                var childPanel = new StackLayout();
                childPanel.Orientation = StackOrientation.Horizontal;
                childPanel.HorizontalOptions = LayoutOptions.Center;
                secondCounter = 0;
                foreach (var descendent in descendents.OrderBy(x => x.NIM))
                {
                    secondCounter++;
                    var button = new Button();
                    button.Text = counter.ToString() + ". " + secondCounter + ". " + descendent.Name.ToUpper();
                    button.HorizontalOptions = LayoutOptions.End;
                    button.Clicked += MargaButton_Clicked;
                    button.CommandParameter = descendent.Id;
                    childPanel.Children.Add(button);
                }
                Panel.Children.Add(childPanel);
            }
        }

        private void GenerateWithListViewStyle(List<Marga> keturunan)
        {
            Panel.Children.Clear();
            int counter = 0;
            foreach (var item in keturunan)
            {
                counter++;
                var secondCounter = 0;
                var saudaraSemua = this.marga.Where(x => x.SuperiorId == item.SuperiorId);
                foreach (var brother in saudaraSemua.OrderBy(x => x.NIM))
                {
                    secondCounter++;
                    if (brother.Id == item.Id)
                    {
                        Panel.Children.Add(new Entry()
                        {

                            Text = counter.ToString() + ". " + secondCounter + ". " + brother.Name.ToUpper(),
                            BackgroundColor = Color.LightGreen
                        });
                    }
                    else
                    {
                        Panel.Children.Add(new Entry()
                        {

                            Text = counter.ToString() + ". " + secondCounter + ". " + brother.Name.ToUpper()
                        });
                    }

                }

            }
        }

        //private void InputMarga_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var inputMarga = this.InputMarga.Text;
        //    if (lastInputSearch == null)
        //    {
        //        lastInputSearch = DateTime.Now;
        //    }

        //    if (inputMarga != null)
        //    {
        //        if ((DateTime.Now - lastInputSearch).TotalMilliseconds > 500)
        //        {
        //            var search = new Search();
        //            search.JSONString = JsonConvert.SerializeObject(inputMarga);
        //            search.CreatedBy = DeviceInfo.Manufacturer + " - " + DeviceInfo.Model + " - " + DeviceInfo.VersionString;
        //            search.CreatedDate = DateTime.Now;
        //            var searchJson = JsonConvert.SerializeObject(search);
        //            App.MargaManager.SaveSearchDataAsync(searchJson);
        //        }
        //        this.MargaListView.ItemsSource = this.marga.Where(x => x.Name.ToLower().ToString().Contains(inputMarga.ToLower()));
        //    }
        //}

        private void MargaButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            var selectedId = Convert.ToInt32(button.CommandParameter);
            var marga = this.marga.Where(x => x.Id == selectedId).FirstOrDefault();
            keturunan = new List<Marga>();
            if (marga != null)
            {

                keturunan.Add(marga);
                while (marga.SuperiorId != null)
                {
                    marga = this.marga.Where(x => x.Id == marga.SuperiorId).FirstOrDefault();
                    keturunan.Add(marga);
                }
            }
            keturunan.Reverse();
            var descendants = this.marga.Where(x => x.SuperiorId == selectedId).ToList();
            //foreach (var item in descendants.OrderBy(x => x.NIM))
            //{
            //    keturunan.Add(item);
            //}
            //GenerateWithListViewStyle(keturunan);
            GenerateWithBoxStyle(keturunan, descendants);

        }
        private void MargaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var marga = ((ListView)sender).SelectedItem as Marga;
            //if (marga == null)
            //{
            //    return;
            //}
            //keturunan.Add(marga);
            //while (marga.SuperiorId != null)
            //{
            //    marga = this.marga.Where(x => x.Id == marga.SuperiorId).FirstOrDefault();
            //    keturunan.Add(marga);
            //}
            //keturunan.Reverse();
            //var descendants = this.marga.Where(x => x.SuperiorId == marga.Id).ToList();
            //GenerateWithBoxStyle(keturunan, descendants);
        }

        private void MargaListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.IsChecked)
            {
                if (button.Content.ToString() == "Hirarki")
                {
                    GenerateWithBoxStyle(keturunan, null);
                }
                else
                {
                    GenerateWithListViewStyle(keturunan);
                }
            }
        }

        private void searchBarMarga_SearchButtonPressed(object sender, EventArgs e)
        {
            var inputMarga = this.searchBarMarga.Text;
            if (lastInputSearch == null)
            {
                lastInputSearch = DateTime.Now;
            }

            if (inputMarga != null)
            {
                if ((DateTime.Now - lastInputSearch).TotalMilliseconds > 500)
                {
                    var search = new Search();
                    search.JSONString = JsonConvert.SerializeObject(inputMarga);
                    search.CreatedBy = DeviceInfo.Manufacturer + " - " + DeviceInfo.Model + " - " + DeviceInfo.VersionString;
                    search.CreatedDate = DateTime.Now;
                    var searchJson = JsonConvert.SerializeObject(search);
                    App.MargaManager.SaveSearchDataAsync(searchJson);
                }
                //this.MargaListView.ItemsSource = this.marga.Where(x => x.Name.ToLower().ToString().Contains(inputMarga.ToLower()));
            }
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            var value = slider.Value;
            this.Panel.Scale = value / 100;
        }
    }
}