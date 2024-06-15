using Otus.Teaching.Pcf.Administration.DataAccess;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;

namespace Otus.Teaching.Pcf.Administration.IntegrationTests.Data;

public class EfTestDbInitializer(DataContext dataContext) : IDbInitializer
{
    public void InitializeDb()
    {
        dataContext.Database.EnsureDeleted();
        dataContext.Database.EnsureCreated();

        dataContext.AddRange(TestDataFactory.Employees);
        dataContext.SaveChanges();
    }

    public void CleanDb()
    {
        dataContext.Database.EnsureDeleted();
    }
}