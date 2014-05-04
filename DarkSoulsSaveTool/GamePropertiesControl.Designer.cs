namespace Hiale.DarkSoulsSaveTool
{
    partial class GamePropertiesControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnChooseTarget = new System.Windows.Forms.Button();
            this.txtTargetDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChooseSource = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.lblStatus);
            this.groupBox.Controls.Add(this.btnChooseTarget);
            this.groupBox.Controls.Add(this.txtTargetDirectory);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.btnChooseSource);
            this.groupBox.Controls.Add(this.txtSourceFile);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(560, 100);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Game Title";
            // 
            // btnChooseTarget
            // 
            this.btnChooseTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseTarget.Location = new System.Drawing.Point(518, 41);
            this.btnChooseTarget.Name = "btnChooseTarget";
            this.btnChooseTarget.Size = new System.Drawing.Size(36, 23);
            this.btnChooseTarget.TabIndex = 3;
            this.btnChooseTarget.Text = "...";
            this.btnChooseTarget.UseVisualStyleBackColor = true;
            this.btnChooseTarget.Click += new System.EventHandler(this.btnChooseTarget_Click);
            // 
            // txtTargetDirectory
            // 
            this.txtTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetDirectory.Location = new System.Drawing.Point(98, 44);
            this.txtTargetDirectory.Name = "txtTargetDirectory";
            this.txtTargetDirectory.Size = new System.Drawing.Size(414, 20);
            this.txtTargetDirectory.TabIndex = 2;
            this.txtTargetDirectory.TextChanged += new System.EventHandler(this.TextBoxTextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Target Directory";
            // 
            // btnChooseSource
            // 
            this.btnChooseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseSource.Location = new System.Drawing.Point(518, 13);
            this.btnChooseSource.Name = "btnChooseSource";
            this.btnChooseSource.Size = new System.Drawing.Size(36, 23);
            this.btnChooseSource.TabIndex = 1;
            this.btnChooseSource.Text = "...";
            this.btnChooseSource.UseVisualStyleBackColor = true;
            this.btnChooseSource.Click += new System.EventHandler(this.btnChooseSource_Click);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFile.Location = new System.Drawing.Point(98, 16);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(414, 20);
            this.txtSourceFile.TabIndex = 0;
            this.txtSourceFile.TextChanged += new System.EventHandler(this.TextBoxTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source File";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 79);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "[Status]";
            // 
            // GamePropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "GamePropertiesControl";
            this.Size = new System.Drawing.Size(560, 100);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btnChooseTarget;
        private System.Windows.Forms.TextBox txtTargetDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChooseSource;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
    }
}
