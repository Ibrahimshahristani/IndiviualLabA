using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IndiviualLabA.Data
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public decimal Total { get; set; }

        // Parent.
        public virtual CustomUser CustomnUser { get; set; }
    }

    public class CustomUser
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Child
        public virtual ICollection<Invoice> Invoices { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define entity collections.
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<CustomUser> CustomUsers { get; set; }

        // Use this method to define composite primary keys and foreign keys.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // required.
            base.OnModelCreating(modelBuilder);

            //-------------------------------------------------------
            // *** Define composite primary keys here. ***

            // This is sample syntax for defining a primary key.
            // modelBuilder.Entity<ProductSupplier>()
            //             .HasKey(ps => new { ps.ProductID, ps.SupplierID });

            //-------------------------------------------------------
            // *** Define composite foreign keys here. ***
            modelBuilder.Entity<Invoice>()
                .HasOne(c => c.CustomnUser)
                .WithMany(c => c.Invoices)
                .HasForeignKey(fk => new { fk.UserName })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }

}
