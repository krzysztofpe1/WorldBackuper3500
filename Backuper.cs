using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldBackuper3500
{
    public class Backuper
    {
        private string MinecraftDirectory, compressedFilePath;
        public Backuper(string _LocalBackupDirectory, string _MinecraftDirectory)
        {
            compressedFilePath = _LocalBackupDirectory + DateTime.Now.ToString("dd.MM.yyyy") + "_server-pecyna.zip";
            MinecraftDirectory = _MinecraftDirectory;
        }
        public bool make(Logger logger)
        {
            if (File.Exists(compressedFilePath))
            {
                logger.writeLog("Backup was already made today.");
                return false;
            }
            try
            {
                ZipFile.CreateFromDirectory(MinecraftDirectory, compressedFilePath);
            }
            catch
            {
                logger.writeLog("Unale to create backup, may be caused by badly assigned privileges to backup directory or not existing backup directory. Also check if Minecraft worlds directory path in config file is specified properly.");
                return false;
            }
            return true;
        }
        public string getFilePath() { return compressedFilePath; }
    }
}
