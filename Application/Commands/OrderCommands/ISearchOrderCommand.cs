using Application.DataTransfer.OrderDto;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.OrderCommands {
    public interface ISearchOrderCommand : ICommand<OrderSearch, PagedResponse<GetOrderDto>> {
    }
}
