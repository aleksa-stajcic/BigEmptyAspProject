using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.UserCommands;
using Application.DataTransfer.UserDto;
using Application.Exceptions;
using DataAccess;

namespace EfCommands.EfUserCommands {
    public class EfGetUserCommand : BaseEfCommand, IGetUserCommand {
        public EfGetUserCommand(BigEmptyContext context) : base(context) {
        }

        public GetUsersDto Execute(int request) {

            var user = Context.Users.Find(request);

            if(user is null || user.IsDeleted is true) {
                throw new EntityNotFoundException("User");
            }

            return new GetUsersDto {
                Role = user.Role.Name,
                Username = user.Username,
                Id = user.Id
            };

        }
    }
}
