using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.RoleDto {
    public class CreateRoleDto {

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

    }
}
