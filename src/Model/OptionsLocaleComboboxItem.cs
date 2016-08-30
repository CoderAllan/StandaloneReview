namespace StandaloneReview.Model
{
    public class OptionsLocaleComboboxItem
    {
        public string Text { get; set; }
        public string Locale { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
