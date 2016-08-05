namespace StandaloneReview.Model
{
    public class ReviewComment
    {
        public int Line { get; set; }
	    public int SelectionStartLine { get; set; }
	    public int SelectionStartColoumn { get; set; }
	    public int SelectionEndLine { get; set; }
	    public int SelectionEndColoumn { get; set; }
        public string Comment { get; set; }
    }
}
