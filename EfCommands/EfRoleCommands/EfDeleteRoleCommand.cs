using Application.Commands.RoleCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfRoleCommands {
    public class EfDeleteRoleCommand : BaseEfCommand, IDeleteRoleCommand {
        public EfDeleteRoleCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(int request) {

            var role = Context.Roles.Find(request);

            if(role is null || role.IsDeleted is true) {
                throw new EntityNotFoundException("Role");
            }

            role.IsDeleted = true;

            Context.SaveChanges();

        }
    }
}
