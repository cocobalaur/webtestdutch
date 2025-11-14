example for repository and unit of work:
For the Student entity type you'll create a repository interface and a repository class. 
When you instantiate the repository in your controller, you'll use the interface so that the controller will accept a reference to any object that implements the repository interface. 
When the controller runs under a web server, it receives a repository that works with the Entity Framework.
When the controller runs under a unit test class, it receives a repository that works with data stored 
in a way that you can easily manipulate for testing, such as an in-memory collection.