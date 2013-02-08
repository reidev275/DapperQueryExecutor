DapperQueryExecutor
===================

What is this?
-------------
Leverages Dapper's simple object mapping to create a command pattern approach rather than an extension method approach.

Why should I use this over Dapper?
----------------------------------
Dapper is a fantastic tool and without it this project would not be possible, but it makes unit testing your repositories difficult because it was created as an extension method to the IDBConnection interface.
This library allows your repositories to be database agnostic and fully testable through the use of the command pattern.

Example Repository
------------------

```csharp

public class DapperPeopleRepository : DapperRepository, IPeopleRepository
{
  public DapperPeopleRepository(IDapperQueryExecutor queryExecutor, string connectionString)
    : base(queryExecutor, connectionString) {  }
    
  //Simple query  
  public IEnumerable<Person> GetAllPeople()
  {
    var query = new DapperQuery(ConnectionString)
      {
        Sql = "Select FirstName, LastName, Age FROM People (NOLOCK)"
      }
    return QueryExecutor.Query<Person>(query);
  }
    
  //Adding Parameters to a query  
  public Person GetPerson(int personId)
  {    
    var query = new DapperQuery(ConnectionString)
      {
        Sql = "Select FirstName, LastName, Age FROM People (NOLOCK) WHERE id = @id",
        Parameters = { id = personId }
      }
    return QueryExecutor.Query<Person>(query).FirstOrDefault();
  }
}
```
