using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.ManufacturerCommands;
using Application.DataTransfer.ManufacturerDto;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase {

        private readonly ICreateManufacturerCommand _create;
        private readonly IDeleteManufacturerCommand _delete;
        private readonly IEditManufacturerCommand _edit;

        public ManufacturersController(ICreateManufacturerCommand create, IDeleteManufacturerCommand delete, IEditManufacturerCommand edit) {
            _create = create;
            _delete = delete;
            _edit = edit;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}", Name = "Get Manufacturer")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Manufacturers
        [HttpPost]
        public IActionResult Post([FromBody] CreateManufacturerDto dto) {

            try {

                _create.Execute(dto);

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/Manufacturers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditManufacturerDto dto) {

            dto.Id = id;

            try {

                _edit.Execute(dto);

                return StatusCode(StatusCodes.Status201Created);

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

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
