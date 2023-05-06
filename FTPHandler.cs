using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorldBackuper3500
{
    public class FTPHandler
    {
        private string serverPath, username, password;
        private int LogLevel;
        public FTPHandler(string _serverIP, string _serverDirectory, string _username, string _password, int _LogLevel)
        {
            serverPath = _serverIP+"/"+ _serverDirectory;
            username = _username;
            password = _password;
            LogLevel = _LogLevel;
        }
        public bool uploadFile(string pathFile, Logger logger)
        {
            string PureFileName = new FileInfo(pathFile).Name;
            string uploadUrl = "ftp://" + serverPath+PureFileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(username, password);
            request.Proxy = null;
            request.KeepAlive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // Copy the contents of the file to the request stream.
            try
            {
                /*StreamReader sourceStream = new StreamReader(pathFile);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();*/
                using (Stream input = File.OpenRead(pathFile))
                {
                    using (Stream output = request.GetRequestStream())
                    {
                        input.CopyTo(output);
                    }
                }
                logger.writeLog("File upload to "+serverPath+" successful");
                return true;
            }
            catch (Exception e)
            {
                logger.writeLog("File upload to " + serverPath + " unsuccessful");
                Console.WriteLine(uploadUrl);
                if(LogLevel==1)logger.writeLog(e.ToString());
                return false;
            }
        }
    }
}
