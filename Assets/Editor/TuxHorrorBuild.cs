#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class TuxHorrorBuild
{
    public static void BuildWindows()
    {
        const string scenePath = "Assets/Scenes/TuxHorror_Prototype.unity";
        const string outputDirectory = "build/StandaloneWindows64";
        const string outputPath = outputDirectory + "/TuxHorror.exe";

        try
        {
            TuxHorrorSceneBuilder.CreateStarterScene();

            if (!File.Exists(scenePath))
                throw new FileNotFoundException("The prototype scene was not generated.", scenePath);

            Directory.CreateDirectory(outputDirectory);

            PlayerSettings.productName = "TuxHorror";
            PlayerSettings.companyName = "Carson";
            PlayerSettings.defaultScreenWidth = 1280;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(scenePath, true)
            };

            BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { scenePath },
                locationPathName = outputPath,
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
            });

            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError("TuxHorror Windows build failed: " + report.summary.result);
                EditorApplication.Exit(1);
                return;
            }

            Debug.Log("TuxHorror Windows build succeeded: " + outputPath);
            EditorApplication.Exit(0);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            EditorApplication.Exit(1);
        }
    }
}
#endif
