using Application.DataTransfer.CategoryDto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.CategoryCommands {
    public interface IGetCategoriesCommand : IGetEntitiesCommand<IEnumerable<GetCategoryDto>> {
    }
}
