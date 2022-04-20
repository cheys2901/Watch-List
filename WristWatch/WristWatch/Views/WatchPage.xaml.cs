using WristWatch.Models;
using WristWatch.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace WristWatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchPage : ContentPage
    {
        private readonly WatchViewModel _viewModel;
        private readonly bool _isLandscape;
        public WatchPage(Watch watch, bool isLandscape)
        {
            _viewModel = new WatchViewModel(watch);
            BindingContext = _viewModel;
            InitializeComponent();

            stackLayout.Orientation = isLandscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
            _isLandscape = isLandscape;
            try
            {
                if (player != null)
                {
                    //player.IsVisible = false;
                    if (!string.IsNullOrEmpty(_viewModel.VideoUrl))
                    {
                        player.WidthRequest = default;
                        player.Source = _viewModel.VideoUrl;
                        player.IsVisible = true;
                        player.ShowsPlaybackControls = true;
                    }
                }
            }
            catch (Exception e)
            {
                _ = 1;
            }
        }
    }
}