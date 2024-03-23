using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<Employee> Employees => new List<Employee>()
        {
            new()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
                AppliedPromocodesCount = 5
            },
            new()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
                AppliedPromocodesCount = 10
            },
        };


        static List<Role> _roles = null;

        public static IEnumerable<Role> Roles =>
            _roles ?? (_roles = new List<Role>()
            {
                new()
                {
                    Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                    Name = "Admin",
                    Description = "Администратор",
                },
                new()
                {
                    Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                    Name = "PartnerManager",
                    Description = "Партнерский менеджер"
                }
            });

        public static IEnumerable<Preference> Preferences => new List<Preference>()
        {
            new()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        public static IEnumerable<Customer> Customers
        {
            get
            {
                var faker = new Faker("en");

                var guids = Enumerable.Range(0, 3).Select(x => Guid.NewGuid()).ToList();
                var customers = new List<Customer>()
                {
                    new()
                    {
                        Id = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
                        Preferences = new List<CustomerPreference>()
                        {
                            new()
                            {
                                CustomerId = guids.First(),
                                PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c")
                            },
                        }
                    },
                    new()

                    {
                        Id = guids.First(),
                        Email = faker.Internet.Email(),
                        FirstName = faker.Name.FirstName(),
                        LastName = faker.Name.LastName(),
                        Preferences = new List<CustomerPreference>()
                        {
                            new()
                            {
                                CustomerId = guids.First(),
                                PreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd")
                            },
                            new()
                            {
                                CustomerId = guids.First(),
                                PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c")
                            },
                        }
                    }
                };


                foreach (var customer in customers)
                {
                    customer.PromoCodes = new List<PromoCode>()
                    {
                        new()
                        {
                            Customer = customer, Code = faker.Commerce.Product(),
                            Id = Guid.NewGuid(),
                            ServiceInfo = faker.Commerce.ProductDescription(),
                            BeginDate = DateTime.Now.AddDays(1),
                            EndDate = DateTime.Now.AddDays(14),
                        }
                    };
                }

                ;


                return customers;
            }
        }
    }
}