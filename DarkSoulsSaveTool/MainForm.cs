using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Hiale.DarkSoulsSaveTool
{
    public partial class MainForm : Form
    {
        private List<GameSaveData> _gameSaveDataList; 

        private Settings _settings;

        public MainForm()
        {
            InitializeComponent();
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            lblCopyright.Text = string.Format("Version {0}, {1}", Assembly.GetExecutingAssembly().GetName().Version, lblCopyright.Text);
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
            GameSaveData gameSaveData = new DarkSoulsSaveData();
            if (gameSaveData.Init())
            {
                SetGameDataSettings(gameSaveData);
                darkSoulsProperties.SourceFilePattern = gameSaveData.FilePattern;
                _gameSaveDataList.Add(gameSaveData);
                darkSoulsProperties.Tag = gameSaveData;
            }
            gameSaveData = new DarkSouls2SaveData();
            if (gameSaveData.Init())
            {
                SetGameDataSettings(gameSaveData);
                darkSouls2Properties.SourceFilePattern = gameSaveData.FilePattern;
                _gameSaveDataList.Add(gameSaveData);
                darkSouls2Properties.Tag = gameSaveData;
            }
            if (_gameSaveDataList.Count < 1 || !validSettings)
                _mAllowVisible = true;
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

        #region Tray

        bool _mAllowVisible;     // ContextMenu's Show command used
        bool _mAllowClose;       // ContextMenu's Exit command used
        bool _mLoadFired;        // Form was shown once

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

            var gameSaveData = (GameSaveData) darkSoulsProperties.Tag;
            darkSoulsProperties.SourceFile = gameSaveData.SourceFile;
            darkSoulsProperties.TargetDirectory = gameSaveData.TargetDirectory;
            gameSaveData = (GameSaveData) darkSouls2Properties.Tag;
            darkSouls2Properties.SourceFile = gameSaveData.SourceFile;
            darkSouls2Properties.TargetDirectory = gameSaveData.TargetDirectory;
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
                File.Copy(gameSaveData.SourceFile, string.Format("{0}-{1}", targetFile, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)));
                System.Media.SystemSounds.Beep.Play();
                var control = GetGamePropertiesControl(gameSaveData);
                if (control != null)
                    control.SetStatus(string.Format("Success! Backup created!"));
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Exclamation.Play();
                var control = GetGamePropertiesControl(gameSaveData);
                if (control != null)
                   control.SetStatus(string.Format("Error: {0}", ex.Message), true);
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
                System.Media.SystemSounds.Beep.Play();
                var control = GetGamePropertiesControl(gameSaveData);
                if (control != null)
                    control.SetStatus(string.Format("Success! Backup restored!"));
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Exclamation.Play();
                var control = GetGamePropertiesControl(gameSaveData);
                if (control != null)
                    control.SetStatus(string.Format("Error: {0}", ex.Message), true);
            }
        }

        private GameSaveData IsDarkSouls()
        {
            string windowTitle = Win32.GetActiveWindowTitle();
            return _gameSaveDataList.FirstOrDefault(gameSaveData => windowTitle == gameSaveData.WindowTitle);
        }

        private GamePropertiesControl GetGamePropertiesControl(GameSaveData gameSaveData)
        {
            if (darkSoulsProperties.Tag == gameSaveData)
                return darkSoulsProperties;
            if (darkSouls2Properties.Tag == gameSaveData)
                return darkSouls2Properties;
            return null;
        }

        private void SaveSettings()
        {
            UnregisterHotKeys();
            _settings.SaveKey = (Keys)cmbSaveKey.SelectedItem;
            _settings.LoadKey = (Keys)cmbLoadKey.SelectedItem;
            _settings.AddSubSettingsValue(darkSoulsProperties.Tag, "SourceFile", darkSoulsProperties.SourceFile);
            _settings.AddSubSettingsValue(darkSoulsProperties.Tag, "TargetDirectory", darkSoulsProperties.TargetDirectory);
            SetGameDataSettings((GameSaveData) darkSoulsProperties.Tag);
            _settings.AddSubSettingsValue(darkSouls2Properties.Tag, "SourceFile", darkSouls2Properties.SourceFile);
            _settings.AddSubSettingsValue(darkSouls2Properties.Tag, "TargetDirectory", darkSouls2Properties.TargetDirectory);
            SetGameDataSettings((GameSaveData)darkSouls2Properties.Tag);
            RegisterHotKeys();
            try
            {
                _settings.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n{1}", "Settings could not be saved:", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBoxDeleteSettings_Click(object sender, EventArgs e)
        {
            // ReSharper disable LocalizableElement
            if (MessageBox.Show("Are you sure you want to delete your settings?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            Settings.Delete();
            MessageBox.Show("Settings deleted. This program will close now. You can then delete this folder.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            _mAllowClose = true;
            Close();
            // ReSharper restore LocalizableElement
        }
    }
}
