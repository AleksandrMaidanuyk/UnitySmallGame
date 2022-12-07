using UnityEditor;
using UnityEngine;
public class CreatorAssetsBundle
{

    [MenuItem("Assets/Build AssetBundles")]     
    static void BuildAssetBundle()
    {

        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Standalone", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/IOS", BuildAssetBundleOptions.None, BuildTarget.iOS);

        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
