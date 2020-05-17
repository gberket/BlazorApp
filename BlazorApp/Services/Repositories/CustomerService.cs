using BlazorApp.Services.Interfaces;
using BlazorAppModels;
using BlazorWebApi.Data.Pagination;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp.Services.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient ??
                throw new ArgumentNullException(nameof(httpClient));
            this.httpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Get list of Customers with paging
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginatedList<Customer>> GetList(int pageNumber, int pageSize)
        {
            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            string url = "/api/customers";
            return await httpClient.GetJsonAsync<PaginatedList<Customer>>(url + $"?pageNumber={pageNumber}&pageSize={pageSize}");
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomer(string id)
        {
            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            return await httpClient.GetJsonAsync<Customer>($"/api/customers/{id}");
        }

        /// <summary>
        /// Add a new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomer(Customer customer)
        {
             return await httpClient.PostJsonAsync<Customer>($"/api/customers", customer);
        }

        /// <summary>
        /// Edit Existing Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await httpClient.PutJsonAsync<Customer>($"/api/customers/{customer.Id}", customer);
        }

        /// <summary>
        /// Delete a customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task DeleteCustomer(string customerId)
        {
            await httpClient.DeleteAsync($"/api/customers/{customerId}");
        }

    }
}
