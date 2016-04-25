using System;
using System.Collections.Generic;
using System.IO;

namespace Hiale.DarkSoulsSaveTool
{
    public abstract class GameSaveData
    {
        internal static Dictionary<string, string> SettingsValues = new Dictionary<string, string>();

        public abstract string Name { get; }

        public abstract string WindowTitle { get; }

        public abstract string SourceFile { get; set; }

        public abstract string TargetDirectory { get; set; }

        public abstract string FilePattern { get; }

        public abstract bool Init();

        protected bool FindSourceFile(string basePath)
        {
            if (!Directory.Exists(basePath))
                return false;
            var subDirs = Directory.GetDirectories(basePath);
            string sourceFile = null;
            var totalFileCount = 0;
            foreach (var gameTag in subDirs)
            {
                totalFileCount = FindSourceFile(gameTag, out sourceFile);
            }
            if (totalFileCount == 0)
                return false;
            if (totalFileCount == 1)
                SourceFile = sourceFile;
            else if (subDirs.Length == 1)
                SourceFile = subDirs[0];
            else
                SourceFile = basePath;
            return true;
        }

        private int FindSourceFile(string path, out string sourceFile)
        {
            var files = Directory.GetFiles(path, FilePattern);
            sourceFile = files.Length == 1 ? files[0] : null;
            return files.Length;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class DarkSoulsSaveData : GameSaveData
    {
        private const string SaveSubDir = "NBGI\\DarkSouls";

        public override string Name
        {
            get { return WindowTitle; }
        }

        public override string WindowTitle
        {
            get { return "DARK SOULS"; }
        }

        public override string SourceFile { get; set; }

        public override string TargetDirectory { get; set; }

        public override string FilePattern
        {
            get { return "draks*.sl2"; }
        }

        public override bool Init()
        {
            return FindSourceFile();
        }

        private bool FindSourceFile()
        {
            var sourceFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sourceFile = Path.Combine(sourceFile, SaveSubDir);
            return FindSourceFile(sourceFile);
        }
    }

    public class DarkSouls2SaveData : GameSaveData
    {
        private const string SaveSubDir = "DarkSoulsII";

        public override string Name
        {
            get { return WindowTitle; }
        }

        public override string WindowTitle
        {
            get { return "DARK SOULS II"; }
        }

        public override string SourceFile { get; set; }

        public override string TargetDirectory { get; set; }

        public override string FilePattern
        {
            get { return "DARKSII*.sl2"; }
        }

        public override bool Init()
        {
            return FindSourceFile();
        }

        private bool FindSourceFile()
        {
            var sourceFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            sourceFile = Path.Combine(sourceFile, SaveSubDir);
            return FindSourceFile(sourceFile);
        }
    }

    //public class DarkSouls2ScholarSaveData : GameSaveData
    //{
    //    private const string SaveSubDir = "DarkSoulsII";

    //    public override string Name
    //    {
    //        get { return "DARK SOULS II: Scholar of the First Sin (NOT TESTED)"; }
    //    }

    //    public override string WindowTitle
    //    {
    //        get { return "DARK SOULS II"; } //ToDo
    //    }

    //    public override string SourceFile { get; set; }

    //    public override string TargetDirectory { get; set; }

    //    public override string FilePattern
    //    {
    //        get { return "DS2SOFS*.sl2"; }
    //    }

    //    public override bool Init()
    //    {
    //        return FindSourceFile();
    //    }

    //    private bool FindSourceFile()
    //    {
    //        var sourceFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    //        sourceFile = Path.Combine(sourceFile, SaveSubDir);
    //        return FindSourceFile(sourceFile);
    //    }
    //}

    public class DarkSouls3SaveData : GameSaveData
    {
        private const string SaveSubDir = "DarkSoulsIII";

        public override string Name
        {
            get { return WindowTitle; }
        }

        public override string WindowTitle
        {
            get { return "DARK SOULS III"; }
        }

        public override string SourceFile { get; set; }

        public override string TargetDirectory { get; set; }

        public override string FilePattern
        {
            get { return "DS3*.sl2"; }
        }

        public override bool Init()
        {
            return FindSourceFile();
        }

        private bool FindSourceFile()
        {
            var sourceFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            sourceFile = Path.Combine(sourceFile, SaveSubDir);
            return FindSourceFile(sourceFile);
        }
    }
}