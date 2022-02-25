#addin nuget:?package=Cake.Unity&version=0.8.1

var target = Argument("target", "Build-Android");

Task("Clean-Artifacts")
    .Does(() =>
{
    CleanDirectory($"./artifacts");
});

using static Cake.Unity.Arguments.BuildTarget;
Task("Build-Android")
    .IsDependentOn("Clean-Artifacts")
    .Does(() =>
{
    UnityEditor(
    new UnityEditorArguments
    {
        ProjectPath = "./TanksProject",
        ExecuteMethod = "Tanks.Editor.GameBuilder.BuildAndroid",
        BuildTarget = Android,
        LogFile = "./artifacts/Unity.log",
    },
    new UnityEditorSettings
    {
        RealTimeLog = true,
    });
});
RunTarget(target);