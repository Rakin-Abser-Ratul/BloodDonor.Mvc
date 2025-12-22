using BloodDonor.Mvc.Models.Entitites;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonor.Mvc.Models.Entities
{
    public class BloodDonorEntity : BaseEntity
    {

        public required string FullName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }

        public required DateTime DateOfBirth { get; set; }
        [Phone]
        [Length(10, 15)]
        public required string ContactNumber { get; set; }
        public required BloodGroup BloodGroup { get; set; }

        public string? ProfilePicture { get; set; }

        [Range(50, 150)]
        [Display(Name = "Weight (kg)")]

        public float Weight { get; set; }

        public string? Address { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public Collection<Donation> Donations { get; set; } = new Collection<Donation>();


    }

   
   

}