using Application.Commands.CategoryCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCategoryCommands {
    public class EfDeleteCategoryCommand : BaseEfCommand, IDeleteCategoryCommand {
        public EfDeleteCategoryCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(int request) {

            if(!Context.Categories.Any(q => q.Id == request)) {
                throw new EntityNotFoundException("Category");
            }

            var category = Context.Categories.Find(request);

            if(category.IsDeleted is true) {
                throw new EntityNotFoundException("Category");
            }

            category.IsDeleted = true;

            var products = Context.Products.Where(q => q.CategoryId == request).ToList();

            if(products.Count > 0) {
                foreach(var item in products) {
                    item.IsDeleted = true;
                }
            }

            Context.SaveChanges();
        }
    }
}
