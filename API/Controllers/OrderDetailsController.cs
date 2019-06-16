using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.OrderCommands;
using Application.DataTransfer.OrderDto;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase {

        private readonly ICreateOrderDetailsCommand _create;

        public OrderDetailsController(ICreateOrderDetailsCommand create) {
            _create = create;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}", Name = "Get OrderDetails")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/OrderDetails
        [HttpPost]
        public ActionResult Post([FromBody] CreateOrderDetailsDto dto) {

            try {

                _create.Execute(dto);

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);
            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/OrderDetails/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
