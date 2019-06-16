using Application.Commands.RoleCommands;
using Application.DataTransfer.RoleDto;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfRoleCommands {
    public class EfGetRolesCommand : BaseEfCommand, IGetRolesCommand {
        public EfGetRolesCommand(BigEmptyContext context) : base(context) {
        }

        public IEnumerable<GetRolesDto> Execute() {

            return Context.Roles.Select(q => new GetRolesDto {
                Id = q.Id,
                Name = q.Name
            });

        }
    }
}
