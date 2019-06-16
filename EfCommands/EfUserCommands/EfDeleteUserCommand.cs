using Application.Commands.UserCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUserCommands {
    public class EfDeleteUserCommand : BaseEfCommand, IDeleteUserCommand {
        public EfDeleteUserCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(int request) {

            var user = Context.Users.Find(request);

            if(user is null || user.IsDeleted is true) {
                throw new EntityNotFoundException("User");
            }

            var password = Context.Passwords.Where(q => q.UserId == request).FirstOrDefault();

            user.IsDeleted = true;
            password.IsDeleted = true;

            Context.SaveChanges();

        }
    }
}
