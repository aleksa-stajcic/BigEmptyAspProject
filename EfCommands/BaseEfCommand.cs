using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands {
    public class BaseEfCommand {

        protected BigEmptyContext Context { get; set; }

        public BaseEfCommand(BigEmptyContext context) => Context = context;

    }
}
