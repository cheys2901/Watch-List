using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using WristWatch.Models;
using WristWatch.Views;
using WristWatch.Services;
using Xamarin.Forms;

namespace WristWatch.ViewModels
{
    public class WatchListViewModel: BaseViewModel
    {
        private bool _isListView;
        private bool _isRefreshing;
        private WatchListPage _page;
        public FilterOptions filterOptions;

        public List<Watch> WatchList { get; set; }
        public List<Watch> Items
        {
            get { return getFilteredWatches(); }
            set { Items = value; }
        }
        public Command RefreshCommand { get; }
        public Command ChangeView { get; }

        public string ViewIcon { get => !_isListView ? "icon_list.png" : "icon_grid.png"; }
        public bool IsListView { get => _isListView; }
        public bool IsGridView { get => !_isListView; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public WatchListViewModel(WatchListPage page, bool isListView)
        {
            _page = page;
            _isListView = isListView;
            filterOptions = new FilterOptions();

            var firebaseDbService = DependencyService.Get<IFirebaseDatabaseService>();

            RefreshCommand = new Command(Refresh);
            ChangeView = new Command(OnChangeView);

            var watchList = firebaseDbService.GetAllWatches();

            WatchList = watchList != null ? watchList : new List<Watch>();
        }

        public void UpdateGrid()
        {
            _page.AddWatchToGrid(Items);
        }

        private async void Refresh(object obj)
        {
            IsRefreshing = true;

            var firebaseDbService = DependencyService.Get<IFirebaseDatabaseService>();
            var watchList = firebaseDbService.GetAllWatches();

            if (watchList != null)
                WatchList = watchList;

            if (!_isListView)
                _page.AddWatchToGrid(Items);

            await Task.Delay(200);
            UpdateFilter();
            //OnPropertyChanged("WatchList");
            IsRefreshing = false;
        }

        private void OnChangeView(object obj)
        {
            _isListView = !_isListView;

            OnPropertyChanged("IsListView");
            OnPropertyChanged("IsGridView");
            OnPropertyChanged("ViewIcon");

            if (!_isListView)
                _page.AddWatchToGrid(Items);

            MessagingCenter.Send(Application.Current.MainPage, "SetListView", _isListView);
        }

        private List<Watch> getFilteredWatches()
        {
            var CurrentList = new List<Watch>(WatchList);
            if (filterOptions.MaxWidth != 0)
                CurrentList = CurrentList.FindAll(x => x.Width <= filterOptions.MaxWidth);
            CurrentList = CurrentList.FindAll(x => x.Width >= filterOptions.MinWidth);
            if (filterOptions.MaxThickness != 0)
                CurrentList = CurrentList.FindAll(x => x.Thickness <= filterOptions.MaxThickness);
            CurrentList = CurrentList.FindAll(x => x.Thickness >= filterOptions.MinThickness);
            if (filterOptions.MaxPrice != 0)
                CurrentList = CurrentList.FindAll(x => x.Price <= filterOptions.MaxPrice);
            CurrentList = CurrentList.FindAll(x => x.Price >= filterOptions.MinPrice);
            if (filterOptions.Type != null && filterOptions.Type.Length > 0)
                CurrentList = CurrentList.FindAll(x => x.Type.ToLower().Contains(filterOptions.Type.ToLower()));

            return CurrentList;
        }

        public void UpdateFilter()
        {
            OnPropertyChanged("Items");
        }
    }
}
