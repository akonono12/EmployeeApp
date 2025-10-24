using EmployeeApp.Data.Employee.Entities;
using EmployeeApp.Data.Employee.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Application.Employees.Queries
{
    public record GetById(Guid Id) : IRequest<Employee?>;
    public class GetByIdHandler : IRequestHandler<GetById, Employee?>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetByIdHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee?> Handle(GetById request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetById(request.Id);
        }
    }
}
