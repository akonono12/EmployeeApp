using Dapper;
using EmployeeApp.Data.Employee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = EmployeeApp.Data.Employee.Entities.Employee;
namespace EmployeeApp.Data.Employee.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Entity>> GetAll();
        Task<Entity?> GetById(Guid id);
        Task<Guid> Add(Entity employee);
        Task<bool> Update(Entity employee);
        Task<bool> Delete(Guid id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEmployeeDbContext _employeeDbContext;

        public EmployeeRepository(IEmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<List<Entity>> GetAll()
        {
            _employeeDbContext.OpenConnection();
            try
            {
                var employees = (await _employeeDbContext
                    .GetDbConnection()
                    .QueryAsync<Entity>("SELECT * FROM Employee"))
                    .ToList();

                return employees;
            }
            finally
            {
                _employeeDbContext.CloseConnection();
            }
        }

        public async Task<Entity?> GetById(Guid id)
        {
            _employeeDbContext.OpenConnection();
            try
            {
                var employee = await _employeeDbContext
                    .GetDbConnection()
                    .QueryFirstOrDefaultAsync<Entity>(
                        "SELECT TOP 1 * FROM Employee WHERE Id = @Id",
                        new { Id = id });

                return employee;
            }
            finally
            {
                _employeeDbContext.CloseConnection();
            }
        }

        public async Task<Guid> Add(Entity employee)
        {
            _employeeDbContext.OpenConnection();
            try
            {
                employee.Id = Guid.NewGuid();

                var sql = @"INSERT INTO Employee (Id, Firstname, Lastname) 
                        VALUES (@Id, @Firstname, @Lastname)";

                await _employeeDbContext.GetDbConnection().ExecuteAsync(sql, employee);

                return employee.Id;
            }
            finally
            {
                _employeeDbContext.CloseConnection();
            }
        }

        public async Task<bool> Update(Entity employee)
        {
            _employeeDbContext.OpenConnection();
            try
            {
                var sql = @"UPDATE Employee 
                        SET Firstname = @Firstname, Lastname = @Lastname 
                        WHERE Id = @Id";

                var rowsAffected = await _employeeDbContext.GetDbConnection().ExecuteAsync(sql, employee);

                return rowsAffected > 0;
            }
            finally
            {
                _employeeDbContext.CloseConnection();
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            _employeeDbContext.OpenConnection();
            try
            {
                var sql = "DELETE FROM Employee WHERE Id = @Id";

                var rowsAffected = await _employeeDbContext.GetDbConnection().ExecuteAsync(sql, new { Id = id });

                return rowsAffected > 0;
            }
            finally
            {
                _employeeDbContext.CloseConnection();
            }
        }
    }
}
