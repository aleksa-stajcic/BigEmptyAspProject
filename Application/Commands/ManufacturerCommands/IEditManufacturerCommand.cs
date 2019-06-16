using Application.DataTransfer.ManufacturerDto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ManufacturerCommands {
    public interface IEditManufacturerCommand : ICommand<EditManufacturerDto> {
    }
}
