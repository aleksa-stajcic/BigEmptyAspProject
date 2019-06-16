using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Exceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EfCommands.EfProductCommands {
    public class EfEditProductCommand : BaseEfCommand, IEditProductCommand {
        public EfEditProductCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(EditProductDto request) {

            var product = Context.Products.Find(request.Id);

            if(product is null) {
                throw new EntityNotFoundException("Product");
            }

            if(product.IsDeleted is true) {
                throw new EntityNotFoundException("Product");
            }

            if(Context.Products.Any(q => q.Name == request.Name && q.Id != request.Id)) {
                throw new EntityAlreadyExistsException("Product");
            }

            if(!Context.Categories.Any(q => q.Id == request.CategoryId)) {
                throw new EntityNotFoundException("Category");
            }

            if(!Context.Manufacturers.Any(q => q.Id == request.ManufacturerId)) {
                throw new EntityNotFoundException("Manufacturer");
            }

            product.Name = request.Name;
            product.ManufacturerId = request.ManufacturerId;
            product.CategoryId = request.CategoryId;
            product.IsDeleted = request.IsDeleted;
            product.Price = request.Price;
            product.Description = request.Description;
            product.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
