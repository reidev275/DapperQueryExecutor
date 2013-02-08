using System.Collections.Generic;

namespace DapperQueryExecutor
{
	/// <summary>
	/// Database agnostic query executor interface
	/// </summary>
	public interface IDapperQueryExecutor
	{
		int Execute(DapperQuery query);
		IEnumerable<T> Query<T>(DapperQuery query);
	}
}
