using BlazorAppModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApi.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        /// <summary>
        /// Connection string and Name of Database
        /// </summary>
        public AppDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = client.GetDatabase("CustomerDB");
        }

        /// <summary>
        /// Database Customers Collection
        /// </summary>
        public IMongoCollection<Customer> CustomerRecord
        {
            get
            {
                return _mongoDatabase.GetCollection<Customer>("CustomerRecord");
            }
        }
    }
}
