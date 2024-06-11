using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;

namespace Otus.Teaching.Pcf.Administration.WebHost.Helpers;

public static class DbInitializationManager
{
    public static IHost InitializeDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.InitializeDb();

        return host;
    }
}