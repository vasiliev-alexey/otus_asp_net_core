using MongoDB.Driver;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;

public class MongoDbInitializer(IMongoCollection<Preference> preferences, IMongoCollection<Customer> customers)
    : IDbInitializer
{
    public void InitializeDb()
    {
        preferences.DeleteMany(_ => true);
        customers.DeleteMany(_ => true);
        preferences.InsertMany(FakeDataFactory.Preferences);
        customers.InsertMany(FakeDataFactory.Customers);
    }
}