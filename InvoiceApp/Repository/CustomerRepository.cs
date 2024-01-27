using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    //The CustomerRepository class that implements the GenericOwnedRepository.
    public class CustomerRepository : GenericOwnedRepository<Customer, CustomerDTO>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper) { }

        //An asynchronous task that creates a CustomerDTO with the use of ClaimsPrincipal.
        public async Task Seed(ClaimsPrincipal User)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                int count = await context.Customers.Where(c => c.UserId == userId).CountAsync();
                if (count == 0)
                {
                    // Seed data.
                    CustomerDTO defaultCustomer = new CustomerDTO
                    {
                        Name = "First Customer"
                    };
                    await AddEntity(User, defaultCustomer);
                }
            }
            return;
        }
    }
}
