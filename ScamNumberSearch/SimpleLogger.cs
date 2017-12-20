using System;
using System.IO;
using System.Reflection;

namespace ScamNumberSearch
{
    public class SimpleLogger
    {
        private readonly string logFileName = $"ScamNumbers_{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Month}_{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.log";

        private readonly string logFilePath;

        public SimpleLogger(string logFilePath = "")
        {
            // If the log file path is not specified, just default to the 
            // current working directory
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                var currentWorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (currentWorkingDirectory != null)
                {
                    this.logFilePath = currentWorkingDirectory;

                    if (File.Exists(logFileName))
                    {
                        return;
                    }

                    File.Create(Path.Combine(this.logFilePath, logFileName)).Dispose();
                    Log("Spotify Scam Phone Numbers");
                    Log(string.Empty);

                    return;
                }
            }

            this.logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            using (var stream = new StreamWriter(Path.Combine(logFilePath, logFileName)))
            {
                stream.WriteLine(message);
            }
        }
    }
}
