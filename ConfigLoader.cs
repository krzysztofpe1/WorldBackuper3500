using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldBackuper3500
{
    public static class ConfigLoader
    {
        public static bool LoadConfig(ref string FTPserverIP, ref string FTPBackupDirectory, ref string FTPuser, ref string FTPpassword, ref string LocalBackupDirectory, ref string TmuxSessionName, ref string LogFile, ref string MinecraftStartFile, ref string MinecraftDirectory, ref int LogLevel)
        {
            try
            {
                int sum = 0;
                foreach (string line in System.IO.File.ReadLines("WorldBackuper3500.conf"))
                {
                    string[] arr = line.Split("=");
                    if (arr.Length != 2 || arr[0][0] == '#') continue;
                    switch (arr[0])
                    {
                        case "FTPserverIP":
                            FTPserverIP = arr[1];
                            sum++;
                            break;
                        case "FTPBackupDirectory":
                            FTPBackupDirectory = arr[1];
                            if (FTPBackupDirectory[FTPBackupDirectory.Length - 1] != '/') FTPBackupDirectory += '/';
                             sum++;
                            break;
                        case "FTPuser":
                            FTPuser = arr[1];
                            sum++;
                            break;
                        case "FTPpassword":
                            FTPpassword = arr[1];
                            sum++;
                            break;
                        case "LocalBackupDirectory":
                            LocalBackupDirectory = arr[1];
                            if (LocalBackupDirectory[LocalBackupDirectory.Length - 1] != '/') LocalBackupDirectory += '/';
                             sum++;
                            break;
                        case "TmuxSessionName":
                            TmuxSessionName = arr[1];
                            sum++;
                            break;
                        case "LogDirectory":
                            LogFile = arr[1];
                            if (LogFile[LogFile.Length - 1] != '/') LogFile += '/';
                            LogFile += "WorldBackuper3500.log";
                             sum++;
                            break;
                        case "MinecraftStartFile":
                            MinecraftStartFile = arr[1];
                            sum++;
                            break;
                        case "MinecraftDirectory":
                            MinecraftDirectory = arr[1];
                            if (MinecraftDirectory[MinecraftDirectory.Length - 1] != '/') MinecraftDirectory += '/';
                             sum++;
                            break;
                        case "LogLevel":
                            LogLevel = arr[1] == "DEBUG" ? 1 : 0;
                            sum++;
                            break;
                    }
                }
                if (sum < 10)
                {
                    if (LogFile != "")
                    {
                        Logger logger = new(LogFile);
                        logger.writeLog("Config file doesnt have enough constants");
                    }
                    else Console.WriteLine("Config file doesnt have enough constants");
                    return false;
                }
                if (FTPserverIP == "" ||
                    FTPBackupDirectory == "" ||
                    FTPuser == "" ||
                    FTPpassword == "" ||
                    LocalBackupDirectory == "" ||
                    TmuxSessionName == "" ||
                    LogFile == "" ||
                    MinecraftStartFile == "")
                {
                    if (LogFile != "")
                    {
                        Logger logger = new(LogFile);
                        logger.writeLog("At least one constant in config file was left blank or deleted");
                    }
                    else Console.WriteLine("At least one constant in config file was left blank or deleted");
                    return false;
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Config file is unreachable");
                return false;
            }
        }
    }
}
