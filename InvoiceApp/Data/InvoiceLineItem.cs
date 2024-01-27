using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
//InvoiceLineItem class
namespace InvoiceApp.Data
{
    public class InvoiceLineItem : IEntity, IOwnedEntity
    {
        //Database wil not generate the Id.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //Mapping the InvoiceId with Invoice and helping with navigation between objects.
        public string InvoiceId { get; set; } = string.Empty;
        public Invoice? Invoice { get; set; } = null;

        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; private set; }

        //Mapping the UserId with Identity User (object) Id and can be null.
        public string UserId { get; set; } = null!;
        public IdentityUser? User { get; set; } = null!;



    }
}
