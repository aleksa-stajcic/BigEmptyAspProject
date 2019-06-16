using Application.DataTransfer.ProductDto;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ProductCommands {
    public interface ISearchProductsCommand : ICommand<ProductSearch, PagedResponse<SearchProductsDto>> {
    }
}
