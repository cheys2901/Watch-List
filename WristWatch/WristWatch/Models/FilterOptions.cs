using System;
using System.Collections.Generic;
using System.Text;

namespace WristWatch.Models
{
    public class FilterOptions
    {
        public string Type { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public int MinThickness { get; set; }
        public int MaxThickness { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public void clear() 
        {
            this.Type = "";
            this.MinWidth = 0;
            this.MaxWidth = 0;
            this.MinThickness = 0;
            this.MaxThickness = 0;
            this.MinPrice = 0;
            this.MaxPrice = 0;
        }

        public FilterOptions(FilterOptions options)
        {
            this.Type = options.Type;
            this.MinWidth = options.MinWidth;
            this.MaxWidth = options.MaxWidth;
            this.MinThickness = options.MinThickness;
            this.MaxThickness = options.MaxThickness;
            this.MinPrice = options.MinPrice;
            this.MaxPrice = options.MaxPrice;
        }

        public FilterOptions()
        {
            this.Type = "";
            this.MinWidth = 0;
            this.MaxWidth = 0;
            this.MinThickness = 0;
            this.MaxThickness = 0;
            this.MinPrice = 0;
            this.MaxPrice = 0;
        }
    }
}
