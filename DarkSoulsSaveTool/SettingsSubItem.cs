using System;
using System.Xml.Serialization;

namespace Hiale.DarkSoulsSaveTool
{
    public class SettingsSubItem
    {
        private string _objectType;

        [XmlIgnore]
        public Object Instance { get; set; }

        public string ObjectType
        {
            get { return Instance == null ? _objectType : Instance.GetType().ToString(); }
            set
            {
                _objectType = value;
            }
        }

        public SerializableDictionary<string, string> SubItemValues { get; set; }

        private SettingsSubItem()
        {
            SubItemValues = new SerializableDictionary<string, string>();
        }

        public SettingsSubItem(object instance) : this()
        {
            Instance = instance;
        }
    }
}
        
    
