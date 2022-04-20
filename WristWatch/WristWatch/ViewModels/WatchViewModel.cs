using System;
using System.Collections.Generic;
using System.Text;
using WristWatch.Models;
using System.Globalization;

namespace WristWatch.ViewModels
{
    public class WatchViewModel: BaseViewModel
    {
        public Watch Watch { get; set; }

        public string Id { get; set; }
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

        public WatchViewModel(Watch watch)
        {
            Watch = watch;

            Id = watch.Id;
            Name = watch.Name;
            Description = watch.Description;
            Type = watch.Type;
            Width = watch.Width.ToString();
            Thickness = watch.Thickness.ToString();
            Price = $"{Watch.Price.ToString(CultureInfo.InvariantCulture)}$";
            ImageUrl = Watch.Image.DownloadUrl;
            ImageName = Watch.Image.FileName;
            VideoUrl = Watch.Video.DownloadUrl;
            VideoName = Watch.Video.FileName;
        }
    }
}
