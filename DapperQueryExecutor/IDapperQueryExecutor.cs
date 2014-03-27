using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperQueryExecutor
{
	/// <summary>
	/// Database agnostic query executor interface
	/// </summary>
	public interface IDapperQueryExecutor
	{
		int Execute(DapperQuery query);
		IEnumerable<T> Query<T>(DapperQuery query);
		Task<IEnumerable<T>> QueryAsync<T>(DapperQuery query);
	}
}
