using Application.Commands.ManufacturerCommands;
using Application.DataTransfer.ManufacturerDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfManufacturerCommands {
    public class EfEditManufacturerCommand : BaseEfCommand, IEditManufacturerCommand {
        public EfEditManufacturerCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(EditManufacturerDto request) {

            var manufacturer = Context.Manufacturers.Find(request.Id);

            if(manufacturer is null) {
                throw new EntityNotFoundException("Manufacturer");
            }

            if(Context.Manufacturers.Any(q => q.Name == request.Name && q.Id != request.Id)) {
                throw new EntityAlreadyExistsException("Manufacturer");
            }

            manufacturer.Name = request.Name;
            manufacturer.IsDeleted = request.IsDeleted;
            manufacturer.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
