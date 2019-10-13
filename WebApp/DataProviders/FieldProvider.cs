using System;
using System.Collections.Generic;
using Npgsql;
using Dapper;
using WebApp.Models;

namespace WebApp.DataProviders
{
    public class FieldProvider : BaseProvider<Field>
    {
        /*
        private readonly NpgsqlConnection Connection;
        public FieldProvider() { this.Connection = new NpgsqlConnection(Program.sConnectionString); }
        public FieldProvider(NpgsqlConnection connection) { this.Connection = connection; }
        */
        override public Exception GetAll(out IEnumerable<Field> _fields)
        {
            _fields = null;
            try
            {
                /*
                _fields = Connection.GetAll<Field>();
                foreach (var field in _fields)
                    field.CompanyEntity = Connection.QueryFirstOrDefault<Company>("select * from company where \"Id\" = @Id", new { Id = field.CompanyId });
                    */
                _fields = Connection.Query<Field, Company, Field>(
                    "SELECT * FROM field f INNER JOIN company c ON f.company_id = c.id",
                    (field, company) =>
                    {
                        field.CompanyEntity = company;
                        //anti-split
                        field.CompanyId = company.Id;
                        return field;
                    },
                    splitOn: "id");

                return null;
            }
            catch (NpgsqlException nsEx)
            {
                return nsEx;
            }
        }
        override public Exception Get(out Field _field, long _id)
        {
            /*
            Field field = Connection.QueryFirstOrDefault<Field>("select * from field where \"Id\" = @Id", new { Id = id });
            field.CompanyEntity = Connection.QueryFirstOrDefault<Company>("select * from company where \"Id\" = @Id", new { Id = field.CompanyId });
            return field;
            */
            _field = null;
            try
            {
                _field = Connection.Query<Field, Company, Field>(
                    "SELECT * FROM field f INNER JOIN company c ON f.company_id = c.id where f.id = @id",
                    (field, company) =>
                    {
                        field.CompanyEntity = company;
                        return field;
                    },
                    new { id = _id },
                    splitOn: "id").AsList()[0];

                return null;
            }
            catch (NpgsqlException nsEx)
            {
                return nsEx;
            }
        }
        override public Exception Set(Field _field)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<Field>(
                    "update field set name = @name, reserve = @reserve, company_id = @companyId where id = @id",
                    new { id = _field.Id, name = _field.Name, reserve = _field.Reserve, companyId = _field.CompanyId }
                    );
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Add(out long _id, Field _field)
        {
            _id = 0;
            try
            {
                _id = Connection.QueryFirstOrDefault<long>(
                    "insert into field (name, reserve, company_id) values (@name, @reserve, @companyId) returning id",
                    new { id = _field.Id, name = _field.Name, reserve = _field.Reserve, companyId = _field.CompanyId }
                    );

                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Del(long _id)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("delete from field where id = @id",
                    new { id = _id });
                return null;

            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception DelAll()
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("delete from field");
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
    }
}
