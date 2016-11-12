namespace StandaloneReview.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Review
    {
        public DateTime ReviewTime { get; set; }
        public string CommitId { get; set; }
        public Dictionary<string, ReviewedFile> ReviewedFiles { get; set; }
        public bool Saved { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (ReviewedFiles != null && ReviewedFiles.Count > 0)
            {
                sb.Append("Review - Dato: ");
                sb.AppendLine(ReviewTime.ToString("dd-MM-yyyy"));
                foreach (var reviewedFile in ReviewedFiles.OrderBy(p => ReviewedFiles[p.Key].Position))
                {
                    sb.Append(ReviewedFiles[reviewedFile.Key]);
                }
            }
            return sb.ToString();
        }

    }
}
