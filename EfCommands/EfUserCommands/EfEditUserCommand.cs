using Application.Commands.UserCommands;
using Application.DataTransfer.UserDto;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUserCommands {
    public class EfEditUserCommand : BaseEfCommand, IEditUserCommand {
        public EfEditUserCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(EditUserDto request) {

            var user = Context.Users.Find(request.Id);

            if(user is null) {
                throw new EntityNotFoundException("User");
            }

            if(Context.Users.Any(q => q.Username == request.Username && q.Id != request.Id)) {
                throw new EntityAlreadyExistsException("User");
            }

            if(Context.Users.Any(q => q.Email == request.Email && q.Id != request.Id)) {
                throw new EntityAlreadyExistsException("User");
            }

            if(!Context.Roles.Any(q => q.Id == request.RoleId)) {
                throw new EntityNotFoundException("Role");
            }

            var password = Context.Passwords
                            .Where(q => q.UserId == request.Id)
                            .FirstOrDefault();

            user.Username = request.Username;
            user.Email = request.Email;
            user.RoleId = request.RoleId;
            user.IsDeleted = request.IsDeleted;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            password.Hidden = request.Password;
            user.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
