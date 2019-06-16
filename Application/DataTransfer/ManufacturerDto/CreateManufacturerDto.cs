using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.ManufacturerDto {
    public class CreateManufacturerDto {

        [Required]
        [MinLength(3, ErrorMessage = "Mane must be at least 3 characters.")]
        public string Name { get; set; }

    }
}
