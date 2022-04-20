using System;
using System.Collections.Generic;
using System.Text;

namespace WristWatch.Models
{
    public class Watch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }
        public int Width { get; set; }
        public int Thickness { get; set; }
        public int Price { get; set; }

        public CloudFileData Image { get; set; }
        public CloudFileData Video { get; set; }
    }
}
