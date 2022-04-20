using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WristWatch.Views;
using WristWatch.Models;

namespace WristWatch
{
    public partial class App : Application
    {
        private object _page = null;
        private bool _listViewMode = true;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            MessagingCenter.Subscribe<Page>(this, "ChangePageToWatchList",
                    (sender) =>
                    {
                        var watchesPage = new WatchListPage(_listViewMode);
                        var page = new NavigationPage(watchesPage);
                        if (_page != null)
                        {
                            if (_page is Watch)
                            {
                                watchesPage.OpenWatch(_page as Watch);
                            }
                            else if (_page is AddWatchPage)
                            {
                                watchesPage.OpenAddWatch(new AddWatchPage());
                            }
                        }
                        Current.MainPage = page;
                    });
            MessagingCenter.Subscribe<Page>(this, "ChangePageToFlyout",
               (sender) =>
               {
                   Current.MainPage = new FlyoutMainPage(_page);
               });
            MessagingCenter.Subscribe<Page, object>(this, "SetPage",
               (sender, args) =>
               {
                   _page = args;
               });

            MessagingCenter.Subscribe<Page, bool>(this, "SetListView",
               (sender, args) =>
               {
                   _listViewMode = args;
               });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
