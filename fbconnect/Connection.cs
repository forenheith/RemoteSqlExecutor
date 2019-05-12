using System.Text;

namespace fbconnect
{
    public class Connection
    {
        private readonly StringBuilder _connectionStringBuilder;

        public Connection(string host, string username, string password, string database, string dialect)
        {
            _connectionStringBuilder = new StringBuilder();
            _connectionStringBuilder.Append($"User={username};");
            _connectionStringBuilder.Append($"Password={password};");
            _connectionStringBuilder.Append($"Database={database};");
            _connectionStringBuilder.Append($"DataSource={host};");
            _connectionStringBuilder.Append("Port=3050;");
            _connectionStringBuilder.Append($"Dialect={dialect};");
            _connectionStringBuilder.Append("Charset=WIN1251;");
            _connectionStringBuilder.Append("Role=;");
            _connectionStringBuilder.Append("Connection lifetime=15;");
            _connectionStringBuilder.Append("Pooling=true;");
            _connectionStringBuilder.Append("MinPoolSize=0;");
            _connectionStringBuilder.Append("MaxPoolSize=50;");
            _connectionStringBuilder.Append("Packet Size=8192;");
            _connectionStringBuilder.Append("ServerType=0;");
        }

        public string ConnectionString => _connectionStringBuilder.ToString();
    }
}