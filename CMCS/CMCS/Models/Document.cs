namespace CMCS.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        // Foreign Key
        public int ClaimID { get; set; }
        public Claim? Claim { get; set; }
    }
}
