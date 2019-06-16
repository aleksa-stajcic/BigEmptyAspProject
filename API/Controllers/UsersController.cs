using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.UserCommands;
using Application.DataTransfer.UserDto;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly ICreateUserCommand _create;
        private readonly IDeleteUserCommand _delete;
        private readonly IEditUserCommand _edit;

        public UsersController(ICreateUserCommand create, IDeleteUserCommand delete, IEditUserCommand edit) {
            _create = create;
            _delete = delete;
            _edit = edit;
        }





        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get User")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserDto dto) {

            try {

                _create.Execute(dto);

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditUserDto dto) {

            dto.Id = id;

            try {

                _edit.Execute(dto);

                return NoContent();

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {

            try {

                _delete.Execute(id);

                return NoContent();

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }
    }
}
