using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.OrderCommands;
using Application.DataTransfer.OrderDto;
using Application.Exceptions;
using DataAccess;

namespace EfCommands.EfOrderCommands {
    public class EfCreateOrderCommand : BaseEfCommand, ICreateOrderCommand {
        public EfCreateOrderCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateOrderDto request) {
            
            if(!Context.Users.Any(q => q.Id == request.UserId)) {
                throw new EntityNotFoundException("User");
            }

            Context.Orders.Add(new Domain.Order {
                UserId = request.UserId
            });

            Context.SaveChanges();

        }
    }
}
