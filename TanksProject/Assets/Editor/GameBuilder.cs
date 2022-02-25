using UnityEditor;

namespace TanksGB.Editor
{
    public static class GameBuilder
    {
        [MenuItem("Build/Android")]
        public static void BuildAndroid()
        {
            BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                target = BuildTarget.Android, 
                locationPathName = "../artifacts/Test.apk",
                scenes = new[]
                {
                    "Assets/Scenes/MainMenu.unity",
                    "Assets/Scenes/LoadingScene.unity",
                    "Assets/Scenes/MainScene.unity"
                }
            });
        }
    }
}
