namespace StandaloneReview.Model
{
    using System.Collections.Generic;
    using System.Text;


    public class ReviewedFile
    {
        public string Filename { get; set; }
        public int Position { get; set; }
        public List<ReviewComment> Comments { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Comments != null && Comments.Count > 0)
            {
                sb.Append("Fil: ");
                sb.AppendLine(Filename);
                foreach (var reviewComment in Comments)
                {
                    sb.Append(reviewComment);
                }
            }
            return sb.ToString();
        }
    }
}
