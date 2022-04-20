using System;
using System.Collections.Generic;
using System.Text;

using WristWatch.Services;
using Xamarin.Forms;

namespace WristWatch.ViewModels
{
    public class LoginViewModel
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        private readonly IFirebaseDatabaseService _firebaseDbService;

        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLandscape { get; set; }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();
            _firebaseDbService = DependencyService.Get<IFirebaseDatabaseService>();
        }

        private async void OnLoginClicked(object obj)
        {
            bool isAuthSuccessful = await _firebaseAuthentication.LoginAsync(Email, Password);

            if (isAuthSuccessful)
            {
                if (IsLandscape)
                {
                    MessagingCenter.Send(Application.Current.MainPage, "ChangePageToFlyout");
                }
                else
                {
                    MessagingCenter.Send(Application.Current.MainPage, "ChangePageToWatchList");
                }


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login error",
                    "Logout Failed", "OK");
            }
        }
    }
}
