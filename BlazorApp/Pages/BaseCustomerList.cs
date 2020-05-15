using BlazorApp.Services.Interfaces;
using BlazorAppModels;
using BlazorWebApi.Data.Pagination;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class BaseCustomerList : ComponentBase
    {
        /// <summary>
        /// Initialize Variables, Services, Objects
        /// </summary>
        [Inject]
        public ICustomerService CustomerService { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public Customer cust = new Customer();
        protected string modalTitle { get; set; }
        protected Boolean isDelete = false;
        protected Boolean isAdd = false;

        public PaginatedList<Customer> paginatedList = new PaginatedList<Customer>();
        // Page and Sort data
        int? pageNumber = 1;
        int pageSize = 2;


        protected override async Task OnInitializedAsync()
        {
            await GetCustomers();
        }

        /// <summary>
        /// Get all Customers with paging
        /// </summary>
        /// <returns></returns>
        protected async Task GetCustomers()
        {
            paginatedList = await CustomerService.GetList(pageNumber, pageSize);
            Customers = paginatedList.Items;
        }

        /// <summary>
        /// Add a new Customer to our DB
        /// </summary>
        protected void AddCustomer()
        {
            cust = new Customer();
            this.modalTitle = "Add Customer";
            this.isAdd = true;
        }

        /// <summary>
        /// Opens the modal to Edit the values of Selected Customer
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        protected async Task EditCustomer(string custId)
        {
            cust = await CustomerService.GetCustomer(custId);
            this.modalTitle = "Edit Customer";
            this.isAdd = true;
        }

        /// <summary>
        /// Save Changes on Edit or Add of a new Customer
        /// </summary>
        /// <returns></returns>
        protected async Task SaveCustomer()
        {
            if (cust.Id != null)
            {
                await CustomerService.UpdateCustomer(cust);
                await GetCustomers();
            }
            else
            {
                await CustomerService.AddCustomer(cust);
                await GetCustomers();
            }
            this.isAdd = false;
        }

        /// <summary>
        /// Opens the modal with details of Customer for deletion
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        protected async Task DeleteConfirm(string custId)
        {
            cust = await CustomerService.GetCustomer(custId);
            this.isDelete = true;
        }

        /// <summary>
        /// Delete the chosen Customer
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        protected async Task DeleteCustomer(string custId)
        {
            await CustomerService.DeleteCustomer(custId);
            this.isDelete = false;
            await GetCustomers();
        }

        /// <summary>
        /// Open or Close the modal For Customer Crud operations
        /// </summary>
        protected void closeModal()
        {
            this.isAdd = false;
            this.isDelete = false;
        }

        /// <summary>
        /// Check if our page index has changed
        /// </summary>
        /// <param name="newPageNumber"></param>
        public async void PageIndexChanged(int newPageNumber)
        {
            if (newPageNumber < 1 || newPageNumber > paginatedList.TotalPages)
            {
                return;
            }
            pageNumber = newPageNumber;
            paginatedList = await CustomerService.GetList(pageNumber, pageSize);
            Customers = paginatedList.Items;
            StateHasChanged();//mipws mporw na to xrisimopoiisw anti tou GetCustomers()
        }
    }
}
