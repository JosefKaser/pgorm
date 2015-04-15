# Welcome to PostgreSQL Object Relational Mapper for .NET #

PGORM is a code generator that can be used to create .NET (C#) objects and classes from a PostgreSQL database.

PGORM produces a "ready to go" and compiled .NET assembly that is directly usable from a .NET solution. With the generated assembly you are able to perform insert/update/delete operations on your PostgreSQL database using a structured object model.

PGORM also generates code for your database functions (stored procedures) so you can directly call them from your .NET code.

**PGORM uses the following external libraries:**
  * [Npgsql 2.0](http://npgsql.org)
  * [StringTemplate](http://www.stringtemplate.org) (ANTLR)
**PGORM requires the following to work:**
  * MS Visual Studio 2008
  * MSBuild 3.5
  * C# 3.0 (Linq)

### Screen shots ###

![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p1.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p1.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p2.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p2.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p3.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p3.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p4.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p4.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p5.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p5.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p6.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p6.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p7.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p7.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p8.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p8.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p9.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p9.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p10.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p10.jpg)
![http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p11.jpg](http://www.truesoftware.net/gevik/wp-content/uploads/2009/04/p11.jpg)

### Code Snippet ###
```
// Initialize the database
DataAccess.InitializeDatabase("localhost",
    "dellstore2", "postgres", "postgres");
```
```
// just to test whether all the data can be loaded with
// the generated assemblies. 
DeepLoader.Load();
```
```
// get all records from this table
customersRecordSet recordSet1 = customersFactory.GetAll();
```
```
// get some records
customersRecordSet recordSet2 = customersFactory.GetBy_customerid(10);
```
```
// get more records
customersRecordSet recordSet3 = customersFactory.GetBy_username("user21");
```
```
// update an existing object
customersObject customer1 = recordSet1[0];
customer1.firstname = "John";
// update the customer by customerid
customersFactory.UpdateBy_customerid(customer1, customer1.customerid.Value);
```
```
// call an stored procedure
StoredProcedures.new_customer(
    "Eva",
    "Adams",
    "Main Street 1",
    "",
    "Glendale",
    "CA",
    0,
    "USA",
    0,
    "eva.adams@example.com",
    "818-1234567890",
    1,
    "1234567890",
    "01-01-2010",
    "eva_adams",
    "password",
    30,
    2500,
    "F");
```