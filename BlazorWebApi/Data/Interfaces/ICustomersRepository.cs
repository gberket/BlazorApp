using BlazorAppModels;
using BlazorWebApi.Data.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApi.Data.Interfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<PaginatedList<Customer>> GetCustomersPaginated(int pageIndex, int pageSize);
        Task<Customer> GetCustomer(string customerId);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task DeleteCustomer(string customerId);

        Task<User> GetUser(string userId);
    }
}
