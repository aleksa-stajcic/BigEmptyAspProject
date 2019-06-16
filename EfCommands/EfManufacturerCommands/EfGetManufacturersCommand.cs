using Application.Commands.ManufacturerCommands;
using Application.DataTransfer.ManufacturerDto;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfManufacturerCommands {
    public class EfGetManufacturersCommand : BaseEfCommand, IGetManufacturersCommand {
        public EfGetManufacturersCommand(BigEmptyContext context) : base(context) {
        }

        public IEnumerable<GetManufacturersDto> Execute() {
            return Context.Manufacturers.Where(q => q.IsDeleted == false).Select(q => new GetManufacturersDto {
                Id = q.Id,
                Name = q.Name
            }).ToList();
        }
    }
}
