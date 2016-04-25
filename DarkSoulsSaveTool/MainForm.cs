using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace Hiale.DarkSoulsSaveTool
{
    // ReSharper disable LocalizableElement
    public partial class MainForm : Form
    {
        private static readonly Color Success = Color.Green;
        private static readonly Color Failure = Color.Red;
        private List<GameSaveData> _gameSaveDataList;

        private Settings _settings;

        public MainForm()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            lblCopyright.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version}, {lblCopyright.Text}";
            gameProperties.SourceFileChanged += GameProperties_SourceFileChanged;
            gameProperties.TargetDirectoryChanged += GameProperties_TargetDirectoryChanged;
            Init();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void Init()
        {
            var validSettings = Settings.Load(out _settings);
            _gameSaveDataList = new List<GameSaveData>();

            var gameSaveDataTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof (GameSaveData)));
            foreach (var gameSaveDataType in gameSaveDataTypes)
            {
                var gameSaveData = (GameSaveData) Activator.CreateInstance(gameSaveDataType);
                if (!gameSaveData.Init())
                    continue;
                SetGameDataSettings(gameSaveData);
                _gameSaveDataList.Add(gameSaveData);
                cmbGameList.Items.Add(gameSaveData);
            }

            if (_gameSaveDataList.Count < 1 || !validSettings)
            {
                _mAllowVisible = true;
                SetStatus("No Game found", true);
                cmbGameList.Items.Add(lblStatus.Text);
            }
            cmbGameList.SelectedIndex = 0;
            trayIcon.Icon = Icon;
            RegisterHotKeys();
        }

        private void SetGameDataSettings(GameSaveData gameSaveData)
        {
            var sourceFile = _settings.GetSubSettingsValue(gameSaveData, "SourceFile");
            if (!string.IsNullOrEmpty(sourceFile))
                gameSaveData.SourceFile = sourceFile;
            var targetDirectory = _settings.GetSubSettingsValue(gameSaveData, "TargetDirectory");
            if (!string.IsNullOrEmpty(targetDirectory))
                gameSaveData.TargetDirectory = targetDirectory;
        }

        private void RegisterHotKeys()
        {
            KeyboardHook.Hook();
            KeyboardHook.RegisterKey(_settings.SaveKey, BackupCommand);
            KeyboardHook.RegisterKey(_settings.LoadKey, RestoreCommand);
        }

        private void UnregisterHotKeys()
        {
            KeyboardHook.UnregisterKey(_settings.SaveKey);
            KeyboardHook.UnregisterKey(_settings.LoadKey);
            KeyboardHook.Unhook();
        }

        private void BackupCommand(Keys key)
        {
            var gameSaveData = IsDarkSouls();
            if (gameSaveData == null)
                return;
            try
            {
                if (!File.Exists(gameSaveData.SourceFile))
                    throw new FileNotFoundException("Source file does not exist!");
                var targetFilename = Path.GetFileName(gameSaveData.SourceFile);
                if (targetFilename == null)
                    return;
                var targetFile = Path.Combine(gameSaveData.TargetDirectory, targetFilename);
                File.Copy(gameSaveData.SourceFile, targetFile, true);
                File.Copy(gameSaveData.SourceFile,
                    $"{targetFile}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)}");
                SystemSounds.Beep.Play();
                SetStatus($"{gameSaveData.Name} - Success! Backup created!");
            }
            catch (Exception ex)
            {
                SystemSounds.Exclamation.Play();
                SetStatus($"{gameSaveData.Name} - Error: {ex.Message}", true);
            }
        }

        private void RestoreCommand(Keys key)
        {
            var gameSaveData = IsDarkSouls();
            if (gameSaveData == null)
                return;
            try
            {
                var targetFilename = Path.GetFileName(gameSaveData.SourceFile);
                if (targetFilename == null)
                    return;
                var sourceFile = Path.Combine(gameSaveData.TargetDirectory, targetFilename);
                File.Copy(sourceFile, gameSaveData.SourceFile, true);
                SystemSounds.Beep.Play();
                SetStatus($"{gameSaveData.Name} - Success! Backup restored!");
            }
            catch (Exception ex)
            {
                SystemSounds.Exclamation.Play();
                SetStatus($"{gameSaveData.Name} - Error: {ex.Message}", true);
            }
        }

        private GameSaveData IsDarkSouls()
        {
            var windowTitle = Win32.GetActiveWindowTitle();
            return _gameSaveDataList.FirstOrDefault(gameSaveData => windowTitle == gameSaveData.WindowTitle);
        }


        private void SaveSettings()
        {
            UnregisterHotKeys();
            _settings.SaveKey = (Keys) cmbSaveKey.SelectedItem;
            _settings.LoadKey = (Keys) cmbLoadKey.SelectedItem;
            foreach (var gameSaveData in _gameSaveDataList)
            {
                _settings.AddSubSettingsValue(gameSaveData, "SourceFile", gameSaveData.SourceFile);
                _settings.AddSubSettingsValue(gameSaveData, "TargetDirectory", gameSaveData.TargetDirectory);
                SetGameDataSettings(gameSaveData);
            }
            RegisterHotKeys();
            try
            {
                _settings.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{"Settings could not be saved:"}\n{ex.Message}", Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBoxDeleteSettings_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Are you sure you want to delete your settings?", Text, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            Settings.Delete();
            MessageBox.Show("Settings deleted. This program will close now. You can then delete this folder.", Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            _mAllowClose = true;
            Close();
        }

        private void cmbGameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGameList.SelectedItem is string)
            {
                gameProperties.Title = "Game";
                gameProperties.Enabled = false;
                return;
            }
            var item = cmbGameList.SelectedItem as GameSaveData;
            if (item == null)
                return;
            gameProperties.Title = item.Name;
            gameProperties.SourceFilePattern = item.FilePattern;
            gameProperties.SourceFile = item.SourceFile;
            gameProperties.TargetDirectory = item.TargetDirectory;
            UpdateStatus(item);
        }

        private void GameProperties_SourceFileChanged(object sender, GenericEventArgs<string> e)
        {
            var gameSaveData = cmbGameList.SelectedItem as GameSaveData;
            if (gameSaveData != null)
                gameSaveData.SourceFile = e.Value;
            UpdateStatus(gameSaveData);
        }

        private void GameProperties_TargetDirectoryChanged(object sender, GenericEventArgs<string> e)
        {
            var gameSaveData = cmbGameList.SelectedItem as GameSaveData;
            if (gameSaveData != null)
                gameSaveData.TargetDirectory = e.Value;
            UpdateStatus(gameSaveData);
        }

        private void SetStatus(string text, bool failure = false)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = failure ? Failure : Success;
        }

        private void UpdateStatus(GameSaveData gameSaveData)
        {
            var sourceOk = File.Exists(gameSaveData.SourceFile);
            var targetOk = Directory.Exists(gameSaveData.TargetDirectory) &&
                           Helper.HasWriteAccess(gameSaveData.TargetDirectory);
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
        }

        #region Tray

        private bool _mAllowVisible; // ContextMenu's Show command used
        private bool _mAllowClose; // ContextMenu's Exit command used
        private bool _mLoadFired; // Form was shown once

        protected override void SetVisibleCore(bool value)
        {
            if (!_mAllowVisible)
                value = false;
            base.SetVisibleCore(value);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_mAllowClose)
            {
                Hide();
                e.Cancel = true;
            }
            else
            {
                UnregisterHotKeys();
            }
            base.OnFormClosing(e);
        }

        protected override void OnShown(EventArgs e)
        {
            _mLoadFired = true;
            cmbSaveKey.DataSource = Enum.GetValues(typeof (Keys));
            cmbSaveKey.SelectedItem = _settings.SaveKey;
            cmbLoadKey.DataSource = Enum.GetValues(typeof (Keys));
            cmbLoadKey.SelectedItem = _settings.LoadKey;
        }

        private void MnuTrayShowClick(object sender, EventArgs e)
        {
            _mAllowVisible = true;
            //_mLoadFired = true;
            Show();
        }

        private void MnuTrayExitClick(object sender, EventArgs e)
        {
            _mAllowClose = _mAllowVisible = true;
            if (!_mLoadFired)
                Show();
            Close();
        }

        #endregion
    }

    // ReSharper restore LocalizableElement
}