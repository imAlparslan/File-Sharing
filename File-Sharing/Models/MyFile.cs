namespace File_Sharing.Models
{
    public class MyFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double FileSize { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
