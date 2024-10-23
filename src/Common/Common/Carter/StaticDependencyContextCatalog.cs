using Carter;
using System.Reflection;

namespace Common.Carter
{
    public class StaticDependencyContextCatalog(Assembly assembly) : DependencyContextAssemblyCatalog
    {
        private readonly Assembly _assembly = assembly;

        public override IReadOnlyCollection<Assembly> GetAssemblies()
        {
            return [_assembly];
        }
    }
}
