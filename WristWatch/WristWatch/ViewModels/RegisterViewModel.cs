using System;
using System.Collections.Generic;
using System.Text;

using WristWatch.Services;
using Xamarin.Forms;

namespace WristWatch.ViewModels
{
    public class RegisterViewModel
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        private readonly IFirebaseDatabaseService _firebaseDbService;

        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public bool IsLandscape { get; set; }

        public Command Register { get; }

        public RegisterViewModel()
        {
            _firebaseDbService = DependencyService.Get<IFirebaseDatabaseService>();
            _firebaseAuthentication = DependencyService.Get<IFirebaseAuthentication>();

            Register = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            if (Password == RePassword && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email))
            {
                if (Password.Length > 5)
                {
                    bool isRegistrationSuccessful = await _firebaseAuthentication.RegisterAsync(Email, Password);

                    if (isRegistrationSuccessful)
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
                        await Application.Current.MainPage.DisplayAlert("Registration error",
                            "There is already a user with this email", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registration error",
                            "Password must be more than 6 characters", "OK");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
                    await Application.Current.MainPage.DisplayAlert("Registration error",
                        "Fields must be filled", "OK");
                if (Password != RePassword && !string.IsNullOrWhiteSpace(Password))
                    await Application.Current.MainPage.DisplayAlert("Registration error",
                        "PasswordsError", "OK");
            }
        }
    }
}