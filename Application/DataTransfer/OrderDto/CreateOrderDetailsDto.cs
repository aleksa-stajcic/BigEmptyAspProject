using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.OrderDto {
    public class CreateOrderDetailsDto {

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Only a maximum of 10 items can be ordered.")]
        public int Quantity { get; set; }

    }
}
