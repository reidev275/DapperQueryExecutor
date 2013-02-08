using System;
using System.Data;

namespace DapperQueryExecutor
{
	/// <summary>
	/// Encapsulates a query that can be executed by Dapper
	/// </summary>
	public class DapperQuery
	{
		public string ConnectionString { get; private set; }
		public string Sql { get; set; }
		public object Parameters { get; set; }
		public IDbTransaction DbTransaction { get; set; }
		public int? CommandTimeout { get; set; }
		public CommandType? CommandType { get; set; }
		public bool Buffered { get; set; }

		public DapperQuery(string connectionString)
		{
			if (connectionString == null) throw new ArgumentNullException("connectionString");
			ConnectionString = connectionString;
			Buffered = true;
		}
	}
}
