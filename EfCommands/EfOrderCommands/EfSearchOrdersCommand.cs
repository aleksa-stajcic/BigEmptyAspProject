using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.OrderCommands;
using Application.DataTransfer.OrderDto;
using Application.Responses;
using Application.Searches;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EfCommands.EfOrderCommands {
    public class EfSearchOrdersCommand : BaseEfCommand, ISearchOrderCommand {
        public EfSearchOrdersCommand(BigEmptyContext context) : base(context) {
        }

        public PagedResponse<GetOrderDto> Execute(OrderSearch request) {

            var query = Context.Orders.Where(q => q.IsDeleted != true).AsQueryable();
                                

            if(request.OrderId != null) {
                query = query.Where(q => q.Id == request.OrderId);
            }

            if(request.UserId != null) {
                query = query.Where(q => q.UserId == request.UserId);
            }

            var total_count = query.Count();
            var skip = (request.PageNumber - 1) * request.PerPage;

            query = query.Include(q => q.OrderDetails)
                                .ThenInclude(q => q.Product)
                                .Skip(skip)
                                .Take(request.PerPage);

            var pages_count = (int)Math.Ceiling((double)total_count / request.PerPage);

            var orders = new PagedResponse<GetOrderDto> {
                Data = query.Select(q => new GetOrderDto {
                    UserId = q.UserId,
                    OrderId = q.Id,
                    TotalPrice = q.OrderDetails.Sum(od => od.UnitPrice * od.Quantity),
                    OrderDetails = q.OrderDetails.Select(od => new GetOrderDetailsDto {
                        OrderId = od.OrderId,
                        ProductId = od.ProductId,
                        UnitPrice = od.UnitPrice,
                        Quantity = od.Quantity,
                        ProductName = od.Product.Name
                    })
                }),
                TotalCount = total_count,
                PagesCount = pages_count,
                CurrentPage = request.PageNumber
            };

            return orders;

        }
    }
}
