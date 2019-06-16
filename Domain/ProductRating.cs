using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class ProductRating : BaseEntity {

        public int Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }

    }
}
