using InvoiceApp.Data;
using InvoiceApp.DTOS;
using System.Security.Claims;

//Interface
namespace InvoiceApp.Repository
{
    public interface ICustomerRepository : IGenericOwnedRepository<Customer, CustomerDTO>
    {
        public Task Seed(ClaimsPrincipal User);
    }
}
