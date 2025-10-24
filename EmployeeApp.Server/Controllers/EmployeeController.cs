using EmployeeApp.Application.Employees.Commands;
using EmployeeApp.Application.Employees.Queries;
using EmployeeApp.Data.Employee.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _mediator.Send(new GetAll());
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _mediator.Send(new GetById(id));
            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
            var id = await _mediator.Send(new Add(employee));
            return CreatedAtAction(nameof(GetById), new { id }, employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
                return BadRequest("Mismatched employee ID");

            var success = await _mediator.Send(new Update(employee));
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new Delete(id));
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
