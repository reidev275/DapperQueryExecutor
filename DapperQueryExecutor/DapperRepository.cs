using System;
using System.Collections;
using System.Collections.Generic;

namespace DapperQueryExecutor
{
	/// <summary>
	/// Helper class providing a common constructor interface for Repository classes
	/// </summary>
	public abstract class DapperRepository
	{
		readonly IDapperQueryExecutor _queryExecutor;
		readonly string _connectionString;

		protected DapperRepository(IDapperQueryExecutor queryExecutor, string connectionString)
		{
			if (queryExecutor == null) throw new ArgumentNullException("queryExecutor");
			if (String.IsNullOrEmpty(connectionString)) throw new ArgumentException("connectionString cannot be null or empty");
			_queryExecutor = queryExecutor;
			_connectionString = connectionString;
		}

		protected DapperQuery CreateQuery()
		{
			return new DapperQuery(_connectionString);
		}

		protected int ExecuteNonQuery(DapperQuery query)
		{
			return _queryExecutor.Execute(query);
		}

		protected IEnumerable<T> Query<T>(DapperQuery query)
		{
			return _queryExecutor.Query<T>(query);
		}
	}
}
