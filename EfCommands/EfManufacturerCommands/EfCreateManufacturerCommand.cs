using Application.Commands.ManufacturerCommands;
using Application.DataTransfer.ManufacturerDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfManufacturerCommands {
    public class EfCreateManufacturerCommand : BaseEfCommand, ICreateManufacturerCommand {
        public EfCreateManufacturerCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateManufacturerDto request) {
            
            if(Context.Manufacturers.Any(q => q.Name == request.Name)) {
                throw new EntityAlreadyExistsException("Manufacturer");
            }

            Context.Manufacturers.Add(new Domain.Manufacturer {
                Name = request.Name
            });

            Context.SaveChanges();
        }
    }
}
