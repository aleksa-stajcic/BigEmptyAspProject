using Application.Commands.CategoryCommands;
using Application.DataTransfer.CategoryDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCategoryCommands {
    public class EfCreateCategoryCommand : BaseEfCommand, ICreateCategoryCommand {

        public EfCreateCategoryCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateCategoryDto request) {
            
            if(Context.Categories.Any(q => q.Name == request.Name)) {
                throw new EntityAlreadyExistsException("Category");
            }

            Context.Categories.Add(new Domain.Category {
                Name = request.Name,
                Description = request.Description
            });

            Context.SaveChanges();


        }
    }
}
