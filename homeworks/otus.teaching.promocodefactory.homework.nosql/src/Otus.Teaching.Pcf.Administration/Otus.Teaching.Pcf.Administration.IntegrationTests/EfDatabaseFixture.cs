﻿using System;
using Otus.Teaching.Pcf.Administration.IntegrationTests.Data;

namespace Otus.Teaching.Pcf.Administration.IntegrationTests;

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