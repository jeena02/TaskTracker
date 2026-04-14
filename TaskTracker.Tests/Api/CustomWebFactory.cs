using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskTracker.Infrastructure.Data;

namespace TaskTracker.Tests.Api
{
    public class CustomWebFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<DbContextOptions<TaskDbContext>>();
                services.RemoveAll<TaskDbContext>();

                // ✅ Use file-based SQLite (stable)
                services.AddDbContext<TaskDbContext>(options =>
                {
                    options.UseSqlite("Data Source=test.db");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

                db.Database.EnsureDeleted();   // clean per test
                db.Database.EnsureCreated();
            });
        }
    }
}