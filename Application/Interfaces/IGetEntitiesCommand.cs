using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces {
    public interface IGetEntitiesCommand<TResponse> {
        TResponse Execute();
    }
}
