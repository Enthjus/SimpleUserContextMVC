using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;

namespace SimpleUser.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                        .HasOne(e => e.CustomerDetail)
                        .WithOne(e => e.Customer)
                        .HasForeignKey<CustomerDetail>("CustomerId")
                        .IsRequired();
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<CustomerDetail>().ToTable("CustomerDetail");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        //}
    }
}
