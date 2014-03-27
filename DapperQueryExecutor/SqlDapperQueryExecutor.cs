using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace DapperQueryExecutor
{
	/// <summary>
	/// Sql implementation of IDapperQueryExecutor
	/// </summary>
	public class SqlDapperQueryExecutor : IDapperQueryExecutor
	{
		public int Execute(DapperQuery query)
		{
			if (query == null) throw new ArgumentNullException("query");
			using (var connection = new SqlConnection(query.ConnectionString))
			{
				connection.Open();
				return connection.Execute(query.Sql, query.Parameters, query.DbTransaction, query.CommandTimeout, query.CommandType);
			}
		}

		public IEnumerable<T> Query<T>(DapperQuery query)
		{
			if (query == null) throw new ArgumentNullException("query");
			using (var connection = new SqlConnection(query.ConnectionString))
			{
				connection.Open();
				return connection.Query<T>(query.Sql, query.Parameters, query.DbTransaction, query.Buffered, query.CommandTimeout, query.CommandType);
			}
		}

		public async Task<IEnumerable<T>> QueryAsync<T>(DapperQuery query)
		{
			if (query == null) throw new ArgumentNullException("query");
			using (var connection = new SqlConnection(query.ConnectionString))
			{
				await connection.OpenAsync();
				return await connection.QueryAsync<T>(query.Sql, query.Parameters, query.DbTransaction, query.CommandTimeout, query.CommandType);
			}
		}
	}
}
