using BlazorAppModels;
using BlazorWebApi.Data.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services.Interfaces
{
    public interface ICustomerService
    {
        //Task<IEnumerable<Customer>> GetCustomers();
        Task<PaginatedList<Customer>> GetList(int? pageNumber, int pageSize);
        Task<Customer> GetCustomer(string id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task DeleteCustomer(string customerId);

        Task<User> GetUser(string id);
    }
}
