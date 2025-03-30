using Serilog;
using System;


namespace LibBMS.Logger
{


    public class LibBMSLogger
    {
        private static readonly Lazy<ILogger> _logger = new Lazy<ILogger>(() =>
         {
             return new LoggerConfiguration()
                 .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                 .WriteTo.File("logs/LibBMS.log", rollingInterval: RollingInterval.Day)
                 .CreateLogger();
         });

        private LibBMSLogger() { }

        public static ILogger Instance => _logger.Value;

        public static void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }
    }

}