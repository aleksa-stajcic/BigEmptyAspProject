using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions {
    public class EntityNotFoundException : Exception {
        public EntityNotFoundException() {
        }

        public EntityNotFoundException(string message) : base($"{message} doesn't exist.") {
        }
    }
}
