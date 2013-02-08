using System;

namespace DapperQueryExecutor
{
	/// <summary>
	/// Helper class providing a common constructor interface for Repository classes
	/// </summary>
	public abstract class DapperRepository
	{
		protected readonly IDapperQueryExecutor QueryExecutor;
		protected readonly string ConnectionString;

		protected DapperRepository(IDapperQueryExecutor queryExecutor, string connectionString)
		{
			if (queryExecutor == null) throw new ArgumentNullException("queryExecutor");
			if (String.IsNullOrEmpty(connectionString)) throw new ArgumentException("connectionString cannot be null or empty");
			QueryExecutor = queryExecutor;
			ConnectionString = connectionString;
		}
	}
}
