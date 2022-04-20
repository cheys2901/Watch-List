using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using WristWatch.Views;
using WristWatch.Services;
using WristWatch.Models;

namespace WristWatch.ViewModels
{
    public class AddWatchViewModel: BaseViewModel
    {
        private AddWatchPage _page;
        private readonly IFirebaseDatabaseService _firebaseDatebaseService;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Width { get; set; }
        public string Thickness { get; set; }
        public string Price { get; set; }

        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string VideoUrl { get; set; }
        public string VideoName { get; set; }

        public ICommand AddImage { get; }
        public ICommand AddVideo { get; }
        public ICommand Save { get; }

        private FileResult _image;
        private FileResult _video;

        public AddWatchViewModel(AddWatchPage page)
        {
            _page = page;
            _firebaseDatebaseService = DependencyService.Get<IFirebaseDatabaseService>();
            _firebaseStorageService = DependencyService.Get<IFirebaseStorageService>();

            Save = new Command(OnSaveButtonClicked);
            AddImage = new Command(OnAddImageButtonClicked);
            AddVideo = new Command(OnAddVideoButtonClicked);
        }

        private async void OnAddImageButtonClicked()
        {
            _image = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Select image" });
        }

        private async Task<string> LoadImage()
        {
            if (_image == null)
            {
                return null;
            }

            string extension = _image.FileName.Split('.')[1];
            var stream = await _image.OpenReadAsync();
            ImageName = Guid.NewGuid().ToString();
            return await _firebaseStorageService.LoadImage(stream, ImageName, extension);
        }

        private async void OnAddVideoButtonClicked()
        {
            _video = await MediaPicker.PickVideoAsync(new MediaPickerOptions { Title = "Select video" });
        }

        private async Task<string> LoadVideo()
        {
            if (_video == null)
            {
                return null;
            }

            string extension = _video.FileName.Split('.')[1];
            var stream = await _video.OpenReadAsync();
            VideoName = Guid.NewGuid().ToString();
            return await _firebaseStorageService.LoadVideo(stream, VideoName, extension);
        }

        private async void OnSaveButtonClicked()
        {
            if (IsCorrectFields())
            {
                ImageUrl = await LoadImage();
                VideoUrl = await LoadVideo();

                var item = new Watch
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    Description = Description,
                    Type = Type,
                    Width = int.Parse(Width),
                    Thickness = int.Parse(Thickness),
                    Price = int.Parse(Price),
                    Image = new CloudFileData
                    {
                        FileName = ImageName,
                        DownloadUrl = ImageUrl
                    },
                    Video = new CloudFileData
                    {
                        FileName = VideoName,
                        DownloadUrl = VideoUrl
                    }
                };

                await _firebaseDatebaseService.AddWatch(item);
                _page.Close();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fields filled in incorrectly",
                    "Please fill in the fields with correct data", "OK");
            }
        }

        private bool IsCorrectFields()
        {
            if (!string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(Description) ||
                !string.IsNullOrWhiteSpace(Type))
            {
                try
                {
                    int.Parse(Width);
                    int.Parse(Thickness);
                    decimal.Parse(Price);

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}