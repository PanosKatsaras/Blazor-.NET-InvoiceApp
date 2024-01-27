using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InvoiceApp.Data
{
    //A class responsible for interacting with the database and includes DbSet properties.
    //Inherits from IdentityDbContext an ASP.NET Core Identity for user authentication and authorization.
    public class ApplicationDbContext : IdentityDbContext
    {
        //All DbSet properties for each entity type.
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceTerms> InvoiceTerms { get; set; }
        public DbSet<InvoiceLineItem> InvoicesLineItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        // Customizations against the delete cascade action.
        protected void RemoveFixups(ModelBuilder modelBuilder, Type type)
        {
            foreach (var relationship in modelBuilder.Model.FindEntityType(type)!.GetForeignKeys())
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientNoAction;
            }
        }
        //Override the default settings of OnModelCreating method.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Customizations against delete cascade action.
            RemoveFixups(modelBuilder, typeof(Invoice));
            RemoveFixups(modelBuilder, typeof(InvoiceTerms));
            RemoveFixups(modelBuilder, typeof(Customer));
            RemoveFixups(modelBuilder, typeof(InvoiceLineItem));

            //Ignore after save behavior if the InvoiceNumber value changed.
            modelBuilder.Entity<Invoice>().Property(u => u.InvoiceNumber).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            //Create a computed column of TotalPrice (UnitPrice * Quantity).
            modelBuilder.Entity<InvoiceLineItem>()
               .Property(u => u.TotalPrice)
               .HasComputedColumnSql("[UnitPrice] * [Quantity]");

            base.OnModelCreating(modelBuilder);
        }
    }
}