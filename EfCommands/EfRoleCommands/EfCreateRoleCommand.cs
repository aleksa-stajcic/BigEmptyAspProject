using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.RoleCommands;
using Application.DataTransfer.RoleDto;
using Application.Exceptions;
using DataAccess;

namespace EfCommands.EfRoleCommands {
    public class EfCreateRoleCommand : BaseEfCommand, ICreateRoleCommand {
        public EfCreateRoleCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateRoleDto request) {
            if(Context.Roles.Any(q => q.Name == request.Name)) {
                throw new EntityAlreadyExistsException("Role");
            }

            Context.Roles.Add(new Domain.Role {
                Name = request.Name
            });

            Context.SaveChanges();

        }
    }
}
