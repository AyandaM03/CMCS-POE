using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
