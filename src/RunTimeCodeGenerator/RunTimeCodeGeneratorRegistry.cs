using RunTimeCodeGenerator.ClassGeneration;
namespace RunTimeCodeGenerator
{
    public class RunTimeCodeGeneratorRegistry : StructureMap.Registry
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