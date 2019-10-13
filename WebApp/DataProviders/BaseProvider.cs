using System;
using System.Collections.Generic;
using Npgsql;

namespace WebApp.DataProviders
{
    abstract public class BaseProvider<T>
    {
        protected readonly NpgsqlConnection Connection;
        public BaseProvider() { this.Connection = new NpgsqlConnection(Program.sConnectionString); }
        public BaseProvider(NpgsqlConnection _Connection) { this.Connection = _Connection; }
        abstract public Exception GetAll(out IEnumerable<T> ts);
        abstract public Exception Get(out T t, long id);
        abstract public Exception Add(out long id, T entity);
        abstract public Exception Set(T entity);
        abstract public Exception Del(long id);
        abstract public Exception DelAll();
    }
}
