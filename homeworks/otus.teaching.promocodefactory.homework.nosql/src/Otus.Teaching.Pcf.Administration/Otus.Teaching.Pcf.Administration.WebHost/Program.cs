using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using Otus.Teaching.Pcf.Administration.WebHost.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddMvcOptions(x =>
    x.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddSingleton<IMongoClient>(
        new MongoClient(builder.Configuration.GetConnectionString("PromocodeFactoryAdministrationDb"))).AddSingleton(
        serviceProvider =>
            serviceProvider.GetRequiredService<IMongoClient>()
                .GetDatabase("promocode_factory_administration_db"))
    .AddSingleton(serviceProvider =>
        serviceProvider.GetRequiredService<IMongoDatabase>()
            .GetCollection<Employee>("Employees"))
    .AddSingleton(serviceProvider =>
        serviceProvider.GetRequiredService<IMongoDatabase>()
            .GetCollection<Role>("Roles"))
    .AddScoped(serviceProvider =>
        serviceProvider.GetRequiredService<IMongoClient>()
            .StartSession());
builder.Services.AddScoped<IDbInitializer, MongoDbInitializer>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));

builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "PromoCode Factory Administration API Doc";
    options.Version = "1.0";
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseOpenApi();
app.UseSwaggerUi(x => { x.DocExpansion = "list"; });

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.InitializeDatabase();
app.Run();