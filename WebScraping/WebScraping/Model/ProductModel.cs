using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.Model
{
    internal class ProductModel
    {
        public string Product { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string Discount { get; set; }

        public string Details { get; set; }
        public string Rate { get; set; }

        public string User { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string URL { get; set; }
    }
}
