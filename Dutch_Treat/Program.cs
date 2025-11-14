using Dutch_Treat.Data;
using Dutch_Treat.Data.Entities;
using Dutch_Treat.Data.Interfaces;
using Dutch_Treat.Data.Repositories;
using Dutch_Treat.Data.Repositories.Helpers;
using DutchTreat.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// dependency injection (software design pattern where an object or function receives its dependencies from an external source rather than creating them itself.)
//ApplicationDbContext is your EF Core database context.
//DI injects ApplicationDbContext whenever a class needs it.
//so when i create a controller/ any other file i dont need to specify the database cause its already creating it for you through ASP.net 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)); //Improves performance by not tracking entities unless explicitly needed
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Registers Identity services, which depend on the ApplicationDbContext.
//DI ensures that Identity can get the DB context automatically.
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DutchSeeder>(); //Registers DutchSeeder with a Transient lifetime, meaning a new instance is created every time it is requested.
                                              //The seeder can then have dependencies injected in its constructor (like ApplicationDbContext or repositories).

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryProvider, RepositoryProvider>();


var app = builder.Build();

await RunSeeding(app);

async Task RunSeeding(WebApplication app)
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopeFactory.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
        await seeder.Seed();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
