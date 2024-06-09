namespace Otus.Teaching.Pcf.Administration.DataAccess.Data;

public class EfDbInitializer(DataContext dataContext) : IDbInitializer
{
    public void InitializeDb()
    {
        dataContext.Database.EnsureDeleted();
        dataContext.Database.EnsureCreated();

        dataContext.AddRange(FakeDataFactory.Employees);
        dataContext.SaveChanges();
    }
}