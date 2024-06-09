using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Integration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Configure services
builder.Services.AddControllers().AddMvcOptions(x => x.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddSingleton<IMongoClient>(
    new MongoClient(configuration.GetConnectionString("PromocodeFactoryGivingToCustomerDb")));
builder.Services.AddSingleton(serviceProvider =>
    serviceProvider.GetRequiredService<IMongoClient>().GetDatabase("promocode_factory_givingToCustomer_db"));
builder.Services.AddSingleton(serviceProvider =>
    serviceProvider.GetRequiredService<IMongoDatabase>().GetCollection<Preference>("Preferences"));
builder.Services.AddSingleton(serviceProvider =>
    serviceProvider.GetRequiredService<IMongoDatabase>().GetCollection<Customer>("Customers"));
builder.Services.AddSingleton(serviceProvider =>
    serviceProvider.GetRequiredService<IMongoDatabase>().GetCollection<PromoCode>("PromoCodes"));
builder.Services.AddScoped(serviceProvider =>
    serviceProvider.GetRequiredService<IMongoClient>().StartSession());
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));
builder.Services.AddScoped<INotificationGateway, NotificationGateway>();
builder.Services.AddScoped<IDbInitializer, MongoDbInitializer>();

builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "PromoCode Factory Giving To Customer API Doc";
    options.Version = "1.0";
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseOpenApi();
app.UseSwaggerUi(settings => settings.DocExpansion = "list");

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.InitializeDb();
}

app.Run();