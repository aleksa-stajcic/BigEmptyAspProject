using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Role : BaseEntity {

        public static int DefaultGroupId => 1;

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
