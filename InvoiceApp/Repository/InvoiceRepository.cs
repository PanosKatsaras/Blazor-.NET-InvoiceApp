using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoiceApp.Repository
{
    //The InvoiceRepository class that implements the GenericOwnedRepository.
    public class InvoiceRepository : GenericOwnedRepository<Invoice, InvoiceDTO>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context, IMapper mapper) 
            : base(context, mapper) { }

        //An asynchronous task that deletes the Invoice and all the InvoicesLineItems that correspond to it with the use of ClaimsPrincipal.
        public async Task DeleteWithLineItems(ClaimsPrincipal User, string invoiceId)
        {
            string? userId = getMyUserId(User);
            var lineItems = await context.InvoicesLineItems.Where(i => i.InvoiceId == invoiceId && i.UserId == userId).ToListAsync();
            foreach (InvoiceLineItem lineItem in lineItems)
            {
                context.InvoicesLineItems.Remove(lineItem);
            }
            Invoice? invoice = await context.Invoices.Where(i => i.Id == invoiceId && i.UserId == userId).FirstOrDefaultAsync();
            if (invoice != null)
            {
                context.Invoices.Remove(invoice);
            }
        }

        //An asynchronous task that gets a list of InvoiceDTO with the use of ClaimsPrincipal.
        public async Task<List<InvoiceDTO>> GetAll(ClaimsPrincipal User)
        {
            string? userId = getMyUserId(User);
            var query = from i in context.Invoices.Where(i => i.UserId == userId).Include(i => i.InvoiceLineItems).Include(i => i.InvoiceTerms).Include(i => i.Customer)
                    select new InvoiceDTO
                    {
                        Id = i.Id,
                        CreateDate = i.CreateDate,
                        InvoiceNumber = i.InvoiceNumber,
                        Description = i.Description,
                        CustomerId = i.CustomerId,
                        CustomerName = i.Customer!.Name,
                        InvoiceTermsId = i.InvoiceTermsId,
                        InvoiceTermsName = i.InvoiceTerms!.Name,
                        Paid = i.Paid,
                        Credit = i.Credit,
                        TaxRate = i.TaxRate,
                        UserId = i.UserId,
                        //Sum the TotalPrice of all InvoiceLineItems
                        InvoiceTotal = i.InvoiceLineItems.Sum(a => a.TotalPrice)
                    };

            List<InvoiceDTO>? results = await query.ToListAsync();
            return results;
        }
    }
}
