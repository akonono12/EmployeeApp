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
    public record Update(Employee Employee) : IRequest<bool>;

    public class UpdateHandler : IRequestHandler<Update, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(Update request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.Update(request.Employee);
        }
    }
}
