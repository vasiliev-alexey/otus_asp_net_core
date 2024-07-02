namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;

public class EfDbInitializer(DataContext dataContext) : IDbInitializer
{
    public void InitializeDb()
    {
        dataContext.Database.EnsureDeleted();
        dataContext.Database.EnsureCreated();

        dataContext.AddRange(FakeDataFactory.Preferences);
        dataContext.SaveChanges();

        dataContext.AddRange(FakeDataFactory.Customers);
        dataContext.SaveChanges();
    }
}