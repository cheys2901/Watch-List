using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WristWatch.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristWatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWatchPage : ContentPage
    {
        public AddWatchPage()
        {
            var portaitAddWatchViewModel = new AddWatchViewModel(this);
            BindingContext = portaitAddWatchViewModel;

            InitializeComponent();
        }
        public async void Close()
        {
            await Navigation.PopAsync();
            MessagingCenter.Send(Application.Current.MainPage, "SetPage", null as object);
        }
    }
}