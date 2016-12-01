namespace StandaloneReview
{
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    using Contracts;

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

        public string PathGetFoldername(string filename)
        {
            return Path.GetDirectoryName(filename);
        }

        public void OpenFolderInExplorer(string foldername)
        {
            Process.Start(foldername);
        }

        public void CopyToClipboard(string text)
        {
            Clipboard.SetText(text);
        }
    }
}
