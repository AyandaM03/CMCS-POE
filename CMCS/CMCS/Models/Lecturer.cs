using System.Security.Claims;

namespace CMCS.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Foreign Key
        public int RoleID { get; set; }
        public UserRole? Role { get; set; }

        // Navigation
        public List<Claim>? Claims { get; set; }
    }
}
