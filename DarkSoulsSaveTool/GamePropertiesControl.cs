using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Hiale.DarkSoulsSaveTool
{
    public partial class GamePropertiesControl : UserControl
    {
        private static readonly Color Success = Color.Green;
        private static readonly Color Failure = Color.Red;

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string Title
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string SourceFile
        {
            get { return txtSourceFile.Text; }
            set { txtSourceFile.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string TargetDirectory
        {
            get { return txtTargetDirectory.Text; }
            set { txtTargetDirectory.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string SourceFilePattern { get; set; }

        public GamePropertiesControl()
        {
            InitializeComponent();
        }

        public void SetStatus(string text, bool failure = false)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = failure ? Failure : Success;
        }

        private void UpdateStatus()
        {
            // ReSharper disable LocalizableElement
            var sourceOk = File.Exists(txtSourceFile.Text);
            var targetOk = Directory.Exists(txtTargetDirectory.Text) && Helper.HasWriteAccess(txtTargetDirectory.Text);
            if (sourceOk && targetOk)
            {
                lblStatus.Text = "Ready to play the game!";

                lblStatus.ForeColor = Success;
                return;
            }
            lblStatus.ForeColor = Failure;
            if (!sourceOk && !targetOk)
            {
                lblStatus.Text = "The source file and the target directory must be set!";
                return;
            }
            if (!sourceOk)
            {
                lblStatus.Text = "The source file does not exist!";

                return;
            }
            lblStatus.Text = "The target directory is invalid!";
            // ReSharper restore LocalizableElement
        }

        private void btnChooseSource_Click(object sender, System.EventArgs e)
        {
            var dlg = new OpenFileDialog();
            var dir = Path.GetDirectoryName(txtSourceFile.Text);
            if (!string.IsNullOrEmpty(SourceFilePattern))
                dlg.Filter = string.Format("Save files|{0}|All files (*.*)|*.*", SourceFilePattern);
            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                dlg.InitialDirectory = dir;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtSourceFile.Text = dlg.FileName;
            }
        }

        private void btnChooseTarget_Click(object sender, System.EventArgs e)
        {
            var dlg = new FolderBrowserDialog {ShowNewFolderButton = true};
            if (Directory.Exists(txtTargetDirectory.Text))
            {
                dlg.SelectedPath = txtTargetDirectory.Text;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTargetDirectory.Text = dlg.SelectedPath;
            }
        }

        private void TextBoxTextChanged(object sender, System.EventArgs e)
        {
            UpdateStatus();
        }
    }
}
