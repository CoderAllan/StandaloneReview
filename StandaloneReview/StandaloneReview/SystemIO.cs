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

        public string PathGetExtension(string filename)
        {
            return Path.GetExtension(filename);
        }
    }
}
