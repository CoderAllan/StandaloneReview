namespace StandaloneReview
{
    public interface ISystemIO
    {
        string FileReadAllText(string filename);
        string PathGetExtension(string filename);
    }
}