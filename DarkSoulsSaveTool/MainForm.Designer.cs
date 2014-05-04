namespace Hiale.DarkSoulsSaveTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.PictureBox pictureBoxDeleteSettings;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTrayShow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLoadKey = new System.Windows.Forms.ComboBox();
            this.cmbSaveKey = new System.Windows.Forms.ComboBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.darkSouls2Properties = new Hiale.DarkSoulsSaveTool.GamePropertiesControl();
            this.darkSoulsProperties = new Hiale.DarkSoulsSaveTool.GamePropertiesControl();
            this.toolTipDeleteSettings = new System.Windows.Forms.ToolTip(this.components);
            pictureBoxDeleteSettings = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxDeleteSettings)).BeginInit();
            this.trayMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxDeleteSettings
            // 
            pictureBoxDeleteSettings.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDeleteSettings.Image")));
            pictureBoxDeleteSettings.Location = new System.Drawing.Point(386, 285);
            pictureBoxDeleteSettings.Name = "pictureBoxDeleteSettings";
            pictureBoxDeleteSettings.Size = new System.Drawing.Size(24, 24);
            pictureBoxDeleteSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxDeleteSettings.TabIndex = 8;
            pictureBoxDeleteSettings.TabStop = false;
            this.toolTipDeleteSettings.SetToolTip(pictureBoxDeleteSettings, "Delete Settings");
            pictureBoxDeleteSettings.Click += new System.EventHandler(this.pictureBoxDeleteSettings_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Text = "Dark Souls Save Tool";
            this.trayIcon.Visible = true;
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTrayShow,
            this.mnuTrayExit});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(113, 48);
            // 
            // mnuTrayShow
            // 
            this.mnuTrayShow.Name = "mnuTrayShow";
            this.mnuTrayShow.Size = new System.Drawing.Size(112, 22);
            this.mnuTrayShow.Text = "Show...";
            this.mnuTrayShow.Click += new System.EventHandler(this.MnuTrayShowClick);
            // 
            // mnuTrayExit
            // 
            this.mnuTrayExit.Name = "mnuTrayExit";
            this.mnuTrayExit.Size = new System.Drawing.Size(112, 22);
            this.mnuTrayExit.Text = "Exit";
            this.mnuTrayExit.Click += new System.EventHandler(this.MnuTrayExitClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(416, 286);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbLoadKey);
            this.groupBox2.Controls.Add(this.cmbSaveKey);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 56);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keys";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Load";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Save";
            // 
            // cmbLoadKey
            // 
            this.cmbLoadKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLoadKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadKey.FormattingEnabled = true;
            this.cmbLoadKey.Location = new System.Drawing.Point(333, 19);
            this.cmbLoadKey.Name = "cmbLoadKey";
            this.cmbLoadKey.Size = new System.Drawing.Size(221, 21);
            this.cmbLoadKey.TabIndex = 1;
            // 
            // cmbSaveKey
            // 
            this.cmbSaveKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaveKey.FormattingEnabled = true;
            this.cmbSaveKey.Location = new System.Drawing.Point(44, 19);
            this.cmbSaveKey.Name = "cmbSaveKey";
            this.cmbSaveKey.Size = new System.Drawing.Size(221, 21);
            this.cmbSaveKey.TabIndex = 0;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCopyright.Location = new System.Drawing.Point(12, 296);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(144, 13);
            this.lblCopyright.TabIndex = 7;
            this.lblCopyright.Text = "Copyright © Hiale 2013-2014";
            // 
            // darkSouls2Properties
            // 
            this.darkSouls2Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.darkSouls2Properties.Location = new System.Drawing.Point(12, 180);
            this.darkSouls2Properties.Name = "darkSouls2Properties";
            this.darkSouls2Properties.Size = new System.Drawing.Size(560, 100);
            this.darkSouls2Properties.SourceFile = "";
            this.darkSouls2Properties.SourceFilePattern = null;
            this.darkSouls2Properties.TabIndex = 6;
            this.darkSouls2Properties.TargetDirectory = "";
            this.darkSouls2Properties.Title = "Dark Souls 2";
            // 
            // darkSoulsProperties
            // 
            this.darkSoulsProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.darkSoulsProperties.Location = new System.Drawing.Point(12, 74);
            this.darkSoulsProperties.Name = "darkSoulsProperties";
            this.darkSoulsProperties.Size = new System.Drawing.Size(560, 100);
            this.darkSoulsProperties.SourceFile = "";
            this.darkSoulsProperties.SourceFilePattern = null;
            this.darkSoulsProperties.TabIndex = 5;
            this.darkSoulsProperties.TargetDirectory = "";
            this.darkSoulsProperties.Title = "Dark Souls";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 315);
            this.Controls.Add(pictureBoxDeleteSettings);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.darkSouls2Properties);
            this.Controls.Add(this.darkSoulsProperties);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "MainForm";
            this.Text = "Dark Souls Save Tool";
            ((System.ComponentModel.ISupportInitialize)(pictureBoxDeleteSettings)).EndInit();
            this.trayMenu.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuTrayShow;
        private System.Windows.Forms.ToolStripMenuItem mnuTrayExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLoadKey;
        private System.Windows.Forms.ComboBox cmbSaveKey;
        private GamePropertiesControl darkSoulsProperties;
        private GamePropertiesControl darkSouls2Properties;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.ToolTip toolTipDeleteSettings;
    }
}

