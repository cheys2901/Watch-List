using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristWatch.Models;

namespace WristWatch.Views
{
    public class FlyoutMainPageFlyoutMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Watch Watch { get; set; }
        public string TargetType { get; set; }
        public string Icon { get; set; }
    }
}