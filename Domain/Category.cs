using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Category : BaseEntity {

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
