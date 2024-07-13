using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TaskManagement
{
    public static class ErrorLogService
    {
        public static void LogError(Exception ex)
        {
            string fileName = WebConfigurationManager.AppSettings["ErrorLogFilePath"];
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                if (!Directory.Exists(fileName))
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                if (!File.Exists(fileName))
                    File.Create(fileName).Close();

                writer.WriteAsync(DateTime.Now.ToString() + ": " + ex.Message +"\n");
            }
        }
    }
}