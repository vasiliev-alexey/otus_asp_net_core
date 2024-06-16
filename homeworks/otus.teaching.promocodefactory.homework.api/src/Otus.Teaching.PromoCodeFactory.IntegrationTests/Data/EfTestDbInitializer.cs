using Otus.Teaching.PromoCodeFactory.DataAccess;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;

namespace Otus.Teaching.PromoCodeFactory.IntegrationTests.Api.Data
{
    public class EfTestDbInitializer
        : IDbInitializer
    {
        private readonly DataContext _dataContext;

        public EfTestDbInitializer(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();
            
            _dataContext.AddRange(TestDataFactory.Employees);
            _dataContext.SaveChanges();
            
            _dataContext.AddRange(TestDataFactory.Preferences);
            _dataContext.SaveChanges();
            
            _dataContext.AddRange(TestDataFactory.Customers);
            _dataContext.SaveChanges();
            
            _dataContext.AddRange(TestDataFactory.Partners);
            _dataContext.SaveChanges();
        }

        public void CleanDb()
        {
            _dataContext.Database.EnsureDeleted();
        }
    }
}