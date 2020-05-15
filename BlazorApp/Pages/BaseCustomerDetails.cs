using BlazorApp.Services.Interfaces;
using BlazorAppModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public class BaseCustomerDetails : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public ICustomerService CustomerService { get; set; }

        public Customer Customer { get; set; } = new Customer();

        protected async override Task OnInitializedAsync()
        {
            Customer = await CustomerService.GetCustomer(Id);
        }
    }
}
