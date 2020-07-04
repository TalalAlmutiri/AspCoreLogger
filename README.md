# AspCoreLogger

Using Serilog to create a shared logger in a class library (asp.net core 3, C#)

A small project for creating a class library as a shared logger to write logs into the Windows event log or a file.

Windows event log class

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
    
Log to file class

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
    
Calling the shared logger class in the Startup.cs

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)     
       {    
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    // Calling the shared logger class
                    // Add the reference using SharedLogger;
                    new EventsLog().WriteLog("Test event");
                    new LogToFile().WriteLog("Test event");
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
 
