using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
//Invoice class
namespace InvoiceApp.Data
{
    public class Invoice : IEntity, IOwnedEntity
    {
        //Database wil not generate the Id.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //Mapping the UserId with Identity User (object) Id and can be null.
        public string UserId { get; set; } = null!;
        public IdentityUser? User { get; set; } = null!;

        //Database wil not generate the InvoiceNumber.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int InvoiceNumber { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string Description { get; set; } = string.Empty;

        //Mapping the CustomerId with Customer and helping with navigation between objects.
        public string CustomerId { get; set; } = string.Empty;
        public Customer? Customer { get; set; } = null;

        //Mapping the InvoiceTermsId with InvoiceTerms and helping with navigation between objects.
        public string InvoiceTermsId { get; set; } = string.Empty;
        public InvoiceTerms? InvoiceTerms { get; set; } = null;

        public double Paid { get; set; } = 0;
        public double Credit { get; set; } = 0;
        public double TaxRate { get; set; } = 0;

        //A navigation property as a list of InvoiceLineItems.
        public ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();

    }
}
