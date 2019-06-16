using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Order : BaseEntity {

        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
