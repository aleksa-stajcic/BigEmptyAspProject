using Application.Commands.UserCommands;
using Application.DataTransfer.UserDto;
using Application.Responses;
using Application.Searches;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUserCommands {
    public class EfGetUsersCommand : BaseEfCommand, IGetUsersCommand {

        public EfGetUsersCommand(BigEmptyContext context) : base(context) {
        }

        public PagedResponse<GetUsersDto> Execute(UserSearch request) {

            var query = Context.Users.AsQueryable();

            if(request.Username != null) {
                var name = request.Username.ToLower().Trim();

                query = query.Where(q => q.Username.ToLower().Contains(name));
            }

            if(request.Email != null) {
                var email = request.Email.ToLower().Trim();

                query = query.Where(q => q.Email.ToLower().Contains(email));
            }

            var total_count = query.Count();
            var skip = (request.PageNumber - 1) * request.PerPage;

            query = query.Include(q => q.Role).Skip(skip).Take(request.PerPage);

            var pages_count = (int)Math.Ceiling((double)total_count / request.PerPage);

            var response = new PagedResponse<GetUsersDto> {
                CurrentPage = request.PageNumber,
                PagesCount = pages_count,
                TotalCount = total_count,
                Data = query.Select(q => new GetUsersDto {
                    Id = q.Id,
                    Role = q.Role.Name,
                    Username = q.Username
                })
            };

            return response;
        }
    }
}
