using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.Entitites;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Data
{
    public class BloodDonorDbContext:DbContext
    {
        
        public BloodDonorDbContext(DbContextOptions<BloodDonorDbContext> options) : base(options)
        {
        }
        public DbSet<BloodDonorEntity> BloodDonors { get; set; }
        public DbSet<Donation> Donations { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BloodDonorEntity>()
                .HasData(            
                    new BloodDonorEntity
                    {
                        Id= 1,
                        FullName = "John Doe",
                        Email = "john.doe@example.com",
                        DateOfBirth = new DateTime(1990, 5, 15),
                        ContactNumber = "123-456-7890",
                        BloodGroup = BloodGroup.APositive,
                        ProfilePicture = null,
                        Weight = 70,
                        Address = "123 Main St, Anytown, USA",
                        LastDonationDate = new DateTime(2023, 1, 10)
                    },
                    new BloodDonorEntity
                    {
                        Id = 2,
                        FullName = "Jane Smith",
                        Email = "jane.smith@example.com",
                        DateOfBirth = new DateTime(1985, 8, 25),
                        ContactNumber = "987-654-3210",
                        BloodGroup = BloodGroup.BNegative,
                        ProfilePicture = null,
                        Weight = 70,
                        Address = "123 Main St, Anytown, USA",
                        LastDonationDate = new DateTime(2023, 1, 10)
                    }
                        
                );
        }
            


    }
}
