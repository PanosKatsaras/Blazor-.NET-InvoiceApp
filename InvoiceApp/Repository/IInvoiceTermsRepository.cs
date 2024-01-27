using InvoiceApp.Data;
using InvoiceApp.DTOS;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    public interface IInvoiceTermsRepository : IGenericOwnedRepository<InvoiceTerms, InvoiceTermsDTO>
    {
        public Task Seed(ClaimsPrincipal User);
    }
}
