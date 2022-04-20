using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristWatch.Models;
using WristWatch.Services;
using WristWatch.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristWatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchListPage : ContentPage
    {
        WatchListViewModel portaitWatchesViewModel;

        public WatchListPage(bool isListView)
        {
            portaitWatchesViewModel = new WatchListViewModel(this, isListView);
            BindingContext = portaitWatchesViewModel;

            InitializeComponent();

            portaitWatchesViewModel.UpdateGrid();
        }

        public async void OpenWatch(Watch watch)
        {
            await Navigation.PushAsync(new WatchPage(watch, false));
        }

        public async void OpenAddWatch(AddWatchPage page)
        {
            await Navigation.PushAsync(page);
        }

        public async void OnItemClicked(object sender, ItemTappedEventArgs e)
        {
            MessagingCenter.Send(Application.Current.MainPage, "SetPage", (Watch)e.Item as object);
            await Navigation.PushAsync(new WatchPage((Watch)e.Item, false));
            WatchList.SelectedItem = null;
        }

        private async void AddWatchClicked(object sender, EventArgs e)
        {
            var page = new AddWatchPage();
            MessagingCenter.Send(Application.Current.MainPage, "SetPage", page as object);
            await Navigation.PushAsync(page);
        }

        private async void FilterWatchClicked(object sender, EventArgs e)
        {
            var page = new FilterPage(portaitWatchesViewModel.filterOptions, portaitWatchesViewModel);
            MessagingCenter.Send(Application.Current.MainPage, "SetPage", null as object);
            await Navigation.PushAsync(page);
        }

        public void AddWatchToGrid(List<Watch> watch)
        {
            var rows = (int)Math.Ceiling((decimal)watch.Count / 3);
            grid.Children.Clear();

            for (int i = grid.RowDefinitions.Count; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = 110 });
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 3 && 3 * i + j < watch.Count; j++)
                {
                    var image = new Image()
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };
                    image.Source = watch[3 * i + j].Image.DownloadUrl;

                    var imgFrame = new Frame
                    {
                        HeightRequest = 110,
                        HorizontalOptions = LayoutOptions.Center,
                        Padding = 0,
                        IsClippedToBounds = true,
                        CornerRadius = 25,
                        Content = image
                    };

                    grid.Children.Add(imgFrame, j, i);

                    var button = new Button();
                    button.Text = watch[3 * i + j].Id;
                    button.BackgroundColor = Color.Transparent;
                    button.TextColor = Color.Transparent;
                    button.Clicked += GridItemClicked;
                    grid.Children.Add(button, j, i);
                }
            }
        }

        private async void GridItemClicked(object sender, EventArgs e)
        {
            var firebaseDatebaseService = DependencyService.Get<IFirebaseDatabaseService>();
            var watch = firebaseDatebaseService.GetWatchById(((Button)sender).Text);

            MessagingCenter.Send(Application.Current.MainPage, "SetPage", watch as object);
            await Navigation.PushAsync(new WatchPage(watch, false));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                MessagingCenter.Send(Application.Current.MainPage, "ChangePageToFlyout");
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(Application.Current.MainPage, "SetPage", null as object);
        }
    }
}