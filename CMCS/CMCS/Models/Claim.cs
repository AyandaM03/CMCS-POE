using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCS.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        [Display(Name = "Lecturer Name")]
        public string LecturerName { get; set; }

        [Required]
        [Display(Name = "Hours Worked")]
        public double HoursWorked { get; set; }

        [Required]
        [Display(Name = "Hourly Rate (R)")]
        public double HourlyRate { get; set; }

        [NotMapped]
        [Display(Name = "Total Amount (R)")]
        public double TotalAmount => HoursWorked * HourlyRate;

        [Display(Name = "Notes (Optional)")]
        public string? Notes { get; set; }

        public string Status { get; set; } = "Pending";

        public string? SupportingDocument { get; set; }
    }
}

