using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Hiale.DarkSoulsSaveTool
{
    public partial class GamePropertiesControl : UserControl
    {
        public GamePropertiesControl()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string Title
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string SourceFile
        {
            get { return txtSourceFile.Text; }
            set { txtSourceFile.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string TargetDirectory
        {
            get { return txtTargetDirectory.Text; }
            set { txtTargetDirectory.Text = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public string SourceFilePattern { get; set; }

        public event EventHandler<GenericEventArgs<string>> SourceFileChanged;

        public event EventHandler<GenericEventArgs<string>> TargetDirectoryChanged;

        private void btnChooseSource_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            var dir = Path.GetDirectoryName(txtSourceFile.Text);
            if (!string.IsNullOrEmpty(SourceFilePattern))
                dlg.Filter = $"Save files|{SourceFilePattern}|All files (*.*)|*.*";
            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                dlg.InitialDirectory = dir;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtSourceFile.Text = dlg.FileName;
            }
        }

        private void btnChooseTarget_Click(object sender, EventArgs e)
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

        private void txtSourceFile_TextChanged(object sender, EventArgs e)
        {
            SourceFileChanged?.Invoke(sender, new GenericEventArgs<string>(txtSourceFile.Text));
        }

        private void txtTargetDirectory_TextChanged(object sender, EventArgs e)
        {
            TargetDirectoryChanged?.Invoke(sender, new GenericEventArgs<string>(txtTargetDirectory.Text));
        }
    }
}