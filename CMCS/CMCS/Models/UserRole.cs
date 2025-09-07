namespace CMCS.Models
{
    public class UserRole
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Navigation
        public List<Lecturer>? Lecturers { get; set; }
    }
}
