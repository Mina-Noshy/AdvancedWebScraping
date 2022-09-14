using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.Model
{
    internal class CustomInputsModel
    {
        public string ScrapingURL { get; set; }
        
        public string? MainNode { get; set; }
        public string? ProductNode { get; set; }
        public string? OldPriceNode { get; set; }
        public string? NewPriceNode { get; set; }

        public string? DiscountNode { get; set; }
        public string? DetailsNode { get; set; }
        public string? RateNode { get; set; }

        public string? UserNode { get; set; }
        public string? MobileNode { get; set; }
        public string? EmailNode { get; set; }

        public string? URL { get; set; }
    }
}
