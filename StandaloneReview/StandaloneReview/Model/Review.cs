using System;

namespace StandaloneReview.Model
{
    using System.Collections.Generic;

    public class Review
    {
        public DateTime ReviewTime { get; set; }
        public string CommitId { get; set; }
        public List<ReviewedFile> ReviewdFiles { get; set; }
    }
}
