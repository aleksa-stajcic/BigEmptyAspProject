using Application.Commands.UserCommands;
using Application.DataTransfer.UserDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUserCommands {
    public class EfCreateUserCommand : BaseEfCommand, ICreateUserCommand {
        public EfCreateUserCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateUserDto request) {

            var users = Context.Users;

            if(users.Any(q => q.Username == request.Username)) {
                throw new EntityAlreadyExistsException("User");
            }

            if(users.Any(q => q.Email == request.Email)) {
                throw new EntityAlreadyExistsException("User");
            }

            if(!users.Any(q => q.Id == request.RoleId)) {
                throw new EntityNotFoundException("Role");
            }

            var password = new Domain.Password {
                Hidden = request.Password
            };



            users.Add(new Domain.User {
                Email = request.Email,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = password,
                RoleId = request.RoleId
            });

            Context.SaveChanges();

        }
    }
}
