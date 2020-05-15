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

        public async Task<Customer> GetCustomer(string customerId)
        {
            FilterDefinition<Customer> filterEmployeeData = Builders<Customer>.Filter.Eq("Id", customerId);
            return await appDbContext.CustomerRecord.Find(filterEmployeeData).FirstAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await appDbContext.CustomerRecord.AsQueryable<Customer>().Where(c => true).ToListAsync();
        }


        public async Task<User> GetUser(string userId)
        {
            FilterDefinition<User> filterUserData = Builders<User>.Filter.Eq("Id", userId);
            return await appDbContext.UsersCollection.Find(filterUserData).FirstAsync();
        }




        public async Task<PaginatedList<Customer>> GetCustomersPaginated(int pageIndex, int pageSize)
        {

            //check pagesize not to b 0!!!!!!!!!!!!!!!!!!!
            int itemsToSkip = (pageIndex - 1) * pageSize;
            int itemsToTake = pageSize;

            var itemsQuery = await appDbContext.CustomerRecord.Find(_ => true).ToListAsync();

            int totalItems = itemsQuery.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = itemsQuery.Skip(itemsToSkip).Take(itemsToTake).ToList();

            return new PaginatedList<Customer>()
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = items
            };
            throw new NotImplementedException();
        }

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
