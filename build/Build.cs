using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.NerdbankGitVersioning;
using Serilog;

[GitHubActions("continuous",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    On = [GitHubActionsTrigger.Push])
]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [NerdbankGitVersioning] readonly NerdbankGitVersioning NerdbankVersioning;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            TemporaryDirectory.CreateOrCleanDirectory();
        });

    Target Versioning => _ => _
        .Executes(() =>
        {
            Log.Information("NerdbankVersioning = {Value}", NerdbankVersioning.AssemblyVersion);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetWorkloadRestore();
            DotNetTasks.DotNetRestore();
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .DependsOn(Versioning)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s =>
                s.SetNoRestore(true));
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest();
        });
}