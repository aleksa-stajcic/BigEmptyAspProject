using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Exceptions;
using DataAccess;

namespace EfCommands.EfProductCommands {
    public class EfGetProductCommand : BaseEfCommand, IGetProductCommand {
        public EfGetProductCommand(BigEmptyContext context) : base(context) {
        }

        public SearchProductsDto Execute(int request) {

            var product = Context.Products.Find(request);

            if(product is null || product.IsDeleted is true) {
                throw new EntityNotFoundException("Product");
            }

            var image = Context.ProductImages.Where(q => q.ProductId == request).FirstOrDefault();

            var category = Context.Categories.Where(q => q.Id == product.CategoryId).FirstOrDefault();

            var manufacturer = Context.Manufacturers.Where(q => q.Id == product.ManufacturerId).FirstOrDefault();

            return new SearchProductsDto {
                Id = product.Id,
                Category = category.Name,
                Manufacturer = manufacturer.Name,
                Name = product.Name,
                Price = product.Price,
                ImageName = image.FileName,
                ImageAlt = image.Alt,
                Description = product.Description,
                Comments = Context.Comments.Where(q => q.ProductId == product.Id).Select(q => new CreateCommentDto {
                    Text = q.Text,
                    UserId = q.UserId,
                     ProductId = q.ProductId
                })
            };

        }
    }
}
