using GreeterNamespace;
using Grpc.Core;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Services
{
    public class CustomerService(
        IRepository<Core.Domain.PromoCodeManagement.Customer> customerRepository,
        IRepository<Core.Domain.PromoCodeManagement.Preference> preferenceRepository)
        : GreeterNamespace.Customer.CustomerBase
    {
        public override async Task<CustomerShortResponseList> GetCustomersAsync(CustomerShortRequest request, ServerCallContext context)
        {
            var customers = await customerRepository.GetAllAsync();

            var response = new CustomerShortResponseList();


            foreach (var item in customers)
            {
                response.Customers.Add(new CustomerShortResponse()
                {
                    Id = item.Id.ToString(),
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName
                });
            }

            return response;
        }

        public override async Task<CustomerShortResponse> GetCustomerAsync(GetCustomerRequest request, ServerCallContext context)
        {
            var customer = await customerRepository.GetByIdAsync(new Guid(request.Id));

            var response = new CustomerShortResponse()
            {
                Id = customer.Id.ToString(),
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return response;
        }

        public override async Task<CustomerResponse> CreateCustomerAsync(CreateOrEditCustomerRequest request, ServerCallContext context)
        {
            var listGuid = request.PreferenceIds.Select(s => new Guid(s)).ToList();

            var preferences = await preferenceRepository
                .GetRangeByIdsAsync(listGuid);


            var id = Guid.NewGuid();

            Core.Domain.PromoCodeManagement.Customer customer = new()
            {
                Id = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Preferences = preferences.Select(x => new CustomerPreference()
                {
                    CustomerId = id,
                    Preference = x,
                    PreferenceId = x.Id
                }).ToList()
            };

            await customerRepository.AddAsync(customer);

            return new CustomerResponse()
            {
                Id = customer.Id.ToString()
            };
        }

        public override async Task<EditCustomerResponse> EditCustomersAsync(EditCustomerRequest request, ServerCallContext context)
        {
            var customer = await customerRepository.GetByIdAsync(new Guid(request.Id));

            if (customer == null)
                throw new ArgumentException("Customer not found");

            var preferences = await preferenceRepository.GetRangeByIdsAsync(
                request.Request.PreferenceIds.Select(s => new Guid(s)).ToList());

            customer.Preferences = preferences.Select(x => new CustomerPreference()
            {
                CustomerId = customer.Id,
                Preference = x,
                PreferenceId = x.Id
            }).ToList();

            customer.Email = request.Request.Email;
            customer.FirstName = request.Request.FirstName;
            customer.LastName = request.Request.LastName;

            await customerRepository.UpdateAsync(customer);

            return new EditCustomerResponse();
        }

        public override async Task<DeleteCustomerResponse> DeleteCustomerAsync(DeleteCustomerRequest request, ServerCallContext context)
        {
            var customer = await customerRepository.GetByIdAsync(new Guid(request.Id));

            if (customer == null)
                throw new ArgumentException("Customer not found");

            await customerRepository.DeleteAsync(customer);

            return new DeleteCustomerResponse();
        }
    }
}
