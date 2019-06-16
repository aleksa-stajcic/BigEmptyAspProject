using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer.ProductDto {
    public class SearchProductsDto {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public string ImageAlt { get; set; }

        public IEnumerable<CreateCommentDto> Comments { get; set; }


    }
}
