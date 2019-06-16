using Application.DataTransfer.ManufacturerDto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ManufacturerCommands {
    public interface IGetManufacturersCommand : IGetEntitiesCommand<IEnumerable<GetManufacturersDto>> {
    }
}
