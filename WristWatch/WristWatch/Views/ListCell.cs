using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WristWatch.Views
{
    public class ListCell: ViewCell
    {
        private readonly Label _lblName;
        private readonly Image _img;
        private readonly Frame _imgFrame;

        public ListCell()
        {
            _lblName = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 0, 0, 0),
                FontSize = 20,
                FontAttributes = FontAttributes.Bold
            };

            _img = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            
            _imgFrame = new Frame
            {
                HeightRequest = 90,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                IsClippedToBounds = true,
                CornerRadius = 25
            };

            var cell = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 130
            };

            var infoCell = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center
            };

            var imageCell = new StackLayout
            {
                WidthRequest = 170,
                VerticalOptions = LayoutOptions.Center
            };

            _imgFrame.Content = _img;
            imageCell.Children.Add(_imgFrame);

            infoCell.Children.Add(_lblName);

            cell.Children.Add(imageCell);
            cell.Children.Add(infoCell);

            View = cell;
        }

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public string WathcesId
        {
            get => (string)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly BindableProperty NameProperty = BindableProperty.Create($"{nameof(Name)}", typeof(string), typeof(ListCell), "");
        public static readonly BindableProperty IdProperty = BindableProperty.Create($"{nameof(WathcesId)}", typeof(string), typeof(ListCell), "");
        public static readonly BindableProperty ImageProperty = BindableProperty.Create($"{nameof(Image)}", typeof(ImageSource), typeof(ListCell), null);

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                _lblName.Text = Name;
                _img.Source = Image;
            }
        }
    }
}
