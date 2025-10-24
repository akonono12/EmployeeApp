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
    public record GetAll : IRequest<List<Employee>>;

    public class GetAllHandler : IRequestHandler<GetAll, List<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<List<Employee>> Handle(GetAll request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
