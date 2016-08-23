namespace StandaloneReview.Contracts
{
    public interface ISystemIO
    {
        string FileReadAllText(string filename);
        void WriteAllText(string filename, string text);
        string PathGetExtension(string filename);
        string PathGetFilename(string filename);
    }
}