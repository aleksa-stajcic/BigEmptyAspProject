using Application.DataTransfer.OrderDto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.OrderCommands {
    public interface ICreateOrderDetailsCommand : ICommand<CreateOrderDetailsDto> {
    }
}
