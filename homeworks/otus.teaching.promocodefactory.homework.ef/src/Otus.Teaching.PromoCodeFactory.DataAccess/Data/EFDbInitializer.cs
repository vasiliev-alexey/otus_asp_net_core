using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data;

public class EfDbInitializer(ApplicationContext applicationContext) : IDbInitializer
{
    public void Initialize()
    {
        applicationContext.Database.EnsureDeleted();
        applicationContext.Database.EnsureCreated();

        applicationContext.Roles.AddRange(FakeDataFactory.Roles);
        applicationContext.SaveChanges();
        applicationContext.Employees.AddRange(FakeDataFactory.Employees);
        applicationContext.SaveChanges();

        applicationContext.Preferences.AddRange(FakeDataFactory.Preferences);
        applicationContext.SaveChanges();
        
        
        applicationContext.Customers.AddRange(FakeDataFactory.Customers);
        applicationContext.SaveChanges();
    }
}