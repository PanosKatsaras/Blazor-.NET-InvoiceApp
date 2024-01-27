using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
//Customer class
namespace InvoiceApp.Data
{
    public class Customer : IEntity, IOwnedEntity
    {
        //Database wil not generate the Id.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        //Mapping the UserId with Identity User (object) Id and can be null.
        public string UserId { get; set; } = null!;
        public IdentityUser? User { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

    }
}
