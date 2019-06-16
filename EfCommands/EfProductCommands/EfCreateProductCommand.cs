using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfProductCommands {
    public class EfCreateProductCommand : BaseEfCommand, ICreateProductCommand {
        public EfCreateProductCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateProductDto request) {

            var category = Context.Categories.Find(request.CategoryId);
            var manufacturer = Context.Manufacturers.Find(request.ManufacturerId);

            if(Context.Products.Any(q => q.Name == request.Name)) {
                throw new EntityAlreadyExistsException("Product");
            }

            if(category is null || category.IsDeleted is true) {
                throw new EntityNotFoundException("Category");
            }

            if(manufacturer is null || manufacturer.IsDeleted is true) {
                throw new EntityNotFoundException("Manufacturer");
            }

            var image = new Domain.ProductImage {
                FileName = request.Image,
                Alt = request.Name
            };

            Context.Products.Add(new Domain.Product {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                ManufacturerId = request.ManufacturerId,
                Price = request.Price,
                ProductImage = image
            });

            Context.SaveChanges();

        }
    }
}
