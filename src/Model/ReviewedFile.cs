namespace StandaloneReview.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Properties;

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
                sb.Append(Resources.ReviewdFileToStringFile);
                sb.Append(" ");
                sb.AppendLine(Filename);
                foreach (var reviewComment in Comments.OrderBy(p => p.Position))
                {
                    sb.Append(reviewComment);
                }
            }
            return sb.ToString();
        }

        public void RemoveComment(ReviewComment commentToRemove)
        {
            Comments.Remove(commentToRemove);
            foreach (var comment in Comments.Where(comment => comment.Position > commentToRemove.Position))
            {
                comment.Position = comment.Position - 1;
            }
        }
    }
}
