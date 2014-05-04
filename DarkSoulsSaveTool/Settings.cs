using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Hiale.DarkSoulsSaveTool
{
    public class Settings
    {
        private const string DefaultSettingsFilename = "Settings.xml";
        private const string AlternatePath = "Hiale\\DarkSoulsSaveTool";

        public Keys SaveKey { get; set; }

        public Keys LoadKey { get; set; }

        [XmlIgnore]
        private readonly Dictionary<object, SettingsSubItem> _subItemMap;

        public List<SettingsSubItem> SubItems { get; set; }

        public Settings()
        {
            _subItemMap = new Dictionary<object, SettingsSubItem>();
            SubItems = new List<SettingsSubItem>();
        }

        public static bool Load(out Settings instance)
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(Settings));
                var textReader = new StreamReader(GetSettingsPath());
                var settings = (Settings)deserializer.Deserialize(textReader);
                textReader.Close();
                instance = settings;
                return true;
            }
            catch (Exception)
            {
                instance = new Settings {SaveKey = Keys.F5, LoadKey = Keys.F9};
                return false;
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Settings));
            var textWriter = new StreamWriter(GetSettingsPath());
            serializer.Serialize(textWriter, this);
            textWriter.Close();
        }

        public void AddSubSettingsValue(object instance, string key, string value)
        {
            SettingsSubItem subItem;
            if (!_subItemMap.TryGetValue(instance, out subItem))
            {
                subItem = CheckList(instance);
                if (subItem == null)
                {
                    subItem = new SettingsSubItem(instance);
                    _subItemMap.Add(instance, subItem);
                    SubItems.Add(subItem);
                }
            }
            string existingValue;
            if (subItem.SubItemValues.TryGetValue(key, out existingValue))
                subItem.SubItemValues[key] = value; //replace
            else
                subItem.SubItemValues.Add(key, value);
        }

        public string GetSubSettingsValue(object instance, string key)
        {
            SettingsSubItem subItem;
            if (!_subItemMap.TryGetValue(instance, out subItem))
            {
                subItem = CheckList(instance);
                if (subItem == null)
                    return null;
            }
            string value;
            return subItem.SubItemValues.TryGetValue(key, out value) ? value : null;
        }

        public void RemoveSubSettingsValue(object instance, string key)
        {
            SettingsSubItem subItem;
            if (!_subItemMap.TryGetValue(instance, out subItem))
                return;
            subItem.SubItemValues.Remove(key);
            if (subItem.SubItemValues.Count == 0)
            {
                _subItemMap.Remove(instance);
                SubItems.Remove(subItem);
            }
        }

        private SettingsSubItem CheckList(object instance)
        {
            SettingsSubItem subItem = null;
            var objectType = instance.GetType().ToString();

            foreach (var settingsSubItem in SubItems.Where(settingsSubItem => settingsSubItem.Instance == null && settingsSubItem.ObjectType == objectType))
            {
                settingsSubItem.Instance = instance;
                subItem = settingsSubItem;
                _subItemMap.Add(instance, settingsSubItem);
            }
            return subItem;
        }

        private static string GetSettingsPath()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (path != null && Helper.HasWriteAccess(path))
                return Path.Combine(path, DefaultSettingsFilename);
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, AlternatePath);
            Directory.CreateDirectory(path);
            return Path.Combine(path, DefaultSettingsFilename);
        }

        private static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static void Delete()
        {
            // ReSharper disable EmptyGeneralCatchClause
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (path != null && Helper.HasWriteAccess(path))
            {
                try
                {
                    File.Delete(Path.Combine(path, DefaultSettingsFilename));
                }
                catch (Exception)
                {
                    //ignore
                }
            }
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, AlternatePath);
            if (!Directory.Exists(path))
                return;
            try
            {
                File.Delete(Path.Combine(path, DefaultSettingsFilename));
            }
            catch (Exception)
            {
                //ignore
            }
            if (!IsDirectoryEmpty(path))
                return;
            try
            {
                Directory.Delete(path);
                path = Directory.GetParent(path).FullName;
                if (IsDirectoryEmpty(path))
                    Directory.Delete(path);
            }
            catch (Exception)
            {
                //ignore
            }
            // ReSharper restore EmptyGeneralCatchClause
        }
    }
}
