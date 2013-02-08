DapperQueryExecutor
===================

What is this?
-------------
Leverages Dapper's simple object mapping to create a command pattern approach rather than an extension method approach.

Why should I use this over Dapper?
----------------------------------
Dapper is a fantastic tool, but it makes unit testing your repositories difficult because it was created as an extension method to the IDBConnection interface.
This library allows your repositories to be database agnostic and fully testable through the use of the command pattern.

