﻿using System;
using Otus.Teaching.Pcf.GivingToCustomer.IntegrationTests.Data;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationTests;

public class EfDatabaseFixture : IDisposable
{
    private readonly EfTestDbInitializer _efTestDbInitializer;

    public EfDatabaseFixture()
    {
        DbContext = new TestDataContext();

        _efTestDbInitializer = new EfTestDbInitializer(DbContext);
        _efTestDbInitializer.InitializeDb();
    }

    public TestDataContext DbContext { get; }

    public void Dispose()
    {
        _efTestDbInitializer.CleanDb();
    }
}