using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Data.Employee
{
    public interface IEmployeeDbContext
    {
        void OpenConnection();
        void CloseConnection();
        IDbConnection GetDbConnection();
    }

    public class EmployeeDbContext: IEmployeeDbContext
    {
        private string connectionString = "Server=tcp:employee-test-svr.database.windows.net,1433;Initial Catalog=EmployeeDatabase;Persist Security Info=False;User ID=dboadmin;Password=System#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"; 
        private IDbConnection DbConnection { get; set; }
        public EmployeeDbContext()
        {
            DbConnection = new SqlConnection(connectionString);
            CreateEmployeeIfNone();
        }

        public void OpenConnection()
        {
            DbConnection.Open();
        }

        public void CloseConnection()
        {
            DbConnection.Close();
        }

        public IDbConnection GetDbConnection()
        {
            return DbConnection;
        }

        private void CreateEmployeeIfNone()
        {
            OpenConnection();

            var sql = @"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Employee')
            BEGIN
                CREATE TABLE Employee
                (
                    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    Firstname NVARCHAR(50) NOT NULL,
                    Lastname NVARCHAR(50) NOT NULL
                )
            END";

            DbConnection.Execute(sql);

            CloseConnection();
        }
    }
}
