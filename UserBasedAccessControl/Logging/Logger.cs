using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserBasedAccessControl.Utils;

namespace UserBasedAccessControl.Logging
{
    static class Logger
    {
        private static void Log(string logType, string logMessage)
        {
            try
            {
                Console.WriteLine(logMessage);
                logMessage = $"{DateTime.Now.ToString()}\t{logType}:\t{logMessage}\n";
                File.AppendAllText(Constants.LogPath, logMessage);
            }
            catch (Exception ex)
            {
                LogException(ex.StackTrace, "Unable to Open Log File");
            }
        }

        public static void LogInfo(string logMessage)
        {
            Log("Info", logMessage);
        }
        public static void LogError(string logMessage)
        {
            Log("Error", logMessage);
        }
        public static void LogWarning(string logMessage)
        {
            Log("Warning", logMessage);
        }
        public static void LogException(string exception, string logMessage)
        {
            logMessage = $"{logMessage}\n{exception}";
            Log("Exception", logMessage);
        }
    }
}
