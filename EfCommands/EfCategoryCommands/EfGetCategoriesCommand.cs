using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.CategoryCommands;
using Application.DataTransfer.CategoryDto;
using DataAccess;

namespace EfCommands.EfCategoryCommands {
    public class EfGetCategoriesCommand : BaseEfCommand, IGetCategoriesCommand {
        public EfGetCategoriesCommand(BigEmptyContext context) : base(context) {
        }

        public IEnumerable<GetCategoryDto> Execute() {

            return Context.Categories.Where(q => q.IsDeleted == false).Select(q => new GetCategoryDto {
                Id = q.Id,
                Name = q.Name
            }).ToList();

        }
    }
}
