using System;
using System.Collections.Generic;
using Npgsql;
using Dapper;
using WebApp.Models;

namespace WebApp.DataProviders
{
    public class BoreholeProvider : BaseProvider<Borehole>
    {
        /*
        private readonly NpgsqlConnection Connection;
        public BoreholeProvider() { this.Connection = new NpgsqlConnection(Program.sConnectionString); }
        public BoreholeProvider(NpgsqlConnection connection) { this.Connection = connection; }
        */
        override public Exception GetAll(out IEnumerable<Borehole> _boreholes)
        {
            _boreholes = null;
            try
            {
                /*
                _boreholes = Connection.GetAll<Borehole>();
                foreach (var borehole in _boreholes)
                {
                    borehole.CompanyEntity = Connection.QueryFirstOrDefault<Company>("select * from company where \"Id\" = @Id", new { Id = borehole.CompanyId });
                    borehole.DepartmentEntity = Connection.QueryFirstOrDefault<Department>("select * from department where \"Id\" = @Id", new { Id = borehole.DepartmentId });
                    borehole.FieldEntity = Connection.QueryFirstOrDefault<Field>("select * from field where \"Id\" = @Id", new { Id = borehole.FieldId });
                }
                */
                _boreholes = Connection.Query<Borehole, Company, Department, Field, Borehole>(
                    "SELECT * FROM borehole b INNER JOIN company c ON b.company_id = c.id " +
                        "INNER JOIN department d ON b.department_id = d.id INNER JOIN field f ON b.field_id = f.id",
                    (borehole, company, department, field) =>
                    {
                        borehole.CompanyEntity = company;
                        borehole.DepartmentEntity = department;
                        borehole.FieldEntity = field;
                        //anti-split
                        borehole.CompanyId = company.Id;
                        borehole.DepartmentId = department.Id;
                        borehole.FieldId = field.Id;
                        return borehole;
                    },
                    splitOn: "id");

                return null;
            }
            catch (NpgsqlException nsEx)
            {
                return nsEx;
            }
        }
        override public Exception Get(out Borehole _borehole, long _id)
        {
            /*
            var borehole = Connection.QueryFirstOrDefault<Borehole>("select * from borehole where \"Id\" = @Id", new { Id = id });
            borehole.CompanyEntity = Connection.QueryFirstOrDefault<Company>("select * from company where \"Id\" = @Id", new { Id = borehole.CompanyId });
            borehole.DepartmentEntity = Connection.QueryFirstOrDefault<Department>("select * from department where \"Id\" = @Id", new { Id = borehole.DepartmentId });
            borehole.FieldEntity = Connection.QueryFirstOrDefault<Field>("select * from field where \"Id\" = @Id", new { Id = borehole.FieldId });
            return borehole;
            */
            _borehole = null;
            try
            {
                _borehole = Connection.Query<Borehole, Company, Department, Field, Borehole>(
                    "SELECT * FROM borehole b INNER JOIN company c ON b.company_id = c.id " +
                        "INNER JOIN department d ON b.department_id = d.id INNER JOIN field f ON b.field_id = f.id" +
                        "WHERE b.id = @id",
                    (borehole, company, department, field) =>
                    {
                        borehole.CompanyEntity = company;
                        borehole.DepartmentEntity = department;
                        borehole.FieldEntity = field;
                        return borehole;
                    },
                    new {id = _id},
                    splitOn: "id").AsList()[0];

                return null;
            }
            catch (NpgsqlException nsEx)
            {
                return nsEx;
            }
        }
        override public Exception Set(Borehole _borehole)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<Borehole>(
                    "UPDATE borehole SET name = @name, depth = @depth, " + 
                        "company_id = @companyId, department_id = @departmentId, field_id = @fieldId WHERE id = @id",
                    new
                    {
                        id = _borehole.Id,
                        name = _borehole.Name,
                        depth = _borehole.Depth,
                        companyId = _borehole.CompanyId,
                        departmentId = _borehole.DepartmentId,
                        fieldId = _borehole.FieldId
                    });
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Add(out long _id, Borehole _borehole)
        {
            _id = 0;
            try
            {
                _id = Connection.QueryFirstOrDefault<long>(
                    "INSERT INTO borehole (name, depth, company_id, department_id, field_id) " +
                        "values (@name, @depth, @companyId, @departmentId, @fieldId) returning id",
                    new
                    {
                        id = _borehole.Id,
                        name = _borehole.Name,
                        depth = _borehole.Depth,
                        companyId = _borehole.CompanyId,
                        departmentId = _borehole.DepartmentId,
                        fieldId = _borehole.FieldId
                    });

                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Del(long _id)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("DELETE FROM borehole WHERE id = @id",
                    new { id = _id });
                return null;

            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception DelAll()
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("DELETE FROM borehole");
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
    }
}
