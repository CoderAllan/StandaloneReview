namespace StandaloneReview.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("AppState")]
    public class ApplicationState
    {
        [XmlIgnore] 
        public static readonly string AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StandaloneReview");
        [XmlIgnore]
        public Review CurrentReview = new Review
            {
                ReviewTime = DateTime.Now,
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
        [XmlIgnore]
        public ReviewedFile CurrentReviewedFile;
        [XmlIgnore]
        public ReviewComment WorkingComment;

        public string ApplicationLocale;
        public int FrmStandaloneReviewWidth;
        public int FrmStandaloneReviewHeight;
        public int FrmStandaloneReviewPosX;
        public int FrmStandaloneReviewPosY;
        public int FrmInsertCommentWidth;
        public int FrmInsertCommentHeight;
        public int FrmInsertCommentPosX;
        public int FrmInsertCommentPosY;
        public int FrmPreviewWidth;
        public int FrmPreviewHeight;
        public int FrmPreviewPosX;
        public int FrmPreviewPosY;
        public int FrmOptionsWidth;
        public int FrmOptionsHeight;
        public int FrmOptionsPosX;
        public int FrmOptionsPosY;

        public static void WriteApplicationState(ApplicationState state)
        {
            XmlTextWriter writer = null;
            try
            {
                var s = new XmlSerializer(typeof (ApplicationState));
                if (!Directory.Exists(AppFolder))
                {
                    Directory.CreateDirectory(AppFolder);
                }
                writer = new XmlTextWriter(new StreamWriter(Path.Combine(AppFolder, "AppState.xml"), false));
                s.Serialize(writer, state);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static ApplicationState ReadApplicationState()
        {
            XmlTextReader reader = null;
            try
            {
                string runLogPath = Path.Combine(AppFolder, "AppState.xml");
                if (File.Exists(runLogPath))
                {
                    reader = new XmlTextReader(new StreamReader(runLogPath, false));
                }
                if (reader == null)
                {
                    return new ApplicationState();
                }
                var xmlSerializer = new XmlSerializer(typeof (ApplicationState));
                return (ApplicationState) xmlSerializer.Deserialize(reader);
            }
            catch
            {
                return new ApplicationState();
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public void ReadComboBoxElements(ComboBox combobox, List<string> appStateList, Action<string, int> addElementToComboBox)
        {
            if (appStateList != null && (appStateList.Count > 0))
            {
                for (int i = appStateList.Count() - 1; i >= 0; i--) 
                {
                    if (combobox.Items.Count >= 20) break;
                    addElementToComboBox(appStateList[i], i);
                }
                if (combobox.Items.Count > 0)
                {
                    combobox.SelectedIndex = 0;
                }
            }
        }

        internal void PersistFrmStandaloneReview(FrmStandaloneReview form)
        {
            FrmStandaloneReviewHeight = form.Height;
            FrmStandaloneReviewWidth = form.Width;
            FrmStandaloneReviewPosX = form.Location.X;
            FrmStandaloneReviewPosY = form.Location.Y;
        }

        internal void PersistFrmInsertComment(FrmInsertComment form)
        {
            FrmInsertCommentHeight = form.Height;
            FrmInsertCommentWidth = form.Width;
            FrmInsertCommentPosX = form.Location.X;
            FrmInsertCommentPosY = form.Location.Y;
        }

        internal void PersistFrmPreview(FrmPreview form)
        {
            FrmPreviewHeight = form.Height;
            FrmPreviewWidth = form.Width;
            FrmPreviewPosX = form.Location.X;
            FrmPreviewPosY = form.Location.Y;
        }

        internal void PersistFrmOptions(FrmOptions form)
        {
            FrmOptionsHeight = form.Height;
            FrmOptionsWidth = form.Width;
            FrmOptionsPosX = form.Location.X;
            FrmOptionsPosY = form.Location.Y;
        }
    }
}
