using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CategoryCommands;
using Application.DataTransfer.CategoryDto;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {

        private readonly ICreateCategoryCommand _create;
        private readonly IDeleteCategoryCommand _delete;
        private readonly IEditCategoryCommand _edit;

        public CategoriesController(ICreateCategoryCommand createCategory, IDeleteCategoryCommand deleteCategory, IEditCategoryCommand editCategory) {
            _create = createCategory;
            _delete = deleteCategory;
            _edit = editCategory;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Categories
        [HttpPost]
        public ActionResult Post([FromQuery] CreateCategoryDto dto) {

            try {

                _create.Execute(dto);

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditCategoryDto dto) {

            dto.Id = id;

            try {

                _edit.Execute(dto);

                return NoContent();

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

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
