using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.OrderDto {
    public class CreateOrderDto {

        [Required]
        public int UserId { get; set; }

    }
}
