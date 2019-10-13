using System;
using System.Collections.Generic;
using Npgsql;
using Dapper;
using WebApp.Models;


namespace WebApp.DataProviders
{
    public class CompanyProvider:BaseProvider<Company>
    {
        /*private readonly NpgsqlConnection Connection;
        public CompanyProvider() : base private BaseProvider() { }
        { this.Connection = new NpgsqlConnection(Program.sConnectionString); }
        public CompanyProvider(NpgsqlConnection _Connection) { this.Connection = _Connection; }*/
        override public Exception GetAll(out IEnumerable<Company> _companies)
        {
            _companies = null;
            try
            {
                _companies = Connection.Query<Company>("select * from company");
                return null;
            }
            catch(NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Get(out Company _company, long _id)
        {
            _company = null;
            try
            {
                _company = Connection.QueryFirstOrDefault<Company>("select * from company where id = @id",
                    new { Id = _id });
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Add(out long _id, Company _company)
        {
            _id = 0;
            try
            {
                _id = Connection.QueryFirstOrDefault<long>("insert into company (name, boss) values ( @name, @boss) RETURNING id",
                    new { id = _company.Id, name = _company.Name, boss = _company.Boss });
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }

        override public Exception Set(Company _company)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("update company set name = @name, boss = @boss where id = @id",
                    new { id = _company.Id, name = _company.Name, boss = _company.Boss });
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
        override public Exception Del(long _id)
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("delete from company where id = @id",
                    new { id = _id });
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }

        }
        override public Exception DelAll()
        {
            try
            {
                _ = Connection.QueryFirstOrDefault<long>("delete from company");
                return null;
            }
            catch (NpgsqlException nsEx) { return nsEx; }
        }
    }
}
