using System;

namespace RunTimeCodeGenerator.Logging
{
    public static class Log
    {
        public static ILogger For(Type type)
        {
            return LogFactory.Create(type);
        }
    }
}