using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Password : BaseEntity {

        public static int DefaultHiddenId => 1001;

        public string Hidden { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

    }
}
