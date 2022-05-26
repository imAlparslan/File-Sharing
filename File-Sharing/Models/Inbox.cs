namespace File_Sharing.Models
{
    public class Inbox
    {
        public int DocumentId { get; set; }
        public string FileOwnerName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double FileSize { get; set; }
        public DateTime SharingDate { get; set; }
    }
}
