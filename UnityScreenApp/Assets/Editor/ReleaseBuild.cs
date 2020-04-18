﻿

using System.IO;
using UnityEditor;
using UnityEngine;

public class ReleaseBuild : MonoBehaviour
{
    [MenuItem("Build/Release Build _F5")]
    public static void Build()
    {
        string releaseFolder = "../Release/";

        // Always clean build, delete the prior build completely.
        FileUtil.DeleteFileOrDirectory(releaseFolder);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/BigScreen 3D.unity" };
        buildPlayerOptions.locationPathName = releaseFolder + "katanga.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        // Copy the key Deviare pieces to the output folder, as the
        // Plugin folder by itself is not sufficient for SpyMgr.Init to succeed.
        FileUtil.CopyFileOrDirectory("Assets/Dependencies/deviare32.db", releaseFolder + "katanga_data/Plugins/deviare32.db");
        FileUtil.CopyFileOrDirectory("Assets/Dependencies/deviare64.db", releaseFolder + "katanga_data/Plugins/deviare64.db");

        FileUtil.CopyFileOrDirectory("Stereo Pictures", releaseFolder + "Stereo Pictures");

        if (Directory.Exists(@"C:\Users\bo3b\Documents\Code\3d_fix_manager\WpfApplication3\bin\VR\Tools"))
        {
            FileUtil.DeleteFileOrDirectory(@"C:\Users\bo3b\Documents\Code\3d_fix_manager\WpfApplication3\bin\VR\Tools\katanga");
            FileUtil.CopyFileOrDirectory(releaseFolder, @"C:\Users\bo3b\Documents\Code\3d_fix_manager\WpfApplication3\bin\VR\Tools\katanga");
        }
        //FileUtil.CopyFileOrDirectory("Assets /Dependencies/katanga.exe.manifest", releaseFolder + "katanga_data/Plugins/katanga.exe.manifest");
    }

    [MenuItem("Build/Debug Build")]
    public static void DebugBuild()
    {
        string releaseFolder = "../Debug/";

        // Always clean build, delete the prior build completely.
        FileUtil.DeleteFileOrDirectory(releaseFolder);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/BigScreen 3D.unity" };
        buildPlayerOptions.locationPathName = releaseFolder + "katanga.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.Development | BuildOptions.ShowBuiltPlayer;
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        // Copy the key Deviare pieces to the output folder, as the
        // Plugin folder by itself is not sufficient for SpyMgr.Init to succeed.
        FileUtil.CopyFileOrDirectory("Assets/Dependencies/deviare32.db", releaseFolder + "katanga_data/Plugins/deviare32.db");
        FileUtil.CopyFileOrDirectory("Assets/Dependencies/deviare64.db", releaseFolder + "katanga_data/Plugins/deviare64.db");

        //FileUtil.CopyFileOrDirectory("Assets/Dependencies/katanga.exe.manifest", releaseFolder + "katanga_data/Plugins/katanga.exe.manifest");
    }


    // Demo build, which will remove all game functionality except being able to show stereo photos on the big screen.

    [MenuItem("Build/Demo Build")]
    public static void DemoBuild()
    {
        string demoFolder = "../Demo/";

        // Always clean build, delete the prior build completely.
        FileUtil.DeleteFileOrDirectory(demoFolder);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/BigScreen 3D.unity" };
        buildPlayerOptions.locationPathName = demoFolder + "katanga.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.Development | BuildOptions.ShowBuiltPlayer;
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "DEMO");
    }
}
