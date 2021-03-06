DapperQueryExecutor
===================

Leverages [Dapper's](https://github.com/SamSaffron/dapper-dot-net) simple object mapping with a command pattern approach rather than an extension method approach.

Available via Nuget
-------------------
Install-Package DapperQueryExecutor

Why should I use this over Dapper?
----------------------------------
[Dapper](https://github.com/SamSaffron/dapper-dot-net) is a fantastic tool and without it this project would not be possible, but it makes unit testing your repositories difficult because it was created as an extension method to the IDBConnection interface.
This library allows your repositories to be database agnostic and fully testable through the use of the command pattern.

Given the following Person class
```csharp
public class Person
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public int Age { get; set; }
}
```

Command pattern way:
```csharp
var query = new DapperQuery("myConnectionString")
  {
    Sql = "Select FirstName, LastName, Age FROM People (NOLOCK)"
  };
IDapperQueryExecutor queryExecutor = new SqlDapperQueryExecutor();
IEnumerable<Person> people = queryExecutor.Query<Person>(query);
```

Dapper's way:
```csharp
IEnumerable<Person> people;
using (var connection = new SqlConnection("myConnectionString"))
{
  connection.Open();
  people = connection.Query<Person>("Select FirstName, LastName, Age FROM People (NOLOCK)")
}
```

Recommended usage with dependency injection
-------------------------------------------

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

public class Person
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public int Age { get; set; }
}
```
As you can see, we can easily mock an IDapperQueryExecutor and fully unit test our repository without
actually hitting the database. Also note that our repository doesn't have a single reference to System.Data.

Usage with Ninject
------------------
By binding all requirements for an IDapperQueryExecutor through an IOC framework we now have a central place to determine which database our app uses.

```csharp
private static void RegisterServices(IKernel kernel)
{
  kernel.Bind<IDapperQueryExecutor>().To<SqlDapperQueryExecutor>().InSingletonScope();
}
```
