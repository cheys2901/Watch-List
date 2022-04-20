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
    public partial class RegisterPage : ContentPage
    {
        private readonly RegisterViewModel _registerViewModel = new RegisterViewModel();
        public RegisterPage()
        {
            BindingContext = _registerViewModel;

            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            _registerViewModel.IsLandscape = width > height;
        }
    }
}