using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WristWatch.Models;
using WristWatch.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristWatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        private FilterOptions _filterOptions;
        WatchListViewModel _WatchListViewModel;

        public FilterPage(FilterOptions filterOptions, WatchListViewModel WatchesViewModel)
        {
            _filterOptions = filterOptions;
            _WatchListViewModel = WatchesViewModel;

            InitializeComponent();

            Type.Text = _filterOptions.Type;
            MinWidth.Text = _filterOptions.MinWidth == 0 ? "" : _filterOptions.MinWidth.ToString();
            MaxWidth.Text = _filterOptions.MaxWidth == 0 ? "" : _filterOptions.MaxWidth.ToString();

            MinThickness.Text = _filterOptions.MinThickness == 0 ? "" : _filterOptions.MinThickness.ToString();
            MaxThickness.Text = _filterOptions.MaxThickness == 0 ? "" : _filterOptions.MaxThickness.ToString();

            MinPrice.Text = _filterOptions.MinPrice == 0 ? "" : _filterOptions.MinPrice.ToString();
            MaxPrice.Text = _filterOptions.MaxPrice == 0 ? "" : _filterOptions.MaxPrice.ToString();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _filterOptions.clear();

            Type.Text = "";
            MinWidth.Text = "";
            MaxWidth.Text = "";

            MinThickness.Text = "";
            MaxThickness.Text = "";

            MinPrice.Text = "";
            MaxPrice.Text = "";
            _WatchListViewModel.UpdateFilter();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (IsCorrectFields())
            {
                var minwidth = int.Parse(MinWidth.Text == "" ? "0" : MinWidth.Text);
                var maxwitdh = int.Parse(MaxWidth.Text == "" ? "0" : MaxWidth.Text);
                var minthickness = int.Parse(MinThickness.Text == "" ? "0" : MinThickness.Text);
                var maxthickness = int.Parse(MaxThickness.Text == "" ? "0" : MaxThickness.Text);
                var minp = int.Parse(MinPrice.Text == "" ? "0" : MinPrice.Text);
                var maxp = int.Parse(MaxPrice.Text == "" ? "0" : MaxPrice.Text);

                _filterOptions.MinPrice = minp;
                _filterOptions.MaxPrice = maxp;
                _filterOptions.MinWidth = minwidth;
                _filterOptions.MaxWidth = maxwitdh;
                _filterOptions.MinThickness = minthickness;
                _filterOptions.MaxThickness = maxthickness;
                _filterOptions.Type = Type.Text;
                _WatchListViewModel.UpdateFilter();
                await Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fields filled in incorrectly",
                    "Please fill in the fields with correct data", "OK");
            }
        }

        private bool IsCorrectFields()
        {
            try
            {
                int.Parse(MinWidth.Text == "" ? "0" : MinWidth.Text);
                int.Parse(MaxWidth.Text == "" ? "0" : MaxWidth.Text);
                int.Parse(MinThickness.Text == "" ? "0" : MinThickness.Text);
                int.Parse(MaxThickness.Text == "" ? "0" : MaxThickness.Text);
                decimal.Parse(MinPrice.Text == "" ? "0.0" : MinPrice.Text);
                decimal.Parse(MaxPrice.Text == "" ? "0.0" : MaxPrice.Text);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}