using Application.Commands.ManufacturerCommands;
using Application.Exceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfManufacturerCommands {
    public class EfDeleteManufacturerCommand : BaseEfCommand, IDeleteManufacturerCommand {
        public EfDeleteManufacturerCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(int request) {

            if(!Context.Manufacturers.Any(q => q.Id == request)) {
                throw new EntityNotFoundException("Manufacturer");
            }

            var manufacturer = Context.Manufacturers.Find(request);

            if(manufacturer.IsDeleted is true) {
                throw new EntityNotFoundException("Manufacturer");
            }

            manufacturer.IsDeleted = true;

            var products = Context
                            .Products
                            .Where(q => q.Manufacturer == manufacturer)
                            .Include(q => q.ProductImage)
                            .ToList();

            if(products.Count > 0) {
                foreach(var item in products) {
                    if(item.ProductImage != null) {
                        item.ProductImage.IsDeleted = true;
                    }
                    item.IsDeleted = true;
                }
            }

            Context.SaveChanges();

        }
    }
}
