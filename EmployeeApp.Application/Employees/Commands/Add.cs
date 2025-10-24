using EmployeeApp.Data.Employee.Entities;
using EmployeeApp.Data.Employee.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Application.Employees.Commands
{
    public record Add(Employee employee):IRequest<Guid>;
    public class AddHandler : IRequestHandler<Add, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public AddHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Guid> Handle(Add request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.Add(request.employee);
        }
    }
}
