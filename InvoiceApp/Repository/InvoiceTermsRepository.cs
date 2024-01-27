using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    //The InvoiceTermsRepository class that implements the GenericOwnedRepository.
    public class InvoiceTermsRepository : GenericOwnedRepository<InvoiceTerms, InvoiceTermsDTO>, IInvoiceTermsRepository
    {
        public InvoiceTermsRepository(ApplicationDbContext context, IMapper mapper) : 
            base(context, mapper) { }

        //An asynchronous task that creates InvoiceTermsDTO objects with the use of ClaimsPrincipal.
        public async Task Seed(ClaimsPrincipal User)
        {
            string? userId = getMyUserId(User);
            if (userId is not null)
            {
                int count = await context.InvoiceTerms.Where(it => it.UserId == userId).CountAsync();
                if (count == 0)
                {
                    // Seed data.
                    InvoiceTermsDTO terms1 = new InvoiceTermsDTO
                    {
                        Name = "Net 30"
                    };
                    InvoiceTermsDTO terms2 = new InvoiceTermsDTO
                    {
                        Name = "Net 60"
                    };
                    InvoiceTermsDTO terms3 = new InvoiceTermsDTO
                    {
                        Name = "Net 90"
                    };
                    await AddEntity(User, terms1);
                    await AddEntity(User, terms2);
                    await AddEntity(User, terms3);

                }
            }
        }
    }
}
