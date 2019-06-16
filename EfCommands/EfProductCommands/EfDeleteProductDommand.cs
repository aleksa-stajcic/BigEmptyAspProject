using Application.Commands.ProductCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfProductCommands {
    public class EfDeleteProductDommand : BaseEfCommand, IDeleteProductCommand {
        public EfDeleteProductDommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(int request) {

            var product = Context.Products.Find(request);

            if(product is null) {
                throw new EntityNotFoundException("Product");
            }

            if(product.IsDeleted is true) {
                throw new EntityNotFoundException("Product");
            }

            var image = Context.ProductImages.Where(q => q.ProductId == product.Id).FirstOrDefault();

            product.IsDeleted = true;

            if(image != null) {
                image.IsDeleted = true;
            }

            Context.SaveChanges();

        }
    }
}
