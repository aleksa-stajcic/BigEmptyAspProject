using Application.DataTransfer.ProductDto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ProductCommands {
    public interface IGetProductCommand : ICommand<int, SearchProductsDto> {
    }
}
