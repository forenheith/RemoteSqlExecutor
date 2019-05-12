using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FirebirdSql.Data.FirebirdClient;

namespace fbconnect
{
    public class Connector : IDisposable
    {
        private readonly string _host;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _database;
        private readonly string _dialect;

        public FbTransaction Transaction { get; set; }
        public FbCommand Command { get; set; }
        public FbConnection Connection { get; set; }

        public Connector(string host, string userName, string password, string database, string dialect = "3")
        {
            _host = host;
            _userName = userName;
            _password = password;
            _database = database;
            _dialect = dialect;

            Connection = GetConnection();
            Transaction = Connection.BeginTransaction();
        }

        private FbConnection GetConnection()
        {
            var connectionString =
                GetConnectionStringBuilder(_host, _userName, _password, _database, _dialect).ToString();
            var connection = new FbConnection(connectionString);
            connection.Open();
            return connection;
        }

        private FbConnectionStringBuilder GetConnectionStringBuilder(string host, string userName, string password,
            string database, string dialect = "3", string charset = "WIN1251")
        {
            return new FbConnectionStringBuilder
            {
                DataSource = host,
                Database = database,
                UserID = userName,
                Password = password,
                Charset = charset,
                Pooling = false
            };
        }

        public bool Execute(string sql, out FbDataReader reader, IEnumerable<SqlParameter> parameters = null,
            bool isNonQuery = false)
        {
            switch (isNonQuery)
            {
                case true:
                {
                    reader = null;
                    return ExecuteNonQuery(sql, parameters) >= 0;
                }
                default:
                {
                    reader = ExecuteQuery(sql, parameters);
                    return reader.HasRows;
                }
            }
        }

        private FbDataReader ExecuteQuery(string sql, IEnumerable<SqlParameter> parameters)
        {
            Command = InitCommand(sql, parameters);
            Command.Connection = Connection;
            Command.Transaction = Transaction;
            return Command.ExecuteReader();
        }

        private int ExecuteNonQuery(string sql, IEnumerable<SqlParameter> parameters)
        {
            Command = InitCommand(sql, parameters);
            return Command.ExecuteNonQuery();
        }

        private static FbCommand InitCommand(string sql, IEnumerable<SqlParameter> parameters)
        {
            var command = new FbCommand
            {
                CommandText = sql
            };

            if (parameters == null)
            {
                return command;
            }

            foreach (var sqlParameter in parameters)
            {
                command.Parameters.Add(sqlParameter.Name, sqlParameter.Value);
            }

            return command;
        }

        public List<List<string>> GetQueryResult(FbDataReader reader)
        {
            var resultList = new List<List<string>> {GetFieldsList(reader)};
            resultList.AddRange(GetPayload(reader));
            return resultList;
        }

        private List<string> GetFieldsList(FbDataReader reader)
        {
            List<string> fieldsList = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                fieldsList.Add(reader.GetName(i));
            }

            return fieldsList;
        }

        private List<List<string>> GetPayload(FbDataReader reader)
        {
           
            var rows = new List<List<string>>();
           
            while (reader.Read())
            {
                var row = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(reader[i].ToString());
                }
                rows.Add(row);
            }
            return rows;
        }

        public void Dispose()
        {
            Transaction.Commit();
            Command.Dispose();
            Connection.Close();
        }
    }
}