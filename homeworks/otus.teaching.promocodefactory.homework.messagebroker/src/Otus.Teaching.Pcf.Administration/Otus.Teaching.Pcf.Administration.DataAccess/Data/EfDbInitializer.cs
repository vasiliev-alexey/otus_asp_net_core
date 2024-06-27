using System;

namespace Otus.Teaching.Pcf.Administration.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly DataContext _dataContext;

        public EfDbInitializer(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!  create db");
            _dataContext.AddRange(FakeDataFactory.Employees);
            _dataContext.SaveChanges();
        }
    }
}