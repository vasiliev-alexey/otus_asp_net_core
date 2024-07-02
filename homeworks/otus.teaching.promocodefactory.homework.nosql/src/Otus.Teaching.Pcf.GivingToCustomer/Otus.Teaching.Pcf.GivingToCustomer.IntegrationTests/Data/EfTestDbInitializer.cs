using Otus.Teaching.Pcf.GivingToCustomer.DataAccess;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationTests.Data;

public class EfTestDbInitializer(DataContext dataContext) : IDbInitializer
{
    public void InitializeDb()
    {
        dataContext.Database.EnsureDeleted();
        dataContext.Database.EnsureCreated();

        dataContext.AddRange(TestDataFactory.Preferences);
        dataContext.SaveChanges();

        dataContext.AddRange(TestDataFactory.Customers);
        dataContext.SaveChanges();
    }

    public void CleanDb()
    {
        dataContext.Database.EnsureDeleted();
    }
}