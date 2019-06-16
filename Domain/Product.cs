using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Product : BaseEntity {

        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Category Category { get; set; }
        public ProductImage ProductImage { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
