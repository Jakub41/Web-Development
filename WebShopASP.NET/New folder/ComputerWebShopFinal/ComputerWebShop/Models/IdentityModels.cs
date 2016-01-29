using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ComputerWebShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ConfirmationToken { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Products> Products  { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<Retrievepassword> Retrievepassword { get; set; }
        public DbSet<ProductsComments> ProductsComments { get; set; }
    }
}