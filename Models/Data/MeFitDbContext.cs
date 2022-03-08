using MeFit.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MeFit.Models.Data
{
    public class MeFitDbContext : DbContext
    {
        public  DbSet<Address> Addresses { get; set; }

        public MeFitDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(new Address
            {
                Id = 1,
                AddressLine1 = "eef",
                AddressLine2 = "fkr",
                AddressLine3 = "efe",
                City =  "jfr",
                PostalCode = "1123",
                Contry = "fw4w"
            });
        }

    }
}
