using EmployeeApp.Data.Employee.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Application.Employees.Commands
{
    public record Delete(Guid Id) : IRequest<bool>;

    public class DeleteHandler : IRequestHandler<Delete, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(Delete request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.Delete(request.Id);
        }
    }
}
