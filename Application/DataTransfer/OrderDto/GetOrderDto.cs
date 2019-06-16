using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer.OrderDto {
    public class GetOrderDto {

        public int UserId { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<GetOrderDetailsDto> OrderDetails { get; set; }

    }
}
