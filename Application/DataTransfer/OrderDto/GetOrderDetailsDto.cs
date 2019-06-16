using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer.OrderDto {
    public class GetOrderDetailsDto {

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }

    }
}
