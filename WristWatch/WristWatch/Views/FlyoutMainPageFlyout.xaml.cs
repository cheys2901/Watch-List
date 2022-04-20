using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using WristWatch.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristWatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMainPageFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutMainPageFlyout()
        {
            InitializeComponent();

            BindingContext = new FlyoutMainPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FlyoutMainPageFlyoutViewModel : INotifyPropertyChanged
        {
            private bool _isRefreshing;

            public ObservableCollection<FlyoutMainPageFlyoutMenuItem> MenuItems { get; set; }

            public Command RefreshCommand { get; }

            public bool IsRefreshing
            {
                get { return _isRefreshing; }
                set
                {
                    _isRefreshing = value;
                    OnPropertyChanged();
                }
            }

            public FlyoutMainPageFlyoutViewModel()
            {
                RefreshCommand = new Command(Refresh);

                MenuItems = new ObservableCollection<FlyoutMainPageFlyoutMenuItem>();
                MenuItems.Add(new FlyoutMainPageFlyoutMenuItem { Id = 0, Title = "Add watch", TargetType = "Add", Icon = "icon_add.png" });

                var firebaseDbService = DependencyService.Get<IFirebaseDatabaseService>();
                var watches = firebaseDbService.GetAllWatches();

                if (watches != null)
                {
                    foreach (var item in watches)
                    {
                        MenuItems.Add(new FlyoutMainPageFlyoutMenuItem { Id = 0, Title = item.Name, Watch = item, TargetType = "Watch", Icon = "icon_watch.png" });
                    }
                }
            }

            private async void Refresh(object obj)
            {
                IsRefreshing = true;

                var firebaseDbService = DependencyService.Get<IFirebaseDatabaseService>();
                var watches = firebaseDbService.GetAllWatches();

                if (watches != null)
                {
                    MenuItems.Clear();

                    MenuItems.Add(new FlyoutMainPageFlyoutMenuItem { Id = 0, Title = "Add watch", TargetType = "Add", Icon = "icon_add.png" });

                    if (watches != null)
                    {
                        foreach (var item in watches)
                        {
                            MenuItems.Add(new FlyoutMainPageFlyoutMenuItem { Id = 0, Title = item.Name, Watch = item, TargetType = "Watch", Icon = "icon_watch.png" });
                        }
                    }
                }

                await Task.Delay(200);
                OnPropertyChanged("MenuItems");
                IsRefreshing = false;
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}