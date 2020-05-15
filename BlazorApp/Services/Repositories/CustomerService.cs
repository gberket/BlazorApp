using BlazorApp.Services.Interfaces;
using BlazorAppModels;
using BlazorWebApi.Data.Pagination;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp.Services.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<User> GetUser(string id)
        {
            return await httpClient.GetJsonAsync<User>($"/api/users/{id}");
        }

        public async Task<PaginatedList<Customer>> GetList(int? pageNumber, int pageSize)
        {
            string url = "/api/customers";
            return await httpClient.GetJsonAsync<PaginatedList<Customer>>(url + $"?pageNumber={pageNumber}&pageSize={pageSize}");
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return await httpClient.GetJsonAsync<Customer>($"/api/customers/{id}");
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            return await httpClient.PostJsonAsync<Customer>($"/api/customers", customer);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await httpClient.PutJsonAsync<Customer>($"/api/customers/{customer.Id}", customer);
        }

        public async Task DeleteCustomer(string customerId)
        {
            await httpClient.DeleteAsync($"/api/customers/{customerId}");
        }

    }
}
