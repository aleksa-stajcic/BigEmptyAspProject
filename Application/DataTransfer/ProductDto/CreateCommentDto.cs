using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.ProductDto {
    public class CreateCommentDto {

        [Required]
        [MinLength(5, ErrorMessage = "Comment can't be shorter than 5 characters.")]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

    }
}
