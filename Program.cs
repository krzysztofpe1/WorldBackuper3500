using System;
using System.IO;
using System.Net;
using System.Threading;

namespace WorldBackuper3500
{
    class Program
    {
        static void Main(string[] args)
        {
            string FTPserverIP = "";
            string FTPBackupDirectory = "";
            string FTPuser= "";
            string FTPpassword= "";
            string LocalBackupDirectory= "";
            string TmuxSessionName= "";
            string LogFile = "";
            string MinecraftStartFile= "";
            string MinecraftDirectory = "";
            int LogLevel=0;
            //FTPHandler ftphandler = new("benzyna.my.to", "minecraft", "ftpclient", "ftp", logger);
            //ftphandler.uploadFile("C:\\Users\\krzys\\id3tagviewer");
            //MinecraftManager.saveWorld();
            if (!ConfigLoader.LoadConfig(ref FTPserverIP,
                                       ref FTPBackupDirectory,
                                       ref FTPuser,
                                       ref FTPpassword,
                                       ref LocalBackupDirectory,
                                       ref TmuxSessionName,
                                       ref LogFile,
                                       ref MinecraftStartFile,
                                       ref MinecraftDirectory,
                                       ref LogLevel)) return;
            Logger logger = new(LogFile);
            MinecraftManager minecraftManager = new(TmuxSessionName, MinecraftStartFile);
            Backuper backuper = new(LocalBackupDirectory, MinecraftDirectory);
            minecraftManager.stop();
            Thread.Sleep(15000);
            bool backupSuccess = backuper.make(logger);
            minecraftManager.start();
            if (backupSuccess)
            {
                //backup na serwer
                FTPHandler ftpHandler = new(FTPserverIP, FTPBackupDirectory, FTPuser, FTPpassword, LogLevel);
                ftpHandler.uploadFile(backuper.getFilePath(), logger);
            }
        }
    }
}
