namespace StandaloneReview.Model
{
    using System.Globalization;
    using System.Text;

    public class ReviewComment
    {
        public int Line { get; set; }
        public string LineText { get; set; }
        public int SelectionStartLine { get; set; }
        public int SelectionStartColumn { get; set; }
        public int SelectionEndLine { get; set; }
        public int SelectionEndColumn { get; set; }
        public string SelectedText { get; set; }
        public string Comment { get; set; }
        public int Position { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Comment))
            {
                if (SelectionStartLine > 0)
                {
                    sb.Append("Ln ");
                    sb.Append(SelectionStartLine.ToString(CultureInfo.InvariantCulture));
                    sb.Append(",Col ");
                    sb.Append(SelectionStartColumn.ToString(CultureInfo.InvariantCulture));
                    sb.Append(" - ");
                    sb.Append("Ln ");
                    sb.Append(SelectionEndLine.ToString(CultureInfo.InvariantCulture));
                    sb.Append(",Col ");
                    sb.Append(SelectionEndColumn.ToString(CultureInfo.InvariantCulture));
                    sb.AppendLine(": ");
                    sb.AppendLine(SelectedText);
                    sb.Append("* ");
                    sb.AppendLine(Comment);
                }
                else
                {
                    sb.Append("Linje ");
                    sb.Append(Line.ToString(CultureInfo.InvariantCulture));
                    sb.AppendLine(": ");
                    sb.AppendLine(LineText);
                    sb.Append("* ");
                    sb.AppendLine(Comment);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
