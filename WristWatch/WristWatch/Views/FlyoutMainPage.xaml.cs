using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WristWatch.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristWatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMainPage : FlyoutPage
    {
        public FlyoutMainPage(object page)
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;

            if (page != null)
            {
                if (page is Watch)
                {
                    var watchPage = new WatchPage(page as Watch, true);
                    Detail = new NavigationPage(watchPage);
                    IsPresented = false;
                    watchPage.Title = (page as Watch).Name;
                }
                else if (page is AddWatchPage)
                {
                    var addPage = new AddWatchPage();
                    addPage.Title = "Add watch";
                    Detail = new NavigationPage(addPage);
                }
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutMainPageFlyoutMenuItem;
            if (item == null)
                return;

            var page = new Page();
            if (item.TargetType == "Watch")
            {
                page = new WatchPage(item.Watch, true);
                MessagingCenter.Send(Application.Current.MainPage, "SetPage", item.Watch as object);
            }
            if (item.TargetType == "Add")
            {
                page = new AddWatchPage();
                MessagingCenter.Send(Application.Current.MainPage, "SetPage", page as object);
            }
            //var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width < height)
            {
                MessagingCenter.Send(Application.Current.MainPage, "ChangePageToWatchList");
            }
        }
    }
}