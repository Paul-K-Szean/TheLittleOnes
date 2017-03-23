using System;
using System.IO;
using TheLittleOnesLibrary.Entities;
namespace TheLittleOnesLibrary.Controllers
{
    public class LogController
    {
        public static string dateTimeFormat = "yyyyMMdd";
        public static string dateTimeFormat_LogMessage = "yyyyMMdd@HH:mm:ss";
        private static string fileName_LogFile;
        private static string filePath_LogFile;
        private static string filePath_LogFileDir;
        public static void Log()
        {
            fileName_LogFile = string.Concat("log", DateTime.Now.ToString(dateTimeFormat), ".txt");
            filePath_LogFile = System.Web.HttpContext.Current.Server.MapPath(string.Concat("~/logging/", fileName_LogFile));
            filePath_LogFileDir = System.Web.HttpContext.Current.Server.MapPath(string.Concat("~/logging"));
            if (!Directory.Exists(filePath_LogFileDir))
            {
                Directory.CreateDirectory(filePath_LogFileDir);
            }
            // creates log obj
            LogEntity logEntity = new LogEntity { message = Environment.NewLine };
            File.AppendAllText(filePath_LogFile,
                string.Format("{0:-5} [{1:5}] {2:5} \n", DateTime.Now.ToString(dateTimeFormat_LogMessage), logEntity.logLevel, logEntity.message));
        }
        public static void LogLine(string message)
        {
            fileName_LogFile = string.Concat("log", DateTime.Now.ToString(dateTimeFormat), ".txt");
            filePath_LogFile = System.Web.HttpContext.Current.Server.MapPath(string.Concat("~/logging/", fileName_LogFile));
            filePath_LogFileDir = System.Web.HttpContext.Current.Server.MapPath(string.Concat("~/logging"));
            if (!Directory.Exists(filePath_LogFileDir))
            {
                Directory.CreateDirectory(filePath_LogFileDir);
            }
            // creates log obj
            LogEntity logEntity = new LogEntity { message = message };
            File.AppendAllText(filePath_LogFile,
                   string.Format("{0:-5} [{1:5}] {2:5} \n", DateTime.Now.ToString(dateTimeFormat_LogMessage), logEntity.logLevel, logEntity.message));
        }
    }
}
