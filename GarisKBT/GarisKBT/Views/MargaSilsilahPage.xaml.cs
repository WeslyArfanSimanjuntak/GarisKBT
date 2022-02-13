using Xamarin.Essentials;
using GarisKBT.Helper;
using GarisKBT.Models;
using GarisKBT.Services;
using GarisKBT.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GarisKBT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MargaSilsilahPage : ContentPage
    {
        MargaDetailViewModel _viewModel;
        public MargaSilsilahPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MargaDetailViewModel();
            ItemTapped = new Command<GetSilsilahResult>(OnItemSelectedSilsilah);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //ShowSilsilahWeb();
            ShowSilsilah();
        }
        public async void ShowSilsilahWeb()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                //var silsilahResult = await App.MargaManager.GetSilsilahAsync(this._viewModel.ItemId);
                await Browser.OpenAsync(Constants.GenerateSilsilah + "/" + this._viewModel.ItemId);
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

        public async void ShowSilsilah()
        {
            try
            {
                var silsilahResult = await App.MargaManager.GetSilsilahAsync(this._viewModel.ItemId);
                for (int i = 1; i <= silsilahResult.Select(x => x.rank).Max(); i++)
                {

                    var brothers = silsilahResult.Where(x => x.rank == i).OrderBy(x => x.order);
                    var stackLayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        StyleId = "sl" + i
                    };
                    //stackLayout.na
                    foreach (var item in brothers)
                    {
                        var frame = new Frame();
                        frame.BorderColor = Color.White;

                        var label = new Label()
                        {
                            Text = item.Nama.ToUpper(),
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = Color.White,
                            FontSize = 12
                        };
                        var labelNim = new Label()
                        {
                            Text = "[" + item.rank + "] " + item.nim,
                            TextColor = Color.White,
                            FontSize = 8,
                            HorizontalTextAlignment = TextAlignment.Center,

                        };

                        frame.CornerRadius = 5;
                        //frame.Padding = new Thickness(5);
                        var slFrame = new StackLayout();
                        slFrame.Children.Add(label);
                        slFrame.Children.Add(labelNim);
                        frame.Content = slFrame;
                        frame.StyleId = item.nim;
                        if (item.isAncestor)
                        {
                            frame.BackgroundColor = Color.Green;

                        }
                        else
                        {
                            frame.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                        }
                        var tapRecog = new TapGestureRecognizer();
                        tapRecog.CommandParameter = item;

                        tapRecog.Command = ItemTapped;
                        tapRecog.NumberOfTapsRequired = 1;
                        frame.GestureRecognizers.Add(tapRecog);
                        stackLayout.Children.Add(frame);

                    }
                    this.StackLayout.Children.Add(stackLayout);
                }



            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Data ");
            }
            finally
            {
                await this.ScrollView.ScrollToAsync(StackLayout.Children.LastOrDefault(), ScrollToPosition.Center, true);
            }
        }
        public Command<GetSilsilahResult> ItemTapped { get; }
        async void OnItemSelectedSilsilah(GetSilsilahResult model)
        {
            if (this._viewModel.IsBusy)
            {
                return;
            }
            try
            {
                this._viewModel.IsBusy = true;
                //Ganti Background Frame ke hijau apabila ketapped
                var stackLayoutSelected = this.StackLayout.Children.Where(x => x.StyleId == "sl" + model.rank).FirstOrDefault();
                if (stackLayoutSelected != null)
                {
                    var sl = (StackLayout)stackLayoutSelected;
                    foreach (var item in sl.Children)
                    {
                        if (item.StyleId == model.nim)
                        {
                            item.BackgroundColor = Color.Green;
                        }
                        else
                        {
                            item.BackgroundColor = (Color)Application.Current.Resources["Primary"];

                        }
                    }
                }


                StackLayout stackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    StyleId = "sl" + (model.rank + 1)
                };
                var stackLayoutTemp = this.StackLayout.Children.Where(x => x.StyleId == "sl" + (model.rank + 1)).FirstOrDefault();


                int indexSL = 0;
                if (stackLayoutTemp != null)
                {
                    //stackLayout = (StackLayout)stackLayoutTemp;
                    indexSL = this.StackLayout.Children.IndexOf(stackLayoutTemp);
                    int CountStackLayout = StackLayout.Children.Count;
                    for (int i = CountStackLayout - 1; i >= indexSL; i--)
                    {
                        StackLayout.Children.RemoveAt(i);
                    }
                }

                var brothers = await App.MargaManager.GetChildsAsync(model.id);
                if (brothers.Count == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Pomparan " + model.Nama + " Tidak Ditemukan");
                }


                //this.StackLayout.Children.remo


                foreach (var item in brothers)
                {
                    var slFrame = new StackLayout();
                    var frame = new Frame();
                    frame.BorderColor = Color.White;
                    var label = new Label()
                    {
                        Text = item.Nama.ToUpper(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.White,
                        FontSize = 12
                    };
                    var labelNim = new Label()
                    {
                        Text = "[" + item.rank + "] " + item.nim,
                        TextColor = Color.White,
                        FontSize = 8,
                        HorizontalTextAlignment = TextAlignment.Center,

                    };

                    frame.CornerRadius = 5;
                    //frame.Padding = new Thickness(5);

                    slFrame.Children.Add(label);
                    slFrame.Children.Add(labelNim);
                    frame.Content = slFrame;
                    if (item.isAncestor)
                    {
                        frame.BackgroundColor = Color.Green;

                    }
                    else
                    {
                        frame.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                    }
                    TapGestureRecognizer tapRecog = new TapGestureRecognizer();
                    tapRecog.CommandParameter = item;
                    tapRecog.Command = ItemTapped;
                    tapRecog.NumberOfTapsRequired = 1;
                    frame.StyleId = item.nim;
                    frame.GestureRecognizers.Add(tapRecog);
                    stackLayout.Children.Add(frame);
                    StackLayout.Children.Add(stackLayout);
                }

            }
            catch (Exception)
            {

                Debug.WriteLine("Failed to Load Data "); ;
            }
            finally
            {
                _viewModel.IsBusy = false;
                await ScrollView.ScrollToAsync(StackLayout.Children.LastOrDefault(), ScrollToPosition.Center, true);
            }

        }
        async void OnItemSelectedSilsilah2(GetSilsilahResult model)
        {
            try
            {
                this._viewModel.ItemId = model.id;
                this._viewModel.IsBusy = true;
                this.StackLayout.Children.Clear();
                var silsilahResult = await App.MargaManager.GetSilsilahAsync(this._viewModel.ItemId);
                for (int i = 1; i <= silsilahResult.Select(x => x.rank).Max(); i++)
                {

                    var brothers = silsilahResult.Where(x => x.rank == i).OrderBy(x => x.order);
                    var stackLayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        StyleId = "sl" + i
                    };
                    //stackLayout.na
                    foreach (var item in brothers)
                    {
                        var frame = new Frame();
                        frame.BorderColor = Color.White;
                        var label = new Label()
                        {
                            Text = item.Nama.ToUpper(),
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = Color.White,
                            FontSize = 12
                        };
                        var labelNim = new Label()
                        {
                            Text = "[" + item.rank + "] " + item.nim,
                            TextColor = Color.White,
                            FontSize = 8,
                            HorizontalTextAlignment = TextAlignment.Center,

                        };

                        frame.CornerRadius = 5;
                        //frame.Padding = new Thickness(5);
                        var slFrame = new StackLayout();
                        slFrame.Children.Add(label);
                        slFrame.Children.Add(labelNim);
                        frame.Content = slFrame;
                        frame.StyleId = item.nim;
                        if (item.isAncestor)
                        {
                            frame.BackgroundColor = Color.Green;

                        }
                        else
                        {
                            frame.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                        }
                        var tapRecog = new TapGestureRecognizer();
                        tapRecog.CommandParameter = item;

                        tapRecog.Command = ItemTapped;
                        tapRecog.NumberOfTapsRequired = 1;
                        frame.GestureRecognizers.Add(tapRecog);
                        stackLayout.Children.Add(frame);

                    }
                    this.StackLayout.Children.Add(stackLayout);
                }


                var brothersNew = await App.MargaManager.GetChildsAsync(model.id);


                //this.StackLayout.Children.remo


                StackLayout stackLayoutNew = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    StyleId = "sl" + (model.rank + 1)
                };
                foreach (var item in brothersNew)
                {
                    var slFrame = new StackLayout();
                    var frame = new Frame();
                    frame.BorderColor = Color.White;
                    var label = new Label()
                    {
                        Text = item.Nama.ToUpper(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.White,
                        FontSize = 12
                    };
                    var labelNim = new Label()
                    {
                        Text = "[" + item.rank + "] " + item.nim,
                        TextColor = Color.White,
                        FontSize = 8,
                        HorizontalTextAlignment = TextAlignment.Center,

                    };

                    frame.CornerRadius = 5;
                    //frame.Padding = new Thickness(5);

                    slFrame.Children.Add(label);
                    slFrame.Children.Add(labelNim);
                    frame.Content = slFrame;
                    if (item.isAncestor)
                    {
                        frame.BackgroundColor = Color.Green;

                    }
                    else
                    {
                        frame.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                    }
                    var tapRecog = new TapGestureRecognizer();
                    tapRecog.CommandParameter = item;
                    tapRecog.Command = ItemTapped;
                    tapRecog.NumberOfTapsRequired = 1;
                    frame.StyleId = item.nim;
                    frame.GestureRecognizers.Add(tapRecog);
                    stackLayoutNew.Children.Add(frame);
                    this.StackLayout.Children.Add(stackLayoutNew);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Data ");
            }
            finally
            {
                this._viewModel.IsBusy = false;
                await this.ScrollView.ScrollToAsync(StackLayout.Children.LastOrDefault(), ScrollToPosition.Center, false);


            }


        }

        

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShowSilsilahWeb();
        }
    }
}