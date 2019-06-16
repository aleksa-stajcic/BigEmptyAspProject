using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class User : BaseEntity {

        public static int DefaultUserId => 3;

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public Password Password { get; set; }

    }
}
