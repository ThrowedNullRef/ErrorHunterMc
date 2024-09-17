using System.Reflection;

namespace Errorhunter.Host;

public static class KnownPaths
{
    public static readonly string ExecutionDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
    public static readonly string WebClientDir = Path.Combine(ExecutionDir, "webclient");

}