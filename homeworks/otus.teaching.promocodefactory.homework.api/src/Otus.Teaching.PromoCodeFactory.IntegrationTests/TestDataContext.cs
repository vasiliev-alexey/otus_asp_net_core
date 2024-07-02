using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.DataAccess;

namespace Otus.Teaching.PromoCodeFactory.IntegrationTests.Api
{
    public class TestDataContext
        : DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=PromoCodeFactoryDb.sqlite");
        }
    }
}