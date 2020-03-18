using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.UserCommands;
using Application.DataTransfer.UserDto;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/**
 Testing git and atom
 Another atom test
 Yet another atom test
*/

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly ICreateUserCommand _create;
        private readonly IDeleteUserCommand _delete;
        private readonly IEditUserCommand _edit;
        private readonly ISearchUsersCommand _search;
        private readonly IGetUserCommand _get;

        public UsersController(ICreateUserCommand create, IDeleteUserCommand delete, IEditUserCommand edit, ISearchUsersCommand search, IGetUserCommand get) {
            _create = create;
            _delete = delete;
            _edit = edit;
            _search = search;
            _get = get;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<PagedResponse<GetUsersDto>> Get([FromQuery]UserSearch search) {

            try {

                var result = _search.Execute(search);
                return Ok(result);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get User")]
        public ActionResult<GetUsersDto> Get(int id) {

            try {

                var user = _get.Execute(id);

                return Ok(user);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST: api/Users
        [HttpPost]
        public ActionResult Post([FromBody] CreateUserDto dto) {

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
        public ActionResult Put(int id, [FromBody] EditUserDto dto) {

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
        public ActionResult Delete(int id) {

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
