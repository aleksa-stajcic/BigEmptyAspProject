using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches {
    public class ProductSearch : BaseSearch {

        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public string ProductName { get; set; }

    }
}
