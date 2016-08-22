namespace StandaloneReview.Model
{
    public class ListboxFilesItem
    {
        public string Filename { get; set; }
        public string FullFilename { get; set; }
        
        public override string ToString()
        {
            return Filename;
        }
    }
}
