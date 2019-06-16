using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Responses;
using Application.Searches;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EfCommands.EfProductCommands {
    public class EfSearchProductsCommand : BaseEfCommand, ISearchProductsCommand {
        public EfSearchProductsCommand(BigEmptyContext context) : base(context) {
        }

        public PagedResponse<SearchProductsDto> Execute(ProductSearch request) {

            var query = Context.Products.Where(q => q.IsDeleted == false).AsQueryable();

            if(request.CategoryId != null) {
                query = query.Where(q => q.CategoryId == request.CategoryId);
            }

            if(request.ManufacturerId != null) {
                query = query.Where(q => q.ManufacturerId == request.ManufacturerId);
            }

            if(request.ProductName != null) {
                var name = request.ProductName.ToLower().Trim();
                query = query.Where(q => q.Name.ToLower().Trim().Contains(name));
            }

            if(request.MinPrice != null) {
                query = query.Where(q => q.Price >= request.MinPrice);
            }

            if(request.MaxPrice != null) {
                query = query.Where(q => q.Price <= request.MaxPrice);
            }

            var total_count = query.Count();
            var skip = (request.PageNumber - 1) * request.PerPage;

            query = query.Include(q => q.Category)
                            .Include(q => q.Manufacturer)
                            .Include(q => q.ProductImage)
                            .Skip(skip)
                            .Take(request.PerPage);

            var pages_count = (int)Math.Ceiling((double)total_count / request.PerPage);

            var response = new PagedResponse<SearchProductsDto> {
                CurrentPage = request.PageNumber,
                PagesCount = pages_count,
                TotalCount = total_count,
                Data = query.Select(q => new SearchProductsDto {
                    Id = q.Id,
                    Name = q.Name,
                    Category = q.Category.Name,
                    Manufacturer = q.Manufacturer.Name,
                    Price = q.Price,
                    ImageName = q.ProductImage.FileName,
                    ImageAlt = q.ProductImage.Alt,
                    Description = q.Description,
                    Comments = q.Comments.Select(c => new CreateCommentDto {
                        Text = c.Text,
                        UserId = c.UserId,
                        ProductId = c.ProductId
                    })
                })
            };

            return response;

        }
    }
}
