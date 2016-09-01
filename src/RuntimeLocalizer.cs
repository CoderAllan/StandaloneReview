// This class is copied from:  http://stackoverflow.com/a/10389737/57855
namespace StandaloneReview
{
    using System.Linq;
    using System.Windows.Forms;
    using System.Globalization;
    using System.Threading;
    using System.ComponentModel;

    public static class RuntimeLocalizer
    {
        public static void ChangeCulture(string cultureCode)
        {
            CultureInfo culture = CultureInfo.GetCultureInfo(cultureCode ?? "");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            foreach (var form in Application.OpenForms.OfType<Form>())
            {
                int formWidth = form.Width;
                int formHeight = form.Height;
                var resources = new ComponentResourceManager(form.GetType());
                ApplyResourceToControl(resources, form, culture);
                resources.ApplyResources(form, "$this", culture);
                form.Width = formWidth; // I dont know why the height and width changes when the localization is applyed
                form.Height = formHeight;
            }
        }

        private static void ApplyResourceToControl(ComponentResourceManager res, Control control, CultureInfo lang)
        {
            if (control.GetType() == typeof(MenuStrip))  // See if this is a menuStrip
            {
                var strip = (MenuStrip)control;

                ApplyResourceToToolStripItemCollection(strip.Items, res, lang);
            }

            foreach (Control c in control.Controls) // Apply to all sub-controls
            {
                ApplyResourceToControl(res, c, lang);
                res.ApplyResources(c, c.Name, lang);
            }

            // Apply to self
            res.ApplyResources(control, control.Name, lang);
        }

        private static void ApplyResourceToToolStripItemCollection(ToolStripItemCollection col, ComponentResourceManager res, CultureInfo lang)
        {
            for (int i = 0; i < col.Count; i++)     // Apply to all sub items
            {
                ToolStripItem item = col[i];

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    var menuitem = (ToolStripMenuItem)item;
                    ApplyResourceToToolStripItemCollection(menuitem.DropDownItems, res, lang);
                }

                res.ApplyResources(item, item.Name, lang);
            }
        }
    }
}
