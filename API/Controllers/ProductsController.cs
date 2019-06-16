using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Exceptions;
using Application.Helpers;
using Application.Responses;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {

        private readonly ICreateProductCommand _create;
        private readonly IDeleteProductCommand _delete;
        private readonly IEditProductCommand _edit;
        private readonly ISearchProductsCommand _search;
        private readonly IGetProductCommand _get;

        public ProductsController(ICreateProductCommand create, IDeleteProductCommand delete, IEditProductCommand edit, ISearchProductsCommand search, IGetProductCommand get) {
            _create = create;
            _delete = delete;
            _edit = edit;
            _search = search;
            _get = get;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<PagedResponse<SearchProductsDto>> Get([FromQuery]ProductSearch search) {

            try {

               var result = _search.Execute(search);

                return Ok(result);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get Product")]
        public ActionResult<SearchProductsDto> Get(int id) {
            try {

                var result = _get.Execute(id);

                return Ok(result);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult Post([FromForm] InsertProduct p) {

            var extension = Path.GetExtension(p.Image.FileName);

            if(!FileUpload.AllowedExtensions.Contains(extension)) {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try {

                var new_file_name = Guid.NewGuid().ToString() + "_" + p.Image.FileName;

                var file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", new_file_name);

                var dto = new CreateProductDto {
                    Name = p.Name,
                    Image = new_file_name,
                    Description = p.Description,
                    ManufacturerId = p.ManufacturerId,
                    CategoryId = p.CategoryId,
                    Price = p.Price
                };

                _create.Execute(dto);

                p.Image.CopyTo(new FileStream(file_path, FileMode.Create));

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(EntityAlreadyExistsException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditProductDto dto) {

            dto.Id = id;

            try {

                _edit.Execute(dto);

                return NoContent();

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(EntityAlreadyExistsException e) {

                return UnprocessableEntity(e.Message);

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
