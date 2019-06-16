using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.OrderCommands;
using Application.DataTransfer.OrderDto;
using Application.Exceptions;
using DataAccess;

namespace EfCommands.EfOrderCommands {
    public class EfCreateOrderDetailsCommand : BaseEfCommand, ICreateOrderDetailsCommand {
        public EfCreateOrderDetailsCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateOrderDetailsDto request) {
            
            if(!Context.Orders.Any(q => q.Id == request.OrderId)) {
                throw new EntityNotFoundException("Order");
            }

            if(!Context.Products.Any(q => q.Id == request.ProductId)) {
                throw new EntityNotFoundException("Product");
            }


            var details = new Domain.OrderDetail {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                UnitPrice = Context.Products.Find(request.ProductId).Price,
                Quantity = request.Quantity
            };

            Context.OrderDetails.Add(details);

            Context.SaveChanges();

        }
    }
}
