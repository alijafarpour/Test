using Mc2.CrudTest.Application.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.Application.Customer.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Customer.Commands.UpdateCustomer;
using Mc2.CrudTest.Application.Customer.Queries.GetAllCustomer;
using Mc2.CrudTest.Application.Customer.Queries.GetCustomerById;
using Mc2.CrudTest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    public class CustomersController : ApiControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await Mediator.Send(new GetAllCustomersQuery());
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<Customer> GetById(int id)
        {
            return await Mediator.Send(new GetCustomerByIdQuery { Id = id });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCustomerCommand(id));

            return NoContent();
        }
    }

}
