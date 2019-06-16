using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class ProductImage : BaseEntity {

        public int ProductId { get; set; }
        public string FileName { get; set; }
        public string Alt { get; set; }

        public Product Product { get; set; }

    }
}
