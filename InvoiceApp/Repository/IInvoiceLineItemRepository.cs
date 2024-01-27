using InvoiceApp.Data;
using InvoiceApp.DTOS;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    public interface IInvoiceLineItemRepository : IGenericOwnedRepository<InvoiceLineItem, InvoiceLineItemDTO>
    {
            public Task<List<InvoiceLineItemDTO>> GetAllByInvoiceId(ClaimsPrincipal User, string invoiceId);
    }
}
