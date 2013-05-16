using RunTimeCodeGenerator.ClassGeneration;

using StructureMap.Configuration.DSL;

namespace RunTimeCodeGenerator
{
    public class RunTimeCodeGeneratorRegistry : Registry
    {
        public RunTimeCodeGeneratorRegistry()
        {
            For<IClassGenerator>()
                .Use<ClassFileGenerator>();
            Scan(s =>
            {
                s.AssemblyContainingType<RunTimeCodeGeneratorRegistry>();
                s.WithDefaultConventions();
            });
        }
    }
}