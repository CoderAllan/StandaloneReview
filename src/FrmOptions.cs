namespace StandaloneReview
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Model;
    using Views;
    using Presenters;
    using Properties;

    public partial class FrmOptions : Form, IBaseForm
    {
        private readonly ApplicationState _appState;
        private readonly BaseFormPresenter _baseFormPresenter;

        public event EventHandler<BaseFormEventArgs> DoFormLoad;

        public FrmOptions(ApplicationState appState)
        {
            _appState = appState;
            _baseFormPresenter = new BaseFormPresenter(this);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmOptionsHeight,
                Width = _appState.FrmOptionsWidth,
                Location = new Point(_appState.FrmOptionsPosX, _appState.FrmOptionsPosY)
            };
            DoFormLoad(this, eventArgs);
        }

        private void FrmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            var frmOptions = (FrmOptions)sender;
            _appState.PersistFrmOptions(frmOptions);
            ApplicationState.WriteApplicationState(_appState);
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            var locale = new OptionsLocaleComboboxItem { Locale = "da-DK", Text = Resources.OptionsLocaleDanish };
            comboBox1.Items.Add(locale);
            if (string.Compare(_appState.ApplicationLocale, locale.Locale, StringComparison.OrdinalIgnoreCase) == 0)
            {
                comboBox1.SelectedItem = locale;
            }
            locale = new OptionsLocaleComboboxItem { Locale = "", Text = Resources.OptionsLocaleDefault };
            comboBox1.Items.Insert(0, locale);
            if (string.IsNullOrWhiteSpace(_appState.ApplicationLocale))
            {
                comboBox1.SelectedItem = locale;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            var optionsLocaleComboboxItem = (OptionsLocaleComboboxItem)comboBox1.SelectedItem;
            _appState.ApplicationLocale = optionsLocaleComboboxItem.Locale;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
