using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class Startup(IConfiguration configuration)
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers();
            services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });


            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlite(
                    $"Data Source={AppContext.BaseDirectory}{configuration.GetConnectionString("DefaultConnection")};");
                options.UseLazyLoadingProxies();
            });


            services.AddScoped(typeof(IRepository<Employee>), (x) =>
                new EfRepository<Employee>(x.GetService<ApplicationContext>()));

            services.AddScoped(typeof(IRepository<Role>), (x) =>
                new EfRepository<Role>(x.GetService<ApplicationContext>()));
            services.AddScoped(typeof(IRepository<Preference>), (x) =>
                new EfRepository<Preference>(x.GetService<ApplicationContext>()));
            services.AddScoped(typeof(IRepository<Customer>), (x) =>
                new EfRepository<Customer>(x.GetService<ApplicationContext>()));

            services.AddScoped(typeof(IRepository<PromoCode>), (x) =>
                new EfRepository<PromoCode>(x.GetService<ApplicationContext>()));

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPreferenceService, PreferenceService>();
            services.AddScoped<IPromoCodeService, PromoCodeService>();
            services.AddScoped<IDbInitializer, EfDbInitializer>();

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseOpenApi();
            app.UseSwaggerUi(x => { x.DocExpansion = "list"; });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            dbInitializer.Initialize();
        }
    }
}