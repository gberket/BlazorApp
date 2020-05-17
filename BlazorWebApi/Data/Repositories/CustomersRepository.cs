using BlazorAppModels;
using BlazorWebApi.Data.Interfaces;
using BlazorWebApi.Data.Pagination;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApi.Data.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AppDbContext appDbContext;

        public CustomersRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        /// <summary>
        /// Add a new Customer in DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                await appDbContext.CustomerRecord.InsertOneAsync(customer);
                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomer(string customerId)
        {
            FilterDefinition<Customer> filterEmployeeData = Builders<Customer>.Filter.Eq("Id", customerId);
            return await appDbContext.CustomerRecord.Find(filterEmployeeData).FirstAsync();
        }

        /// <summary>
        /// Get all Customers from Database 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await appDbContext.CustomerRecord.AsQueryable<Customer>().Where(c => true).ToListAsync();
        }

        /// <summary>
        /// Get Customers List with paging
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginatedList<Customer>> GetCustomersPaginated(int pageIndex, int pageSize)
        {

            int itemsToSkip = (pageIndex - 1) * pageSize ;
            int itemsToTake = pageSize;

            var itemsQuery = await appDbContext.CustomerRecord.Find(_ => true).ToListAsync();

            int totalItems = itemsQuery.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize );

            var items = itemsQuery.Skip(itemsToSkip).Take(itemsToTake).ToList();

            return new PaginatedList<Customer>()
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = items
            };
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update an existing Customer record
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                await appDbContext.CustomerRecord.ReplaceOneAsync(filter: g => g.Id == customer.Id, replacement: customer);
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete an existing Customer from Database
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task DeleteCustomer(string customerId)
        {
            try
            {
                await appDbContext.CustomerRecord.DeleteOneAsync(g => g.Id == customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
