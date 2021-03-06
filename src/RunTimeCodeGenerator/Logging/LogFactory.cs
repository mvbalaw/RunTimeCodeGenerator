using System;

namespace RunTimeCodeGenerator.Logging
{
    public static class LogFactory
    {
        public static Logger Create(Type type)
        {
            // All types get both console and file logging
            var logger = new Logger(type.Name);
            logger.Listeners.Add(new FileListener(DefaultSettings.LogFile));
            return logger;
        }
    }
}