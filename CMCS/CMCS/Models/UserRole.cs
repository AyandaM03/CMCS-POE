using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class UserRole
    {
        [Key] 
        public int RoleID { get; set; }

        public string RoleName { get; set; } = string.Empty;

        // Navigation property
        public List<Lecturer>? Lecturers { get; set; }
    }
}

