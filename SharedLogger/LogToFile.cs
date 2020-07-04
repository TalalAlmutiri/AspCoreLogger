using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLogger
{
    // Required
    //Install-Package Serilog.Sinks.File

    /// <summary>
    /// Using Serilog in a class library as a shared logger to write logs into a file
    /// </summary>
    public class LogToFile
    {
        private ILogger _logger;

        public LogToFile()
        {
            _logger = new LoggerConfiguration()
                     .WriteTo.File("D://log.txt", rollingInterval: RollingInterval.Month)
                     .CreateLogger();
        }
        /// <summary>
        ///  Writing events to a file
        /// </summary>
        /// <param name="text"></param>
        public void WriteLog(string text)
        {
            try
            {

                _logger.Information(text);
                _logger.Error(text);
            }
            catch
            {
                throw;
            }
        }
    }
}
