using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.RoleCommands;
using Application.DataTransfer.RoleDto;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase {

        private readonly ICreateRoleCommand _create;
        private readonly IEditRoleCommand _edit;
        private readonly IDeleteRoleCommand _delete;
        private readonly IGetRolesCommand _get;

        public RolesController(ICreateRoleCommand create, IEditRoleCommand edit, IDeleteRoleCommand delete, IGetRolesCommand get) {
            _create = create;
            _edit = edit;
            _delete = delete;
            _get = get;
        }




        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<GetRolesDto>> Get() {
            try {

                var response = _get.Execute();

                return Ok(response);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Roles/5
        [HttpGet("{id}", Name = "Get Role")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Roles
        [HttpPost]
        public ActionResult Post([FromBody] CreateRoleDto dto) {

            try {

                _create.Execute(dto);

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditRoleDto dto) {

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
