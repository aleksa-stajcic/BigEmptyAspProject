using Application.Commands.RoleCommands;
using Application.DataTransfer.RoleDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfRoleCommands {
    public class EfEditRoleCommand : BaseEfCommand, IEditRoleCommand {
        public EfEditRoleCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(EditRoleDto request) {

            var role = Context.Roles.Find(request.Id);

            if(role is null) {
                throw new EntityNotFoundException("Role");
            }

            if(Context.Roles.Any(q => q.Name == request.Name && q.Id != request.Id)) {
                throw new EntityAlreadyExistsException("Role");
            }

            role.Name = request.Name;
            role.IsDeleted = request.IsDeleted;
            role.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
