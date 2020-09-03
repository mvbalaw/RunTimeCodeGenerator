using StructureMap;

namespace RunTimeCodeGenerator
{
    public static class Bootstrapper
    {
        private static bool _initialized;

        private static void Initialize()
        {
	        // ReSharper disable once UnusedVariable
	        var container = new Container(new RunTimeCodeGeneratorRegistry());
        }

        public static void Reset()
        {
            if (!_initialized)
            {
                Initialize();
                _initialized = true;
            }
        }
    }
}