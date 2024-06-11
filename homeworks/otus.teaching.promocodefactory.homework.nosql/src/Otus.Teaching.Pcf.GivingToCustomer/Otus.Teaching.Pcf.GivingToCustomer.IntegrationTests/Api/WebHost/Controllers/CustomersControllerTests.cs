using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Controllers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using Xunit;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationTests.Api.WebHost.Controllers;

public class CustomersControllerTests
{
    private readonly CustomersController _customerController;

    private readonly EfRepository<Customer> _customerRepository;
    private readonly EfRepository<Preference> _preferencesRepository;


    public CustomersControllerTests(EfDatabaseFixture efDatabaseFixture)
    {
        _customerRepository = new EfRepository<Customer>(efDatabaseFixture.DbContext);
        _preferencesRepository = new EfRepository<Preference>(efDatabaseFixture.DbContext);
        _customerController = new CustomersController(_customerRepository, _preferencesRepository);
    }


    [Fact]
    public async Task CreateCustomerAsync_CanCreateCustomer_ShouldCreateExpectedCustomer()
    {
        //Arrange 


        var preferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c");
        var request = new CreateOrEditCustomerRequest
        {
            Email = "some@mail.ru",
            FirstName = "Иван",
            LastName = "Петров",
            PreferenceIds = new List<Guid>
            {
                preferenceId
            }
        };

        //Act
        //   var response = await client.PostAsJsonAsync("/api/v1/customers", request);
        var response = await _customerController.CreateCustomerAsync(request);
        //Assert

        response.Result.Should().Be(HttpStatusCode.Created);

        //Теперь получаем объект, который должен было создан, если REST правильно написан, то в Location будет
        //готовый URL для получения нового объекта
        var actualContent = "fix me latter";
        var actual = JsonConvert.DeserializeObject<CustomerResponse>(actualContent);

        actual.Email.Should().Be(request.Email);
        actual.FirstName.Should().Be(request.FirstName);
        actual.LastName.Should().Be(request.LastName);
        actual.Preferences.Should()
            .ContainSingle()
            .And
            .Contain(x => x.Id == preferenceId);
    }

    [Fact]
    public async Task GetCustomerAsync_CustomerExisted_ShouldReturnExpectedCustomer()
    {
        //Arrange 
        //var client = _factory.CreateClient();

        //Переопределяем как угодно
        //_factory.WithWebHostBuilder((builder) => { }).CreateClient();

        //Переопределяем реальные зависимости заглушками

        var expected = new CustomerResponse
        {
            Id = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
            Email = "ivan_sergeev@mail.ru",
            FirstName = "Иван",
            LastName = "Петров",
            Preferences = new List<PreferenceResponse>
            {
                new()
                {
                    Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                    Name = "Театр"
                },
                new()
                {
                    Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                    Name = "Дети"
                }
            }
        };

        //Act
        //   var response = await client.GetAsync($"/api/v1/customers/{expected.Id}");

        //Assert
        // response.IsSuccessStatusCode.Should().BeTrue();
        // response.StatusCode.Should().Be(HttpStatusCode.OK);
        //
        // var actual = JsonConvert.DeserializeObject<CustomerResponse>(
        //     await response.Content.ReadAsStringAsync());

        //actual.Should().BeEquivalentTo(expected);
    }
}