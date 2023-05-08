using static BuildTargets;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string envVarMissing = " environment variable is missing. Aborting.";
const string packOutput = "nuget";

Target(Restore, () =>
{
    Run("dotnet", "restore");
});

Target(CleanBuildOutput, DependsOn(Restore), () =>
{
    Run("dotnet", "clean -c Release -v m --nologo");
});

Target(Build, DependsOn(CleanBuildOutput), () =>
{
    Run("dotnet", "build -c Release --nologo");
});

Target(Test, DependsOn(Build), () =>
{
    Run("dotnet", "test -c Release --no-build");
});

Target(CleanPackOutput, () =>
{
	if (Directory.Exists(packOutput))
		Directory.Delete(packOutput, true);
});

Target(Pack, DependsOn(Build, CleanPackOutput), () =>
{
	var outputDir = Directory.CreateDirectory(packOutput);
	Run("dotnet", $"pack QdrantNet/QdrantNet.csproj -c Release -o \"{outputDir.FullName}\" --no-build --nologo");
});

Target(Default, DependsOn(Test));

await RunTargetsAndExitAsync(args, ex => ex is SimpleExec.ExitCodeException || ex.Message.EndsWith(envVarMissing));

internal static class BuildTargets
{
    public const string CleanBuildOutput = "clean-build-output";
    public const string CleanPackOutput = "clean-pack-output";
    public const string Build = "build";
    public const string Test = "test";
    public const string Default = "default";
    public const string Restore = "restore";
    public const string Pack = "pack";
}
