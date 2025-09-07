using System.Reflection.Metadata;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Status { get; set; } = "Pending";

        // Foreign Key
        public int LecturerID { get; set; }
        public Lecturer? Lecturer { get; set; }

        // Navigation
        public List<Document>? Documents { get; set; }
    }
}
