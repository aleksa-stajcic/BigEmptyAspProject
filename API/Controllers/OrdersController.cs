using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.OrderCommands;
using Application.DataTransfer.OrderDto;
using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {

        private readonly ICreateOrderCommand _create;
        private readonly ISearchOrderCommand _search;

        private readonly IEmailSender _sender;

        public OrdersController(ICreateOrderCommand create, ISearchOrderCommand search, IEmailSender sender) {
            _create = create;
            _search = search;
            _sender = sender;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<PagedResponse<GetOrderDto>> Get([FromBody]OrderSearch search) {

            try {

                var response = _search.Execute(search);

                return Ok(response);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);

            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get Order")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Orders
        [HttpPost]
        public ActionResult Post([FromBody] CreateOrderDto dto, [FromBody][EmailAddress]string email) {

            try {

                _create.Execute(dto);

                if(email != null) {
                    _sender.Subject = "Order creation.";
                    _sender.ToEmail = email;
                    _sender.Body = "Order successfully created.";
                    _sender.Send();
                }

                return StatusCode(StatusCodes.Status201Created);

            } catch(EntityNotFoundException e) {

                return NotFound(e.Message);
            } catch(Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
