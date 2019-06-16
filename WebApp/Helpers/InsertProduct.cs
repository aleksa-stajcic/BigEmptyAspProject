using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helpers {
    public class InsertProduct {

        [Required]
        [RegularExpression("^([A-Z][a-z0-9\\s]{3,30})$", ErrorMessage = "Name in the wrong format.")]
        public string Name { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, 7000, ErrorMessage = "Price must be between 1 and 7000.")]
        public decimal Price { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Description must be at least 5 characters.")]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

    }
}
