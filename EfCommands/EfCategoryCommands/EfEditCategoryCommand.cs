using Application.Commands.CategoryCommands;
using Application.DataTransfer.CategoryDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCategoryCommands {
    public class EfEditCategoryCommand : BaseEfCommand, IEditCategoryCommand {

        public EfEditCategoryCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(EditCategoryDto request) {

            var category = Context.Categories.Find(request.Id);

            if(category is null) {
                throw new EntityNotFoundException("Category");
            }

            if(Context.Categories.Any(q => q.Name == request.Name && q.Id != request.Id)) {
                throw new EntityAlreadyExistsException("Category");
            }

            category.Name = request.Name;


            category.IsDeleted = request.IsDeleted;
            category.Description = request.Description;
            category.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
