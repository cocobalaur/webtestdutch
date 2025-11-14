example for repository and unit of work:
use the repository and unit of work patterns for CRUD operations.
For the Student entity type you'll create a repository interface and a repository class. 
When you instantiate the repository in your controller, you'll use the interface so that the controller will accept a reference to any object that implements the repository interface. 
When the controller runs under a web server, it receives a repository that works with the Entity Framework.
When the controller runs under a unit test class, it receives a repository that works with data stored 
in a way that you can easily manipulate for testing, such as an in-memory collection.

unit of work:
unit of work class can ensure that all repositories use the same context.


Controller
   ↓
UnitOfWork
   ↓
RepositoryProvider
   ↓
Repository (Generic or Specific)
   ↓
ApplicationDbContext (EF Core)
   ↓
Database

Controllers don’t directly talk to DbContext
Dependency Injection (DI) injects the repository (IDutchRepository<T>) and logger.
The controller doesn’t know how the repository works, it just uses its interface

UnitOfWork: Coordinator of Repositories
The UnitOfWork ensures all repositories share the same DbContext.

Layer	Responsibility
Generic Repository	Handles common CRUD for any entity (GetAll, Add, Update, Delete)
Specific Repository	Handles entity-specific operations, e.g., GetOrdersByCustomerId
RepositoryProvider	Provides repository instances (factory & cache)
UnitOfWork	Coordinates multiple repositories and ensures same DbContext across them
Controller	Uses repository interface to perform UI/business logic

Dependency Injection Glue

Here’s how DI makes everything work automatically:

In Program.cs:

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryProvider, RepositoryProvider>();
builder.Services.AddDbContext<ApplicationDbContext>(...);


When ASP.NET Core creates a ProductController:
It sees the constructor asks for IUnitOfWork and ILogger.
DI system creates:
ApplicationDbContext (scoped per request)
RepositoryProvider
UnitOfWork (injects the above)
UnitOfWork uses RepositoryProvider to get the right repository for Product.
Controller now has a ready-to-use repository without knowing how to construct it.


Write C# classes → Add migration → EF builds database
Define or update a data model in code.
Add a Migration to translate this model into changes that can be applied to the database.
Check that the Migration correctly represents your intentions.
Apply the Migration to update the database to be in sync with the model.
Repeat steps 1 through 4 to further refine the model and keep the database in sync.

Update-Database

The Identity model
Entity types
The Identity model consists of the following entity types.

Entity type	Description
User	Represents the user.
Role	Represents a role.
UserClaim	Represents a claim that a user possesses.
UserToken	Represents an authentication token for a user.
UserLogin	Associates a user with a login.
RoleClaim	Represents a claim that's granted to all users within a role.
UserRole	A join entity that associates users and roles.

Entity type relationships
The entity types are related to each other in the following ways:

Each User can have many UserClaims.
Each User can have many UserLogins.
Each User can have many UserTokens.
Each Role can have many associated RoleClaims.
Each User can have many associated Roles, and each Role can be 
associated with many Users. This is a many-to-many relationship that requires a join table in the database. The join table is represented by the UserRole entity.

view <- controller requests  -> model 
views response -> model 
