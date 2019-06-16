using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.CategoryDto {
    public class EditCategoryDto {

        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters.")]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters.")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

    }
}
