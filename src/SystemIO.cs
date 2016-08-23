using StandaloneReview.Contracts;

namespace StandaloneReview
{
    using System.IO;

    public class SystemIO : ISystemIO
    {
        public string FileReadAllText(string filename)
        {
            return File.ReadAllText(filename);
        }

        public void WriteAllText(string filename, string text)
        {
            File.WriteAllText(filename, text);
        }

        public string PathGetExtension(string filename)
        {
            return Path.GetExtension(filename);
        }

        public string PathGetFilename(string filename)
        {
            return Path.GetFileName(filename);
        }
    }
}
