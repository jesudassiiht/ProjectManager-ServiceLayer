using System;
using System.IO;
using System.Web;

namespace PrjManager.Infrastructure.Logging
{
    public class TextFileLogger : ILogger
    {
        private object locker = new object();
        private enum LogType
        {
            Info = 0,
            Error = 1
        }

        public Guid Error(Exception ex)
        {
            return Error(ex.ToString());
        }

        public Guid Error(string message)
        {
            var guid = Guid.NewGuid();
            var msg = string.Format("Reference Id: {0}, Details {1}", guid, message);
            Write(LogType.Error, msg);

            return guid;
        }

        public void Info(string message)
        {
            Write(LogType.Info, message);
        }

        public void InfoFormat(string format, params object[] values)
        {
            var message = string.Format(format, values);
            Write(LogType.Error, message);
        }

        private void Write(LogType logType, string message)
        {
            var dir = GetLogDirectory();
            var fileName = GetFileName();
            var fullName = Path.Combine(dir, fileName);

            try
            {
                lock (locker)
                {
                    using (var writer = File.AppendText(fullName))
                    {
                        writer.WriteLine("{0} {1} {2}", DateTime.Now, logType.ToString().ToUpperInvariant(), message);
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception)
            {
                // There was a problem in writing log
                throw;
            }
        }

        private string GetLogDirectory()
        {
            var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logging");
            
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            return logDir;
        }

        private string GetFileName()
        {
            return string.Format("Logging-{0}.txt", DateTime.Today.ToString("MM-dd-yyyy"));
        }
    }
}
