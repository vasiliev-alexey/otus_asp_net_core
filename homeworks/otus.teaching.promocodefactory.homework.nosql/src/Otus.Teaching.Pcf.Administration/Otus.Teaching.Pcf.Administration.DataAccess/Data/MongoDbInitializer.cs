using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;

namespace Otus.Teaching.Pcf.Administration.DataAccess.Data;

public class MongoDbInitializer(IMongoCollection<Employee> employees, IMongoCollection<Role> roles)
    : IDbInitializer
{
    public void InitializeDb()
    {
        employees.DeleteMany(_ => true);
        roles.DeleteMany(_ => true);
        employees.InsertMany(FakeDataFactory.Employees);
        roles.InsertMany(FakeDataFactory.Roles);
    }
}