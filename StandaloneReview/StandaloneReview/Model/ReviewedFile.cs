namespace StandaloneReview.Model
{
    using System.Collections.Generic;

    public class ReviewedFile
    {
        public string Filename { get; set; }
        public List<ReviewComment> Comments { get; set; }
    }
}
