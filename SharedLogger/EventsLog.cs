using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLogger
{
    // Required
    //Install-Package Serilog.Sinks.EventLog

    /// <summary>
    /// Using Serilog in a class library as a shared logger
    /// </summary>
    public class EventsLog
    {
        private ILogger _logger;

        public EventsLog()
        {
            // Configuration of the Serilog logger
            _logger = new LoggerConfiguration()
                 .WriteTo.EventLog("Application", manageEventSource: true)
                  .CreateLogger();
        }
        /// <summary>
        ///  Writing events to Windows events log
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
