using Application.DataTransfer.UserDto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.UserCommands {
    public interface ICreateUserCommand : ICommand<CreateUserDto> {
    }
}
