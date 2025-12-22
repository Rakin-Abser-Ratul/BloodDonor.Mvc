using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonor.Mvc.Models.Entities
{
    public class Donation : BaseEntity
    {
        
        public required DateTime DonationDate { get; set; }

        [ForeignKey("BloodDonor")]
        public required int BloodDonorId { get; set; }
    }
}
