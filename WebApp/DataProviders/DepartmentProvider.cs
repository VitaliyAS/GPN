using System;
using System.Collections.Generic;
using Npgsql;
using Dapper;
using WebApp.Models;

namespace WebApp.DataProviders
{
    public class DepartmentProvider : BaseProvider<Department>
    {
        /*
        private readonly NpgsqlConnection Connection;
        public DepartmentProvider() { this.Connection = new NpgsqlConnection(Program.sConnectionString); }
        public DepartmentProvider(NpgsqlConnection _Connection) { this.Connection = _Connection; }
        */
        override public Exception GetAll(out IEnumerable<Department> _departments)
        {
            _departments = null;
            try
            {
                /*
                _departments = Connection.GetAll<Department>();
                foreach (var department in _departments)
                    department.CompanyEntity = Connection.QueryFirstOrDefault<Company>("select * from company where id = @id", new { id = department.CompanyId });
                */
                _departments = Connection.Query<Department, Company, Department>(
                    "SELECT d.*, c.* FROM department d INNER JOIN company c ON d.company_id = c.id",
                    (department, company) =>
                        {
                            department.CompanyEntity = company;
                            //anti-split
                            department.CompanyId = company.Id;
                            return department;
                        },
                    splitOn: "company_id"
                    );

                return null;
            }
            catch (NpgsqlException nsEx)
            {
                return nsEx;
            }
        }
        override public Exception Get(out Department _department, long _id)
        {
            _department = null;
            try
            {
                /*
                _department = Connection.QueryFirstOrDefault<Department>("select * from department where id = @id", 
                    new { id = _id });
                _department.CompanyEntity = Connection.QueryFirstOrDefault<Company>("select * from company where Id = @id", 
                    new { id = _department.CompanyId });
                 */
                _department = Connection.Query<Department, Company, Department>(
                   "SELECT d.*, c.id company_id, c.name FROM department d INNER JOIN company c ON d.company_id = c.id where d.id = @id",
                   (department, company) =>
                   {
                       department.CompanyEntity = company;
                       return department;
                   },
                   new { id = _id },
                   splitOn: "company_id").AsList()[0];

                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Set(Department _department)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<Department>(
                    "update department set name = @name, boss = @boss, company_id = @companyId where id = @id",
                    new { id = _department.Id, name = _department.Name, boss = _department.Boss, companyId = _department.CompanyId }
                    );
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Add(out long _id, Department _department)
        {
            _id = 0;
            try
            {
                _id = Connection.QueryFirstOrDefault<long>(
                    "insert into department (name, boss, company_id) values (@name, @boss, @companyId) returning id",
                    new { id = _department.Id, name = _department.Name, boss = _department.Boss, companyId = _department.CompanyId }
                    );

                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Del(long _id)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("delete from department where id = @id",
                    new { id = _id });
                return null;

            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception DelAll()
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("delete from department");
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
    }
}
