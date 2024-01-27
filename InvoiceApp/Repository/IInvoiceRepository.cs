using InvoiceApp.Data;
using InvoiceApp.DTOS;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    public interface IInvoiceRepository : IGenericOwnedRepository<Invoice, InvoiceDTO>
    {
        public Task<List<InvoiceDTO>> GetAll(ClaimsPrincipal User);
        public Task DeleteWithLineItems(ClaimsPrincipal User, string invoiceId);

    }
}
