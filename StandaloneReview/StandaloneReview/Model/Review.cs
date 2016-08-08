namespace StandaloneReview.Model
{
    using System;
    using System.Collections.Generic;

    public class Review
    {
        public DateTime ReviewTime { get; set; }
        public string CommitId { get; set; }
        public Dictionary<string, ReviewedFile> ReviewedFiles { get; set; }
    }
}
