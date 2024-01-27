//Interface of the User that always has a UserId
namespace InvoiceApp.Data
{
    public interface IOwnedEntity
    {
        public string UserId { get; set; }
    }
}
